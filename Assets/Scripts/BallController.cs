using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed, runMaxSpeed, normalMaxSpeed, maxSpeed;
    public bool isMovingBall, isJoggingBall;   
    public ForceMode forceModeMovement;
    public Transform currentPlayer;
    private FollowObject playerFollowController;    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerFollowController = currentPlayer.GetComponent<FollowObject>();
    }    
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        if (movement != Vector3.zero)
        {
            isMovingBall = true;            
            JogBall(movement);
        }
        else
        {
            isMovingBall = false;
        }

        //rb.AddForce(movement * speed); // Move Ball constantly (ordinary movement)

        //Keep a limit of velocity
        if (rb.velocity.magnitude > maxSpeed)
        {            
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = runMaxSpeed;
            playerFollowController.speed = playerFollowController.runSpeed;
        }
        else
        {
            maxSpeed = normalMaxSpeed;
            playerFollowController.speed = playerFollowController.normalSpeed;
        }

        //print(rb.velocity.magnitude);
    }

    void JogBall(Vector3 movement)
    {
        //A little force the push the ball in front of the player
        //Idea: When this push happens, try to increase the minDistance variable
        //to the player not follow the ball with so much rigidity
        if (!isJoggingBall && playerFollowController.distanceOfPlayer < 2)
        {
            isJoggingBall = true;

            rb.AddForce(movement * speed, forceModeMovement);
            rb.AddForce(Vector3.up, forceModeMovement);
            Invoke("ResetIsJoggingBall", 0.8f);
        }
    }

    void ResetIsJoggingBall()
    {
        isJoggingBall = false;
    }
}
