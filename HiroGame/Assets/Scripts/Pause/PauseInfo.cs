using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseInfo : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }
    public static bool RESUME { get; set; }
}
