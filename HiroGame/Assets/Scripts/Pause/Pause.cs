using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public  GameObject menu;
    public Text text1;

    private void Start()
    {
        menu.SetActive(true);

        
    }

    // Update is called once per frame

    public   void PauseGame()
    {
        PauseInfo.RESUME = false;
        Time.timeScale = 0;
        menu.SetActive(true);
       
    }
    public  void ResumeGame()
    {
        PauseInfo.RESUME = true;
        Time.timeScale = 1;
        menu.SetActive(false);
        text1.text = "Resume";

    }
}
