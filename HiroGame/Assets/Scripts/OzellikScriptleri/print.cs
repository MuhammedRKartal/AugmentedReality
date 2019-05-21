using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class print:MonoBehaviour
{

    public void printbtton()
    {
        Debug.Log("Second scene");

        LoadInfo.LoadAllInfo();
        Debug.Log(GameInfo.Strenght);
    }
}
    
