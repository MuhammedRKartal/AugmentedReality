using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static bool isB = true;
    public static int levelCounter = 1;
    public static int transfergold = 0;
    public static bool isReward = false;
    private bool toforge = false;


    public void toInfoScene()
    {
        SceneManager.LoadScene("InfoScene");
    }

    public void toForgePage()
    {
        toforge = true;
        SceneManager.LoadScene("ForgePage");

    }
    public void toTheBar()
    {
        SceneManager.LoadScene("TheBar");
    }
    public void toTheCityWin()
    {
        if (levelCounter == 4)
        {
            SceneManager.LoadScene("EndScene");
        }
        else
        {
            isReward = true;
            transfergold = 20;
            levelCounter++;
            Debug.Log("Level : " + levelCounter);
            SceneManager.LoadScene("TheCity");
        }
    }
    public void toTheCityLost()
    {
        isReward = true;
        transfergold = 5;
        SceneManager.LoadScene("TheCity");

    }
    public void toTheCityFromforge()
    {
        if (GameInfo.Gold > 0)
        {

        }
        SaveInfo.SaveAllInfo();
        SceneManager.LoadScene("TheCity");

    }


    public void ArenaButton()
    {

        //level 1 
        if (isB && levelCounter == 1)
        {
            SceneManager.LoadScene("Scene1_Dialog_(B)");
        }
        else if (isB == false && levelCounter == 1)
        {
            SceneManager.LoadScene("Scene1_Dialog_(E)");
        }
        //level 2
        if (isB && levelCounter == 2)
        {
            SceneManager.LoadScene("Scene2_Dialog_(B)");
        }
        else if (isB == false && levelCounter == 2)
        {
            SceneManager.LoadScene("Scene2_Dialog_(E)");
        }
        //level 3
        if (isB && levelCounter == 3)
        {
            SceneManager.LoadScene("Scene3_Dialog_(B)");
        }
        else if (isB == false && levelCounter == 3)
        {
            SceneManager.LoadScene("Scene3_Dialog_(E)");
        }
        //level 4
        if (isB && levelCounter == 4)
        {
            SceneManager.LoadScene("Scene4_Dialog_(B)");
        }
        else if (isB == false && levelCounter == 4)
        {
            SceneManager.LoadScene("Scene4_Dialog_(E)");
        }

    }



}
