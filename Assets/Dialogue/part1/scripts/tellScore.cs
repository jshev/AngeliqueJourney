using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tellScore : MonoBehaviour {

    public Text rockScore;
    int previousScore;

    // Use this for initialization
    void Start () {

        previousScore = PlayerPrefs.GetInt("Magicrocks", 0);

        rockScore.text = previousScore.ToString(); 

        //previous scoreis the variable you should use to implement the number of rocks
    }
}
