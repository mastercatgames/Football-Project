using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 5.0f;
    public float normalSpeed = 5.0f;
    public float runSpeed = 15.0f;
    private float jumpHeight = 1.0f;
    public float shootForce = 1.0f;
    private float gravityValue = -9.81f;
    private bool isRunning = false;
    public bool isHavingBall = false;
    public Transform ball;
    public float ballSpeed = 10f;

    public Rigidbody rb;

    public float distanceOfPlayer;
    public float followBallSpeed;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        controller.enabled = false;
    }

    // private void LateUpdate()
    // {
    //     distanceOfPlayer = Vector3.Distance(ball.position, transform.position);
    //     if (isHavingBall)
    //     {
    //         normalSpeed = 0f;

    //         transform.LookAt(ball.parent.Find("playerLookAt").position);

    //         if (distanceOfPlayer > 4f)
    //         {
    //             //transform.position = Vector3.MoveTowards(transform.position, ball.position, 15f * Time.deltaTime);
    //             //transform.position = Vector3.Lerp(transform.position, ball.position, Time.deltaTime * 10f);
    //             transform.Translate(Vector3.forward * followBallSpeed * Time.deltaTime);
    //         }
    //     }
    // }

    private void FixedUpdate() {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // print("move: " + move);
        // print("move normalized: " + move.normalized);

        rb.MovePosition(transform.position + move * playerSpeed * Time.deltaTime);

        

        // if (move != Vector3.zero)
        // {
        //     rb.MovePosition(transform.position + transform.forward * 5.0f * Time.deltaTime);
        // }
    }

    void Update()
    {
        

        if (isHavingBall)
        {
            //playerSpeed = 0f;            
            //transform.LookAt(ball.parent.Find("playerLookAt").position);
        }

        // distanceOfPlayer = Vector3.Distance(ball.position, transform.position);

        // if (distanceOfPlayer > 5f)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, ball.position, 5f * Time.deltaTime);
        // }

        // if (distanceOfPlayer > 5f)
        // {
        //     //Follow the ball
        //     //transform.LookAt(ball.parent.Find("playerLookAt"));
        //     transform.Translate(Vector3.forward * 15f * Time.deltaTime);

        // }
        // if (distanceOfPlayer < 3.5f)
        // {
        //     //transform.LookAt(ball);
        //     //Vector3 direction = ball.position - transform.position;

        //     //transform.LookAt(ball);
        //     //transform.Translate(Vector3.forward * 2 * Time.deltaTime);



        //     // if (distanceOfPlayer <= 3.5f)
        //     // {
        //     //     isHavingBall = false;
        //     // }

        //     //print("Direction: " + direction);
        // }
        // else
        // {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

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

            gameObject.transform.forward = move;

            // if (isHavingBall)
            // {
            //     print("Carry ball!");
            //     ball.GetComponent<SimplePlayerMove>().Move(300f, 1f);
            // }

            //ball.GetComponent<SimplePlayerMove>().Move(ballSpeed);
        }

        // Changes the height position of the player..
        // if (Input.GetButtonDown("Jump") && groundedPlayer)
        // {
        //     playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        //     //GetComponent<Rigidbody>().velocity = transform.forward * shootForce;

        //     //playerVelocity.x += Mathf.Sqrt(shootForce * 3.0f);
        // }

        // if (Input.GetKey(KeyCode.Space))
        // {
        //     rb.velocity = transform.forward * shootForce;
        // }

        // if (playerVelocity.x > 0)
        // {
        //     playerVelocity.x -= Time.deltaTime * shootForce;
        // }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);



        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ball"/* && !isHavingBall*/)
        {
            //transform.position = new Vector3(0f, 1.28f, -2f);
            this.enabled = false;


            isHavingBall = true;          

            //ball.GetComponent<SimplePlayerMove>().Move(300f);

            // controller.enabled = false;            
            ball.GetComponent<SimplePlayerMove>().enabled = true;
            // transform.parent = other.transform.parent.Find("Direction");            
            // transform.position = new Vector3(0f, 1.28f, -2f);


            //transform.position = Vector3.zero;

            //normalSpeed = 0f;

            //Join ball
            // SpringJoint joint = ball.gameObject.AddComponent<SpringJoint>();
            // joint.autoConfigureConnectedAnchor = false;
            // joint.connectedAnchor = new Vector3(0f, 0f, 2f);
            // // joint.connectedBody = transform.Find("Foot").GetComponent<Rigidbody>();
            // joint.connectedBody = rb;
            // joint.connectedMassScale = 20f;
            // // joint.massScale = .001f;
            // // joint.damper = 2f;
            // //joint.maxDistance = 1.5f;
            // //ball.GetComponent<SimplePlayerMove>().joint = joint;

            ball.GetComponent<Rigidbody>().drag = 0;

            /*
            ball = other.transform;
            print(other.transform.parent.Find("Direction"));
            Vector3 oriPosition = transform.position;
            transform.parent = other.transform.parent.Find("Direction");
            other.transform.parent.Find("Direction").position = oriPosition;
            other.transform.GetComponent<SimplePlayerMove>().enabled = true;
            */
        }
    }

    public void EnablePlayerToRun()
    {
        //controller.enabled = true;
        isHavingBall = false;
        ball.GetComponent<SimplePlayerMove>().enabled = false;
    }
}
