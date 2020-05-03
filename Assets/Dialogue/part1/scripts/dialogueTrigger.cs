using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dialogueTrigger : MonoBehaviour {

    public playerprefs Prefs;
    Scene m_Scene;
    string sceneName;


    private void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        if (sceneName == "dialogue1") //this resets the score to zero from the first narrative scene
        {
            Prefs = FindObjectOfType<playerprefs>();
            Prefs.resetPref();
            Debug.Log("resetPref called");
            Debug.Log(PlayerPrefs.GetInt("Magicrocks"));
        }
    }
   
}
