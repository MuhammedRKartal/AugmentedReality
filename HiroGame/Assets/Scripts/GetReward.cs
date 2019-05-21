using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetReward : MonoBehaviour
{
    private void Start()
    {
        LoadInfo.LoadAllInfo();
    }
    public void getReward()
    {
        if (SceneChanger.isReward == true)
        {

            GameInfo.Gold += SceneChanger.transfergold;
            SceneChanger.isReward = false;
        }
        SaveInfo.SaveAllInfo();
        Debug.Log(GameInfo.Gold);
    }
}