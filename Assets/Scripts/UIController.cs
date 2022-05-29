using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public AnimationController anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<AnimationController>();
    }    
}
