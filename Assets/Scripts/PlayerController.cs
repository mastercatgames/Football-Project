using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed, normalSpeed, runSpeed, shootForce;
    private bool isRunning, isHavingBall;
    public BallController ballController;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballController = GameObject.Find("Ball").GetComponent<BallController>();
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (move != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isRunning = true;
                playerSpeed = runSpeed;
            }
            else
            {
                isRunning = false;
                playerSpeed = normalSpeed;
            }

            rb.MovePosition(transform.position + move * playerSpeed * Time.deltaTime);
            gameObject.transform.forward = move; //Rotation
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ball" && !ballController.isShooting)
        {
            this.enabled = false;
            isHavingBall = true;
            //ballController.enabled = true;
            ballController.currentPlayer = this.transform;
            ballController.playerFollowController = GetComponent<FollowObject>();

            rb.isKinematic = true;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<FollowObject>().enabled = true;

            ballController.GetComponent<Rigidbody>().drag = 2f;
        }
    }

    public void EnablePlayerToRun()
    {
        //After shoot/pass
        //TODO: Make it in ballController
        this.enabled = true;
        isHavingBall = false;
        //ballController.enabled = false;
        ballController.currentPlayer = null;

        rb.isKinematic = false;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<FollowObject>().enabled = false;
    }
}
