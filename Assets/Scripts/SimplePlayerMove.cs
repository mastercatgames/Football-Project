using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float maxSpeed = 10f;

    private Rigidbody rb;
    public float shootPower = 20f;
    public float upForce = 20f;
    //public float gravityValue;
    public bool isShooting = false;
    //public bool isMoving = false;
    public bool isMovingBall = false;
    public Transform direction;
    public ForceMode forceModeShoot;
    public ForceMode forceModeMovement;

    //public SpringJoint joint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (Input.GetButton("Fire2"))
        {
            #region tests
            //rb.velocity = transform.forward * shootPower;
            //            rb.AddForce(transform.parent.forward * Time.deltaTime * shootPower * 1000f, ForceMode.Acceleration);

            // rb.AddForce(transform.parent.forward * Time.deltaTime * shootPower * 1f, ForceMode.VelocityChange);
            // rb.AddForce(transform.parent.up * Time.deltaTime * shootPower * 1f, ForceMode.VelocityChange);

            // rb.AddForce(transform.parent.forward * Time.deltaTime * shootPower * 1f, ForceMode.Acceleration);
            // rb.AddForce(transform.parent.up * Time.deltaTime * shootPower * 1f, ForceMode.Acceleration);

            // rb.AddForce(transform.parent.forward * Time.deltaTime * shootPower * 20f, ForceMode.Impulse);
            // rb.AddForce(transform.parent.up * Time.deltaTime * shootPower * 5f, ForceMode.Impulse);
            #endregion

            if (shootPower < 100f)
            {
                shootPower += Time.deltaTime * 50f;


                // if (joint != null)
                // {
                //     Destroy(GetComponent<SpringJoint>());
                // }
            }
        }

        if (Input.GetButtonUp("Fire2") && isShooting == false)
        {
            Shoot(shootPower);
        }

        //rb.AddForce(1, gravityValue, 1);

        float horizontaInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontaInput, 0, verticalInput);
        movementDirection.Normalize();

        direction.position = transform.position;

        transform.parent.Find("playerLookAt").position = new Vector3(transform.position.x, 2.4f, transform.position.z);

        if (movementDirection != Vector3.zero)
        {
            // Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            //Transform direction = transform.Find("Direction");
            direction.forward = movementDirection;

            //     // direction.rotation = Quaternion.RotateTowards(direction.rotation, toRotation, 10f * Time.deltaTime);

                if (!isMovingBall)
                {
                    Move(moveSpeed);
                }
            //     rb.AddForce(new Vector3(horizontaInput, 0f, verticalInput) * moveSpeed, forceModeMovement);
        }

        //moveSpeed = 12f;

        //if (Input.GetButton("Fire3"))
        // if (Input.GetKey(KeyCode.LeftShift))
        // {
        //     moveSpeed = moveSpeed + 10f;
        // }
        // else {
        //     moveSpeed = 300f;
        // }
        // if (/*isMoving && */!isMovingBall)
        // {
        //     Move(5f);
        // }

        //transform.Find("Direction").Translate(movementDirection * speed * Time.deltaTime, Space.World);
        //print(rb.velocity.magnitude);

        //rb.AddForce(new Vector3(horizontaInput * moveSpeed, gravityValue, verticalInput * moveSpeed), ForceMode.Acceleration);

        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        //rb.velocity.y += gravityValue /** Time.deltaTime*/;

        //print("Velocity (magnitude): " + rb.velocity.magnitude);

        if (rb.velocity.magnitude < maxSpeed)
        {
            //rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
            //rb.AddForce(new Vector3(horizontaInput * moveSpeed, gravityValue, verticalInput * moveSpeed), /*ForceMode.Acceleration*/forceModeMovement);
        }
    }

    void ResetIsShooting()
    {
        isShooting = false;
        shootPower = 20f;
    }

    void Shoot(float force)
    {
        //TODO: Indentify why sometimes the SpringJoint won't destroyed when Shoot
        Destroy(GetComponent<SpringJoint>());
        Destroy(GetComponent<SpringJoint>());
        Destroy(GetComponent<SpringJoint>());
        isShooting = true;
        rb.AddForce(direction.forward * Time.deltaTime * force * 50f, forceModeShoot /*ForceMode.Impulse*/);
        rb.AddForce(direction.up * Time.deltaTime * force * upForce, forceModeShoot/*ForceMode.Impulse*/);

        //Curve Test
        //rb.AddForceAtPosition(direction.right * Time.deltaTime * 500f, transform.position, ForceMode.VelocityChange/*ForceMode.Impulse*/);

        print("Shoot! Force: " + force);
        Invoke("ResetIsShooting", 1f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().EnablePlayerToRun();
    }

    public void Move(float force, float interval = 0.8f)
    {
        if (!isMovingBall && rb.velocity.magnitude < maxSpeed)
        {
            isMovingBall = true;
            Vector3 velocity = direction.forward * Time.deltaTime * force;
            //velocity.y += gravityValue * Time.deltaTime;
            rb.AddForce(velocity, forceModeMovement/*ForceMode.Impulse*/);
           // rb.AddForce(direction.up * Time.deltaTime * force * 20f, forceModeMovement/*ForceMode.Impulse*/);
            print("Move! Force: " + force);
            Invoke("ResetIsMovingBall", interval);
        }
    }

    void ResetIsMovingBall()
    {
        isMovingBall = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "GoalCollider")
        {
            rb.drag = 1;
            print("Goal!");
        }

        // if (other.name == "Foot")
        // {
        //     Move(moveSpeed);
        // }
    }
}
