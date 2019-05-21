using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseHero : BaseClass
{
    public int strength;
    public int vitality;
    public int intelligence;
    public int toughness;

    public List<BaseAttack> Spells = new List<BaseAttack>();
    public List<GameObject> SpellsObj = new List<GameObject>();
}
