using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInfo
{
   public static void LoadAllInfo()
    {
        GameInfo.PlayerName = PlayerPrefs.GetString("PLAYERNAME");
        GameInfo.PlayerLevel = PlayerPrefs.GetInt("PLAYERLEVEL");
        //

        GameInfo.Strenght = PlayerPrefs.GetInt("STRENGHT");
        GameInfo.Vitality = PlayerPrefs.GetInt("VITALITY");
        GameInfo.Intelligence = PlayerPrefs.GetInt("INTELLIGENCE");
        GameInfo.Toughness = PlayerPrefs.GetInt("TOUGHNESS");
        GameInfo.Gold = PlayerPrefs.GetInt("GOLD");

        Debug.Log(GameInfo.Strenght);
        Debug.Log("Info Loaded");
    } 
}
