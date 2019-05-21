using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Berserker_Level1 : MonoBehaviour
{
    public Transform Enemy;
    static Animator anim;
    public Slider HPBerserker;
    public Slider HPenemy;
    private float damage=10;
    private bool carryon;
    void Start()
    {
        anim = GetComponent<Animator>();
        LoadInfo.LoadAllInfo();
        HPBerserker.maxValue += GameInfo.Vitality;
        HPBerserker.value += GameInfo.Vitality*10;
        PauseInfo.RESUME = false;
        carryon = PauseInfo.RESUME;
        damage += GameInfo.Strenght / 3;


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

            if (HPBerserker.value <= 0)
            {
                anim.Play("Death");
                this.enabled = false;
            }
            else
            {
                //transform.LookAt(Enemy);
                Vector3 direction = Enemy.transform.position - this.transform.position;
                //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 2);
                if (Vector3.Distance(Enemy.position, this.transform.position) <= 2)
                {
                    anim.Play("Attack");
                    HPenemy.value -= damage;

                }
                else
                {

                    anim.Play("Walk");
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 3);
                }
            }
        }

    }
}
