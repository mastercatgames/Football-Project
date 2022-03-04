using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed;
    public float runMaxSpeed;
    public float normalMaxSpeed;
    private float maxSpeed;
    
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        }
        else
        {
            maxSpeed = normalMaxSpeed;
        }

        print(rb.velocity.magnitude);
    }
}
