using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Skeleton_Level1 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform dusman;
    static Animator anim;
    public Slider HPSkeleton;
    public Slider HPenemy;
    public bool carryon;

    void Start()
    {
        anim = GetComponent<Animator>();
        PauseInfo.RESUME = false;
        carryon = PauseInfo.RESUME;
    }

    // Update is called once per frame
    void Update()
    {
        carryon = PauseInfo.RESUME;
        if (carryon)
        {
            if (HPenemy.value <= 0)
            {
                anim.Play("idle1");
                this.enabled = false;
            }

            if (HPSkeleton.value <= 0)
            {
                anim.Play("Death1");
                this.enabled = false;
                string x = dusman.name;
                string scenename = "YouWin" + "" + x;
                SceneManager.LoadScene(scenename);
            }
            else
            {
                //transform.LookAt(dusman);
                Vector3 direction = dusman.transform.position - this.transform.position;
                //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 2);
                if (Vector3.Distance(dusman.position, this.transform.position) <= 1.2f)
                {
                    anim.Play("Attack2");
                    HPenemy.value -= 1f;

                }
                else
                {
                    anim.Play("Run");
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 1);
                }
            }
        }
    }


}