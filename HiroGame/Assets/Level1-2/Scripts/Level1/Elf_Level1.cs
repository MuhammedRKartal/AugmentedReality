using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elf_Level1 : MonoBehaviour
{
    public Transform Enemy;
    static Animator anim;
    public Slider HPElf;
    public Slider HPenemy;
    private float damage;
    private bool carryon;
    void Start()
    {
        anim = GetComponent<Animator>();
        LoadInfo.LoadAllInfo();

        HPElf.maxValue += GameInfo.Vitality;
        HPElf.value += GameInfo.Vitality;
        PauseInfo.RESUME = false;
        carryon = PauseInfo.RESUME;
        damage += GameInfo.Strenght / 2;


    }

    // Update is called once per frame
    void Update()
    {
        carryon = PauseInfo.RESUME;
        if (carryon)
        {
            if (HPenemy.value <= 0)
            {
                anim.Play("idle");
                this.enabled = false;
            }

            if (HPElf.value <= 0)
            {
                anim.Play("Death");
                this.enabled = false;
            }
            else
            {
                //transform.LookAt(Enemy);
                Vector3 direction = Enemy.transform.position - this.transform.position;
                //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 2);
                if (Vector3.Distance(Enemy.position, this.transform.position) <= 0.6f)
                {
                    anim.Play("Attack1");
                    HPenemy.value -= damage+20;

                }
                else
                {
                    Debug.Log("in game value" + HPElf.value);

                    anim.Play("Walk");
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 1);
                }
            }
        }
    }


}
