using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public float speed, runMaxSpeed, normalMaxSpeed, maxSpeed;
    public bool isMovingBall, isJoggingBall, isShooting, isOut;
    public ForceMode forceModeMovement, forceModeShoot;
    public Transform currentPlayer, LeftGoal;
    public FollowObject playerFollowController;
    private Rigidbody rb;
    private UIController uiController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        uiController = GameObject.FindWithTag("UI").GetComponent<UIController>();
    }
    void FixedUpdate()
    {
        if (currentPlayer != null)
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

            currentPlayer.GetComponent<PlayerController>().MovePlayerAnimState(isMovingBall);

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
        }

        // if (transform.position.y < 1f && rb.velocity.magnitude < 4)
        // {
        //     //Break the ball if its rolling some time
        //     rb.drag = 2f;
        // }
        // else
        // {
        //     rb.drag = 0f;
        // }

        //print(rb.velocity.magnitude);
    }

    void JogBall(Vector3 movement)
    {
        //A little force the push the ball in front of the player
        //Idea: When this push happens, try to increase the minDistance variable
        //to the player not follow the ball with so much rigidity
        if (!isJoggingBall && playerFollowController.distanceOfPlayer < 0.9)
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

    void ResetIsShooting()
    {
        isShooting = false;
    }

    public void Shoot()
    {
        float force = currentPlayer.GetComponent<PlayerController>().shootForce;
        float upForce = currentPlayer.GetComponent<PlayerController>().shootForce - 200f; //if L1 is pressed: -70f


        //float upForce = 40f;
        rb.AddForce(currentPlayer.Find("AimGoal").forward * Time.deltaTime * force, forceModeShoot);
        rb.AddForce(currentPlayer.Find("AimGoal").up * Time.deltaTime * upForce, forceModeShoot);

        rb.drag = 0.01f;

        isShooting = true;
        Invoke("ResetIsShooting", 0.8f);
        currentPlayer.GetComponent<PlayerController>().EnablePlayerToRun();
    }

    private void OnTriggerEnter(Collider other)
    {

        //Temporary
        if (other.name.Contains("throwin")
        || other.name.Contains("corner")
        || other.name.Contains("Goal"))
        {
            if (other.name == "GoalCollider")
            {
                rb.drag = 2;
                print("Goal!");
            }

            //Get current position of the ball

            isOut = true;
            uiController.anim.ChangeState("FadeOut");

            if (currentPlayer != null)
            {
                currentPlayer.GetComponent<PlayerController>().EnablePlayerToRun();
            }

            Invoke("ReloadScene", 2f);
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    // private void CenterBallAndPlayer()
    // {
    //     transform.localPosition = new Vector3 (2.5f, 0f, 0f);
    //     currentPlayer.localPosition = new Vector3 (2.5f, 0f, 0f);
    // }
}
