using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class dialogueManager2 : MonoBehaviour {

	public Text dialogueText;

	//public Button startbutton; 

//	public Button continueButton;

	private Queue<string> sentences;

	public GameObject normalsprite;

	public GameObject scarysprite;

    public dialogue2 dialogue;






    // Use this for initialization
    void Start () {
		sentences = new Queue<string> ();
	//	startbutton.gameObject.SetActive (true);
	//	continueButton.gameObject.SetActive (false);
		scarysprite.gameObject.SetActive (false);
		normalsprite.gameObject.SetActive (false);
        StartDialogue(dialogue);
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
		if (sentences.Count == 0) 
		{
			EndDialogue ();
			return;
		}
		string sentence = sentences.Dequeue ();
		StopAllCoroutines (); //stops the typewriter if player clicks continuewhile it is still typing
		StartCoroutine (TypeSentence (sentence)); //calls TypeSentence

		//I want to change the sprite to angry / or normal boss on certain lines
		//if scenename = this, do this
		if (sentences.Count % 2 != 0) { //testing for odd numbers, when the boss is speaking,  if the remainder when dividing by 2 is not 0
			
			dialogueText.color = Color.red;
		}//boss text color will be red

			
		if (sentences.Count % 2 == 0) {
				dialogueText.color = Color.black;
				if (sentences.Count == 34) { //the first time crazy lady appears, it is scary sprite
					scarysprite.gameObject.SetActive (true);
				} else if (sentences.Count == 32) { //then she goes back to normal
				normalsprite.gameObject.SetActive (true);	//this isnt being called
				scarysprite.gameObject.SetActive (false);
					
				} else if (sentences.Count == 16) { //then she goes crazy again for the rest of the scene
					scarysprite.gameObject.SetActive (true);
					normalsprite.gameObject.SetActive (false);
				}	else if (sentences.Count == 0) 
					{
						dialogueText.color = Color.blue;
					}

			}

		}

	
	

	//typewriter text

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
        Invoke("LoadBoss", 3);
	}

    void LoadBoss() {
        SceneManager.LoadScene("Boss");
    }
}
