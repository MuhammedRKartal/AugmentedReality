using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue
{

    //public string name;

    //that decides border of area which has lines (min,max)
    [TextArea(3, 10)]

    public string[] sentences;

}
