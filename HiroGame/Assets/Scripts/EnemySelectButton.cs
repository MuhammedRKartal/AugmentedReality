using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject EnemyCube;

    public void SelectEnemy()
    {
        //Save input enemy prefab.
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input2(EnemyPrefab,EnemyCube);
    }
}
