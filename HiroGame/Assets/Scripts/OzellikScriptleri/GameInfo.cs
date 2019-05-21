using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }
    public static string PlayerName { get; set; }
    public static int PlayerLevel { get; set; }
    public static BaseClass1 PlayerClass { get; set; }

    public static int Strenght { get; set; }
    public static int Vitality { get; set; }
    public static int Intelligence { get; set; }
    public static int Toughness { get; set; }
    public static int Gold { get; set; }



}
