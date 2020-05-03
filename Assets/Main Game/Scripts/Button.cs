using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

    public void PlayMyGame()
    {

        SceneManager.LoadScene("Level"); //when button is pressed load first area of game

    }

    public void ReplayBoss() {
        SceneManager.LoadScene("Boss");
    }
}
