using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarControl : MonoBehaviour
{

    public Sprite[] sprites;
    public GameObject Player;
    private SpriteRenderer render;

    public int health;
    public bool gotHealth;

    // Use this for initialization
    void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();
        health = 6;
        gotHealth = false;
        //Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (health >= 1)
        {
            gotHealth = true;
        }
        else
        {
            gotHealth = false;
        }
    }

    public void updateHealthBar()
    {
        health -= 1;
        render.sprite = sprites[health - 1];
    }

    public void addHealth()
    {
        health += 1;
        render.sprite = sprites[health - 1];
    }
}
