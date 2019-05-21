using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerWin : MonoBehaviour
{
    public Transform berserker;
    static Animator anim;
    
    void Start()
    {   
        anim = berserker.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {    
        anim.Play("Idle");
    }
}
