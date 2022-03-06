using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform ball;
    //public Vector3 offset;
    public float speed;
    [SerializeField]
    private float distanceOfPlayer;
    private Rigidbody rb;
    public float minDistance = 0.09f;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //This code works with a little jitter
        // distanceOfPlayer = Vector3.Distance(ball.position, transform.position);
        // if (distanceOfPlayer > 2f)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, ball.position, speed * Time.fixedDeltaTime);
        // }

        //Another solution using rigidbody (not tested yet)
        //Link: https://stackoverflow.com/questions/52116241/how-can-i-use-rigidbody2d-velocity-to-follow-an-object-in-unity
        //Find direction
        // Vector3 dir = (ball.position - rb.position).normalized;
        // //Check if we need to follow object then do so 
        // if (Vector3.Distance(ball.position, rb.position) > minDistance)
        // {
        //     rb.MovePosition(rb.transform.position + dir * speed * Time.fixedDeltaTime);
        // }

        //transform.LookAt(ball.GetComponent<BallController>().playerLookAt);           

        //Vector3 dir = (ball.position - transform.position).normalized;


        distanceOfPlayer = Vector3.Distance(ball.position, transform.position);
        if (distanceOfPlayer > 2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, ball.position, speed * Time.fixedDeltaTime); // Move player to the ball
            transform.position = new Vector3(transform.position.x, 2f, transform.position.z); //Keep the player on ground
        }

        //Look at the ball
        Vector3 rot = Quaternion.LookRotation(ball.position - transform.position).eulerAngles;
        rot.x = rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);
    }
}
