using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBarControl : MonoBehaviour {

	public Sprite[] sprites;
	public GameObject Player;
	private SpriteRenderer render;

	public int ammo;
	public bool gotAmmo;

	// Use this for initialization
	void Start () {
		render = gameObject.GetComponent<SpriteRenderer>();
		ammo = PlayerPrefs.GetInt("Magicrocks", 0);
        setAmmoBar(ammo);
        gotAmmo = false;
		Player = GameObject.FindWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
		if (ammo >= 1) {
			gotAmmo = true;
		} else {
			gotAmmo = false;
		}
	}

	public void updateAmmoBar() {
		ammo -= 1;
		render.sprite = sprites[ammo];
	}

    public void setAmmoBar(int amount)
    {
        ammo = amount;
        render.sprite = sprites[ammo];
    }

    public void addAmmo() {
		ammo += 1;
		render.sprite = sprites[ammo];
	}
}
