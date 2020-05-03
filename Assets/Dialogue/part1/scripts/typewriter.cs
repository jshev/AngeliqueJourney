using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this should be applied to scenes that only need a next Scene button

public class typewriter : MonoBehaviour
{

    public Text dialogueText;
    public dialogue dialogue;

    private Queue<string> sentences; //this variable keeps track of all of our setences

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
        StartDialogue(dialogue);
    }

    public void StartDialogue(dialogue dialogue) //when press start button
    {
        Debug.Log("Starting conversation");
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
       
      if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
    
        StopAllCoroutines(); //stops the typewriter if player clicks continuewhile it is still typing
        StartCoroutine(TypeSentence(sentence)); //calls TypeSentence
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) //converts to character string
        {
            dialogueText.text += letter;
            yield return null;
        }
    }


    void EndDialogue()
    {
        Debug.Log("End of Conversation");
    }
}
