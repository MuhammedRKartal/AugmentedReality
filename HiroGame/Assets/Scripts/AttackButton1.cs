using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton1 : MonoBehaviour
{
    public BaseAttack spellAttackToPerform;
    //public int spellIndexToPerform;

    public void CastSpellAttack()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input4(spellAttackToPerform);

    }
}
