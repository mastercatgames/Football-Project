
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public string currentAnimaton;
    public Animator animator;

    // //Animation States
    // const string PLAYER_IDLE = "Player_idle";
    // const string PLAYER_RUN = "Player_run";
    // const string PLAYER_JUMP = "Player_jump";
    // const string PLAYER_ATTACK = "Player_attack";
    // const string PLAYER_AIR_ATTACK = "Player_air_attack";

    public void ChangeState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}
