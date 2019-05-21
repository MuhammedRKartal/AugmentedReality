using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable] 

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;


    [TextArea(3, 10)] //to expand the input area
    public string[] sentencesFromText;

    private Queue<string> sentences;

    //public Text nameText;
   
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

        StartDialogue(sentencesFromText);  // Trigger to start dialog
    }

    public void StartDialogue(string[] sentences1) 
    {
        // Debug.Log("Start : " + dialogue.name);

        //nameText.text = dialogue.name;

        //nameText.test = dialogue.name;
        sentences.Clear();

        foreach (string sentence in sentences1)
        {

            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence() // displaying sentences one by one
    {

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //dialogueText.text = sentence;

        StopAllCoroutines();
        StartCoroutine(TypeSentences(sentence));
    }

    IEnumerator TypeSentences(string sentence) //for the collection for every char
    {
        dialogueText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

    }


    void EndDialogue() // to understand dialog is working or not
    {
        Debug.Log("End of conver.");


        //level 1 
        if (SceneChanger.isB && SceneChanger.levelCounter == 1)
        {
            SceneManager.LoadScene("Level_01_(B)");// for berzerker
        }
        else if (SceneChanger.isB == false && SceneChanger.levelCounter == 1)
        {
            SceneManager.LoadScene("Level_01_(Elf)");// for elf
        }
        //level 2
        if (SceneChanger.isB && SceneChanger.levelCounter == 2)
        {
            SceneManager.LoadScene("Level_02_Arena(B)");// for berzerker
        }
        else if (SceneChanger.isB == false && SceneChanger.levelCounter == 2)
        {
            SceneManager.LoadScene("Level_02_Arena"); // for elf
        }
        //level 3
        if (SceneChanger.isB && SceneChanger.levelCounter == 3)
        {
            SceneManager.LoadScene("Level3Berserker");// for berzerker
        }
        else if (SceneChanger.isB == false && SceneChanger.levelCounter == 3)
        {
            SceneManager.LoadScene("Level3ElfMage");// for elf
        }
        //level 4

        if (SceneChanger.isB && SceneChanger.levelCounter == 4)
        {
            SceneManager.LoadScene("Level4Berserker");// for berzerker
        }
        else if (SceneChanger.isB == false && SceneChanger.levelCounter == 4)
        {
            SceneManager.LoadScene("Level4ElfMage"); // for elf
        }

    }

    
}
