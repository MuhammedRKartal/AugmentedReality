using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CreatePlayer : MonoBehaviour
{
    private BasePlayerClass newPlayer;
    private string PlayerName = "P";

    //UI
    public Text strenghtText;
    public Text vitalityText;
    public Text intelligenceText;
    public Text toughnessText;
    private int pointsToSpend=0;
    public Text pointsText;
    public GameObject[] characters;

    void Start()
    {
        foreach(GameObject character in characters)
        {
            character.SetActive(false);
        }
        newPlayer = new BasePlayerClass();
        updateUI();
    }

    public void CreateNewPlayer()
    {
        newPlayer.PlayerLevel = 1;
        newPlayer.PlayerName = PlayerName;
        GameInfo.PlayerLevel = newPlayer.PlayerLevel;
        GameInfo.PlayerName = newPlayer.PlayerName;
        GameInfo.PlayerClass = newPlayer.PlayerClass;

        GameInfo.Strenght = newPlayer.Strenght;
        GameInfo.Intelligence = newPlayer.Intelligence;
        GameInfo.Vitality = newPlayer.Vitality;
        GameInfo.Toughness = newPlayer.Toughness;


        SaveInfo.SaveAllInfo();
        //SceneManager.LoadScene("TestScene");

    }
    public void LoadStuff()
    {
        LoadInfo.LoadAllInfo();
        if(GameInfo.PlayerClass != null)
        {
            SceneManager.LoadScene("TheCity");
           
        }  
    }


    public void SetElfClass()
    {
        Animator x = characters[0].GetComponent<Animator>();
        characters[1].SetActive(false);
        characters[0].SetActive(true);
        x.Play("Spawn");
        pointsToSpend = 30;
        newPlayer.PlayerClass = new BaseElfClass();
        newPlayer.Strenght = newPlayer.PlayerClass.Strenght;
        newPlayer.Vitality = newPlayer.PlayerClass.Vitality;
        newPlayer.Intelligence = newPlayer.PlayerClass.Intelligence;
        newPlayer.Toughness = newPlayer.PlayerClass.Toughness;
        //Update UI
        SceneChanger.isB = false;
        updateUI();
        
    }
    public void SetBerserkerClass()
    {
        Animator x = characters[1].GetComponent<Animator>();
        characters[0].SetActive(false);
        characters[1].SetActive(true);
        x.Play("Die");
        pointsToSpend = 30;
        newPlayer.PlayerClass = new BaseBerserkerClass();
        newPlayer.Strenght = newPlayer.PlayerClass.Strenght;
        newPlayer.Vitality = newPlayer.PlayerClass.Vitality;
        newPlayer.Intelligence = newPlayer.PlayerClass.Intelligence;
        newPlayer.Toughness = newPlayer.PlayerClass.Toughness;
        //Update UI
        SceneChanger.isB = true;
        updateUI();
        
    }
    void updateUI()
    {
        strenghtText.text = newPlayer.Strenght.ToString();
        intelligenceText.text= newPlayer.Intelligence.ToString();
        vitalityText.text = newPlayer.Vitality.ToString();
        toughnessText.text = newPlayer.Toughness.ToString();
        pointsText.text = pointsToSpend.ToString();
    }



    public void SetStrenght(int amount)
    {
        if (newPlayer.PlayerClass != null)
        {
            if (amount > 0 && pointsToSpend > 0)
            {
                newPlayer.Strenght += amount;
                pointsToSpend--;
                updateUI();
            }
            else if (amount < 0 && newPlayer.Strenght > newPlayer.PlayerClass.Strenght)
            {
                newPlayer.Strenght += amount;
                pointsToSpend++;
                updateUI();
            }
            else
            {
                Debug.Log("No class Chosen");
            }
        }
    }
    public void SetVitality(int amount)
    {
        if (newPlayer.PlayerClass != null)
        {
            if (amount > 0 && pointsToSpend > 0)
            {
                newPlayer.Vitality += amount;
                pointsToSpend--;
                updateUI();
            }
            else if (amount < 0 && newPlayer.Vitality > newPlayer.PlayerClass.Vitality)
            {
                newPlayer.Vitality += amount;
                pointsToSpend++;
                updateUI();
            }
            else
            {
                Debug.Log("No class Chosen");
            }
        }
    }
    public void SetIntelligence(int amount)
    {
        if (newPlayer.PlayerClass != null)
        {
            if (amount > 0 && pointsToSpend > 0)
            {
                newPlayer.Intelligence += amount;
                pointsToSpend--;
                updateUI();
            }
            else if (amount < 0 && newPlayer.Intelligence > newPlayer.PlayerClass.Intelligence)
            {
                newPlayer.Intelligence += amount;
                pointsToSpend++;
                updateUI();
            }
            else
            {
                Debug.Log("No class Chosen");
            }
        }
    }
    public void SetToughness(int amount)
    {
        if (newPlayer.PlayerClass != null)
        {
            if (amount > 0 && pointsToSpend > 0)
            {
                newPlayer.Toughness += amount;
                pointsToSpend--;
                updateUI();
            }
            else if (amount < 0 && newPlayer.Toughness > newPlayer.PlayerClass.Toughness)
            {
                newPlayer.Toughness += amount;
                pointsToSpend++;
                updateUI();
            }
            else
            {
                Debug.Log("No class Chosen");
            }
        }
    }




}
