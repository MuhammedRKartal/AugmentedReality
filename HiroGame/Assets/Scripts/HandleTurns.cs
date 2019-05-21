using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandleTurns
{
    public string Attacker; //Name of attacker.
    public string Type; //Type of attacker.
    public GameObject AttackersGameObject; //Who attacks.
    public GameObject AttackersTarget; //Who is going to be attacked.
    public GameObject AttackersTargetCube;


    //which attack is performed
    public BaseAttack choosenAttack; 
}
