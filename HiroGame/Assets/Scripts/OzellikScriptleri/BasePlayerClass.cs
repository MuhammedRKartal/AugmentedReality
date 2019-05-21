using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerClass 
{
    private string playerName;
    private int playerLevel;
    private BaseClass1 playerClass;
    private int strenght;
    private int vitality;
    private int intelligence;
    private int toughness;

    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }
    public int PlayerLevel
    {
        get { return playerLevel; }
        set { playerLevel = value; }
    }
    public BaseClass1 PlayerClass
    {
        get { return playerClass; }
        set { playerClass = value; }
    }


    public int Strenght
    {
        get { return strenght; }
        set { strenght = value; }
    } 
    public int Vitality
    {
        get { return vitality; }
        set { vitality = value; }
    }
    public int Intelligence
    {
        get { return intelligence; }
        set { intelligence = value; }
    }
    public int Toughness
    {
        get { return toughness; }
        set { toughness = value; }
    }
}