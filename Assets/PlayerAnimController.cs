using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void Shoot()
    {
        playerController.ballController.Shoot();
    }

    public void SetToIdle()
    {
        playerController.GetComponent<AnimationController>().ChangeState("Idle");
    }
}
