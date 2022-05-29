using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed, normalSpeed, runSpeed, shootForce, normalShootForce, shootLimit;
    public bool isRunning, isHavingBall, isPreShooting;
    public BallController ballController;
    private Rigidbody rb;
    private AnimationController animationController;
    public Transform GoalToLookAt, playerModel;

    #region Animation States
    const string IDLE = "Idle";
    const string JOG_FORWARD = "Jog Forward";
    const string SHOOT = "Shoot";
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballController = GameObject.Find("Ball").GetComponent<BallController>();
        animationController = GetComponentInChildren<AnimationController>();
    }

    private void FixedUpdate()
    {
        if (!isHavingBall)
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
                MovePlayerAnimState(true);
            }
            else
            {
                MovePlayerAnimState(false);
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (shootForce < shootLimit)
            {
                shootForce += Time.deltaTime * 300f;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && !ballController.isShooting)
        {
            if (isHavingBall)
            {
                animationController.ChangeState(SHOOT);
                //ballController.Shoot();                
            }
            else
            {
                isPreShooting = true;
                GetComponent<FollowObject>().enabled = true;
                GetComponent<FollowObject>().speed = GetComponent<FollowObject>().runSpeed;
            }
        }

        transform.Find("AimGoal").LookAt(GoalToLookAt);

        //Fix the player model position
        playerModel.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ball" && !ballController.isShooting && !ballController.isOut)
        {
            //this.enabled = false;
            isHavingBall = true;
            //ballController.enabled = true;
            ballController.currentPlayer = this.transform;
            ballController.playerFollowController = GetComponent<FollowObject>();

            rb.isKinematic = true;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<FollowObject>().enabled = true;

            ballController.GetComponent<Rigidbody>().drag = 2f;

            if (isPreShooting)
            {
                animationController.ChangeState(SHOOT);
                isPreShooting = false;
                GetComponent<FollowObject>().speed = GetComponent<FollowObject>().normalSpeed;
            }
        }
    }

    public void EnablePlayerToRun()
    {
        //After shoot/pass
        //TODO: Make it in ballController
        //this.enabled = true;
        isHavingBall = false;
        //ballController.enabled = false;
        ballController.currentPlayer = null;

        rb.isKinematic = false;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<FollowObject>().enabled = false;

        shootForce = normalShootForce;
    }

    public void MovePlayerAnimState(bool isMoving)
    {
        if (animationController.currentAnimaton != SHOOT)
        {
            if (isMoving)
            {
                animationController.ChangeState(JOG_FORWARD);
            }
            else
            {
                animationController.ChangeState(IDLE);
            }
        }
    }
}
