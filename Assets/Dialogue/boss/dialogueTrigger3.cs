﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger3 : MonoBehaviour {

	public dialogue2 dialogue;


	public void triggerDialogue(){
		Debug.Log ("Initializing Dialogue");
		FindObjectOfType<dialogueManager3> ().StartDialogue (dialogue);
	}
}
