using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy : BaseClass
{
    public List<BaseAttack> Spells = new List<BaseAttack>();
    public List<GameObject> SpellsObj = new List<GameObject>();
}
