using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAttack : BaseAttack
{
    public SmokeAttack()
    {
        attackName = "Smoke";
        attackDamage = 20f;
        manaCost = 20f;
        spellNumber = 1;
    }
}
