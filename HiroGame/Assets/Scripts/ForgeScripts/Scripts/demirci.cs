using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class demirci : MonoBehaviour
{
    //public GameObject[] buttonsList;
    public GameObject interactButton;
    public GameObject panel;
    public GameObject goldPanel;

    static bool x = false;
    static Animator anim;
    private int GoldAmount = 1;
    public int gold;


    public Text GoldText;


    // Start is called before the first frame update
    void Start()
    {
        /*foreach (GameObject button in buttonsList)
        {
            button.SetActive(false);
        }*/
        panel.SetActive(false);
        goldPanel.SetActive(false);
        interactButton.SetActive(true);
        anim = GetComponent<Animator>();

        gold = GameInfo.Gold;



    }

    // Update is called once per frame
    void Update()
    {
        if (x)
        {
            anim.Play("Stun");
            x = false;
        }


    }

    public void oynat()
    {
        x = true;
        showUpButtons();
    }

    public void showUpButtons()
    {
        GoldText.text = gold.ToString();
        goldPanel.SetActive(true);

        panel.SetActive(true);
        /*foreach(GameObject button in buttonsList){
            button.SetActive(true);
        }*/
        interactButton.SetActive(false);
    }
    public void GetVitality()

    {
        if (gold > 0)
        {
            gold -= GoldAmount;
            GoldText.text = gold.ToString();
            Debug.Log("before Vital" + GameInfo.Vitality);
            GameInfo.Vitality += 1;
            SaveInfo.SaveAllInfo();

            Debug.Log("after vital" + GameInfo.Vitality);
            GameInfo.Gold -= GoldAmount;

        }
    }
    public void GetStrenght()

    {
        if (gold > 0)
        {
            gold -= GoldAmount;
            GoldText.text = gold.ToString();
            Debug.Log("before strenght" + GameInfo.Strenght);
            GameInfo.Strenght += 1;
            SaveInfo.SaveAllInfo();
            Debug.Log("after strenght" + GameInfo.Strenght);
            GameInfo.Gold -= GoldAmount;

        }
    }
    public void GetToughness()

    {
        if (gold > 0)
        {
            gold -= GoldAmount;
            GoldText.text = gold.ToString();
            Debug.Log("before Toughness" + GameInfo.Toughness);
            GameInfo.Toughness += 1;
            SaveInfo.SaveAllInfo();
            Debug.Log("after Toughness" + GameInfo.Toughness);
            GameInfo.Gold -= GoldAmount;

        }
    }
    public void GetIntelligence()

    {
        if (gold > 0)
        {
            gold -= GoldAmount;
            GoldText.text = gold.ToString();
            Debug.Log("before Intelligence" + GameInfo.Intelligence);
            GameInfo.Intelligence += 1;
            SaveInfo.SaveAllInfo();
            Debug.Log("after Intelligence" + GameInfo.Intelligence);
            GameInfo.Gold -= GoldAmount;

        }
    }

    public void loadScene()
    {
        SceneManager.LoadScene("Level_01_(Elf)");
    }
}
