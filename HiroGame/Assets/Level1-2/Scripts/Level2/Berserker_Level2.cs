using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Berserker_Level2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform hero;
    public Transform Enemy1;
    public Transform Enemy2;
    static Animator anim;
    public Slider HPBerserker;
    public Slider HPenemy;
    public Slider HPenemy2;
    private float damage;
    public bool carryon;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        LoadInfo.LoadAllInfo();

        HPBerserker.maxValue += GameInfo.Vitality;
        HPBerserker.value += GameInfo.Vitality;
        PauseInfo.RESUME = false;
        carryon = PauseInfo.RESUME;
        damage += GameInfo.Strenght*0.62f +1;

    }

    // Update is called once per frame
    void Update()
    {
        carryon = PauseInfo.RESUME;
        if (carryon)
        {
            if (HPenemy.value <= 0 && HPenemy2.value <= 0)
            {
                anim.Play("idle");
                this.enabled = false;
                string x = hero.name;
                string scenename = "YouWin" + "" + x;
                SceneManager.LoadScene(scenename);
            }
            if (HPBerserker.value <= 0)
            {
                anim.Play("Death");
                this.enabled = false;
                string x = hero.name;
                string scenename = "YouLose" + "" + x;
                SceneManager.LoadScene(scenename);
            }
            else
            {
                bool x = false;

                {
                    //transform.LookAt(Enemy1);
                    Vector3 direction = Enemy1.transform.position - this.transform.position;
                    if (Vector3.Distance(Enemy1.position, this.transform.position) <= 0.8)
                    {
                        anim.Play("Attack");
                        HPenemy.value -= damage;
                        x = true;
                    }
                }


                if (HPenemy2.value > 0)
                {
                    //transform.LookAt(Enemy2);
                    Vector3 direction2 = Enemy2.transform.position - this.transform.position;
                    if (Vector3.Distance(Enemy2.position, this.transform.position) <= 1)
                    {
                        anim.Play("Attack");
                        HPenemy2.value -= damage;
                        x = true;
                    }
                }
                if (!x)
                {
                    anim.Play("Walk");
                    this.transform.Translate(Vector3.forward * Time.smoothDeltaTime);
                }



            }
        }
    }






}