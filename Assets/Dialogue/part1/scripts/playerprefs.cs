using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerprefs : MonoBehaviour {

    public int magicRocksCount;


    public void Dialogue1(string dialogue1) //start button on menu loads dialogue scene
    {
        SceneManager.LoadScene(dialogue1);
    }

    public void resetPref() //this is called in dialogue trigger, when the player clicks the start button in dialogue 1
    {
        magicRocksCount = 0;
        PlayerPrefs.SetInt("Magicrocks", magicRocksCount); //score starts as zero
    }

    public void Itsnoneofyourbusiness(string itsnoneofyourbusiness) //it's none of your business button loads that scene
    {
        SceneManager.LoadScene(itsnoneofyourbusiness);
        magicRocksCount += 2; //add two rocks
        PlayerPrefs.SetInt("Magicrocks", magicRocksCount); //get the score
        Debug.Log(magicRocksCount);
      
    }

    public void showhertheletter(string showhertheletter) //show her the letter scene loads the appropiate scene
    { 
        SceneManager.LoadScene(showhertheletter);

    }

    public void Ilikecausingtrouble(string ilikecausingtrouble) //ilikecausingtrouble button loads that scene from letter
    {
        SceneManager.LoadScene(ilikecausingtrouble);
    }

    public void Ineedtoknow(string ineedtoknow) //i need to know button loads that scene from letter
    {
        SceneManager.LoadScene(ineedtoknow);
    }


    public void Sunking(string sunking) //sunking button loads appropiate scene
    {
        SceneManager.LoadScene(sunking);
        magicRocksCount = PlayerPrefs.GetInt("Magicrocks");
        magicRocksCount += 4;
        PlayerPrefs.SetInt("Magicrocks", magicRocksCount); //get the score
        Debug.Log(magicRocksCount);
        //rocks = 5
    }

    public void Awayout(string awayout) //a way out button loads appropiate scene
    {
        SceneManager.LoadScene(awayout);
        magicRocksCount = PlayerPrefs.GetInt("Magicrocks");
        magicRocksCount += 6;
        PlayerPrefs.SetInt("Magicrocks", magicRocksCount); //get the score
        Debug.Log(magicRocksCount);
        //rocks = 6
    }

    public void Revenge(string revenge) //revenge button loads appropiate scene
    {
        SceneManager.LoadScene(revenge);
        magicRocksCount = PlayerPrefs.GetInt("Magicrocks");
        magicRocksCount += 5;
        PlayerPrefs.SetInt("Magicrocks", magicRocksCount); //get the score
        Debug.Log(magicRocksCount);
        //rocks = 4
    }

    public void Shouldnthavefallenasleep(string fallenasleep) //show her the letter scene loads the appropiate scene
    { //adds no rocks
        //rocks = 2
        SceneManager.LoadScene(fallenasleep);
        magicRocksCount = PlayerPrefs.GetInt("Magicrocks");
        magicRocksCount += 3;
        PlayerPrefs.SetInt("Magicrocks", magicRocksCount); //get the score
        Debug.Log(magicRocksCount);
    }

    public void Themagicrocks(string themagicrocks) //continue button loads themagicrocks scene
    {
        SceneManager.LoadScene(themagicrocks);
        magicRocksCount = PlayerPrefs.GetInt("Magicrocks");
        Debug.Log(magicRocksCount);
    }

}
