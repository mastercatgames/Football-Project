using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed;
    public float runMaxSpeed;
    public float normalMaxSpeed;
    private float maxSpeed;
    public Transform playerLookAt;
    
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        playerLookAt.position = new Vector3(transform.position.x, 2.4f, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        rb.AddForce(movement * speed);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = runMaxSpeed;
            //TODO: Increase the player following speed too (or using the same variable)
        }
        else
        {
            maxSpeed = normalMaxSpeed;
        }

        print(rb.velocity.magnitude);
    }

    void JogBall()
    {
        //A little force the push the ball in front of the player
        //Idea: When this push happens, try to increase the minDistance variable
        //to the player not follow the ball with so much rigidity
    }
}
