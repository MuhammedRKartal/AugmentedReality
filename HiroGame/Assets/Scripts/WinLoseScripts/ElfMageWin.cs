using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfMageWin : MonoBehaviour
{
    public Transform elf;
    static Animator anim;

    void Start()
    {
        anim = elf.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.Play("Idle");
    }
}
