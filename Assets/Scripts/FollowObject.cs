using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform ball;
    public float normalSpeed, runSpeed, speed, distanceOfPlayer;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        distanceOfPlayer = Vector3.Distance(ball.position, transform.position);
        if (distanceOfPlayer > 2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, ball.position, speed * Time.fixedDeltaTime); // Move player to the ball
            transform.position = new Vector3(transform.position.x, 2f, transform.position.z); //Keep the player on ground
        }

        LookAtBall();        
    }

    void LookAtBall()
    {
        Vector3 rot = Quaternion.LookRotation(ball.position - transform.position).eulerAngles;
        rot.x = rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);
    }
}
