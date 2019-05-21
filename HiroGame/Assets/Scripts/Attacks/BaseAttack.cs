using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseAttack: MonoBehaviour
{
    public string attackName;
    public float attackDamage;
    public float manaCost;

    public int spellNumber;
}
