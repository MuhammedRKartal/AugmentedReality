using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : BaseAttack
{
    public MeleeAttack()
    {
        attackName = "Melee";
        attackDamage = 10f;
        manaCost = 0;
        spellNumber = 999;
    }
}
