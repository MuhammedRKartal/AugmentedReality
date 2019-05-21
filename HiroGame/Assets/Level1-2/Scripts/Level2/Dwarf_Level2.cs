using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dwarf_Level2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Enemy;
    static Animator anim;
    public Slider HPDwarf;
    public Slider HPELF;
    public bool carryon;
    void Start()
    {
        PauseInfo.RESUME = false;
        carryon = PauseInfo.RESUME;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        carryon = PauseInfo.RESUME;
        if (carryon)
        {
            if (HPELF.value <= 0)
            {
                anim.Play("idle1");
                this.enabled = false;
            }
            if (HPDwarf.value <= 0)
            {
                anim.Play("Death1");
                this.enabled = false;
            }
            else
            {
                //transform.LookAt(Enemy);
                Vector3 direction = Enemy.transform.position - this.transform.position;
                if (Vector3.Distance(Enemy.position, this.transform.position) <= 0.8)
                {
                    anim.Play("Attack2");
                    HPELF.value -= 1;
                }
                else
                {
                    anim.Play("Run");
                    this.transform.Translate(Vector3.forward * Time.deltaTime);
                }
            }
        }


    }
}
