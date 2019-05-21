using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack :BaseAttack
{
    public FireballAttack()
    {
        attackName = "FireBall";
        attackDamage = 25f;
        manaCost = 25f;
        spellNumber = 2;
    }
}
