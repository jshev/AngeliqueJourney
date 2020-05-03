using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager3 : MonoBehaviour {

	public Text dialogueText;

    public dialogue2 dialogue;

    //public Button startbutton; 

    //public Button continueButton;

    //public Button backToMenu; // button to get back to the menu after the scene ends

    private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string> ();
        StartDialogue(dialogue);
        //startbutton.gameObject.SetActive (true);
        //continueButton.gameObject.SetActive (false);
        //backToMenu.gameObject.SetActive (false); //setting menu button to false
    }

	public void StartDialogue(dialogue2 dialogue) //called when start button is clicked
	{
		Debug.Log ("Starting conversation");
	//	startbutton.gameObject.SetActive (false);
	//	continueButton.gameObject.SetActive(true);
		sentences.Clear();
		foreach(string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		DisplayNextSentence();
	}


	public void DisplayNextSentence() //this is called when the player hits the continue button
	{
		Debug.Log (sentences.Count);
		if (sentences.Count == 0) {
			EndDialogue ();
			return;
		}
		else if (sentences.Count == 1) 
		{
			dialogueText.color = Color.red;
			//backToMenu.gameObject.SetActive (true); //setting menu button to true
		}

		string sentence = sentences.Dequeue ();
		StopAllCoroutines (); //stops the typewriter if player clicks continuewhile it is still typing
		StartCoroutine (TypeSentence (sentence)); //calls TypeSentence

	}

	IEnumerator TypeSentence( string sentence)
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
		Debug.Log ("End of Dialogue");
	}
	

}
