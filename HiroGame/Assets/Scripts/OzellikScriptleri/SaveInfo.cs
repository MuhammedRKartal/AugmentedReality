using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInfo 
{
    public static void SaveAllInfo()
    {
        PlayerPrefs.SetString("PLAYERNAME", GameInfo.PlayerName);
        PlayerPrefs.SetInt("PLAYERLEVEL", GameInfo.PlayerLevel);
        //
        PlayerPrefs.SetInt("STRENGHT", GameInfo.Strenght);
        PlayerPrefs.SetInt("VITALITY", GameInfo.Vitality);
        PlayerPrefs.SetInt("INTELLIGENCE", GameInfo.Intelligence);
        PlayerPrefs.SetInt("TOUGHNESS", GameInfo.Toughness);
        PlayerPrefs.SetInt("GOLD", GameInfo.Gold);
    }
}
   

