using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour {

    public Text dialogueText;
    public dialogue dialogue;

    // public Button ShowHerLetter; //show her the letter button
    // public Button ItsNotBusiness; //its none of your business button
    // public Button continueButton; //continue button


    private Queue<string> sentences; //this variable keeps track of all of our setences

    public playerprefs Prefs;

    // Use this for initialization
    void Start () {
       // triggerDialogue();
        
        sentences = new Queue<string>();
        StartDialogue(dialogue);

        // ShowHerLetter.gameObject.SetActive(false);
        // ItsNotBusiness.gameObject.SetActive(false);
        // continueButton.gameObject.SetActive(true);

        // ShowHerLetter = GameObject.FindGameObjectWithTag("LetterButton").GetComponent<Button>();
        // ItsNotBusiness = GameObject.FindGameObjectWithTag("BusinessButton").GetComponent<Button>();
        // continueButton = GameObject.FindGameObjectWithTag("ContinueButton").GetComponent<Button>();

    }

    public void StartDialogue(dialogue dialogue) //when scene starts
    {
        Prefs = FindObjectOfType<playerprefs>();
        Prefs.resetPref();
        Debug.Log("resetPref called");
        Debug.Log(PlayerPrefs.GetInt("Magicrocks"));
        // continueButton.gameObject.SetActive(true);
        sentences.Clear();
        Debug.Log("Starting conversation");
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 1)
        {
            // ShowHerLetter.gameObject.SetActive(true);
            // ItsNotBusiness.gameObject.SetActive(true);
            // continueButton.gameObject.SetActive(false);
            Debug.Log("Starting conversation");
        }
        else if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        // dialogueText.text = sentence;
        string sentence = sentences.Dequeue();
        // dialogueText.text = sentence;
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

   /* public void triggerDialogue()
    {
        Debug.Log("Initializing dialog");
        FindObjectOfType<dialogueManager>().StartDialogue(dialogue);
    } */

    void EndDialogue()
    {
        Debug.Log("End of Conversation");
    }

}
