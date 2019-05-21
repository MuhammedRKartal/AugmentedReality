using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{

    private GameObject[] characterList; //Create gameobject array for characters.
    private int index; //Defaultly it is equal to 0

    private bool mageSelected = false;

    // Start is called before the first frame update
    private void Start()
    {
        //Fill the gameobject array with size of characters. 
        //transform.childCount means number of child objects inside of our transform.
        //our transform will be an array.
        characterList = new GameObject[transform.childCount];

        //Adding all of the transform object elements to our gameobject array.
        for (int i= 0; i<transform.childCount; i++)
            characterList[i] = transform.GetChild(i).gameObject; 

        //At the beginning of screen making the characters invisible.
        foreach(GameObject go in characterList)
            go.SetActive(false);
        
        
    }

    public void ToggleLeft()
    {
        
        characterList[index].SetActive(false);

        index--;
        if(index < 0)
        {
            index = characterList.Length - 1;
        }

        characterList[index].SetActive(true);
    }

    public void ToggleRight()
    {
        characterList[index].SetActive(false);

        index++;
        if (index == characterList.Length)
        {
            index = 0;
        }

        characterList[index].SetActive(true);
    }

    public void ToggleConfrim()
    {
        if (index == 1)
            SceneManager.LoadScene("BerserkerGeneralPage");
        else
            SceneManager.LoadScene("MageGeneralPage");
    }
}
