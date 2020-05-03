using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Designed and Written by Kevin Maeder
// Anything Involving Animations and Sprites Written by Drew Williams
// Everything Combat related is by Julianna Shevchenko

// Edited for Different Project by Julianna Shevchenko


public class PlayerMovement : MonoBehaviour {

	//Movement Related
	float VelX;
	float VelY;
	public int jumpCharge;
	int timer;
	int timerMax;
	float timerMult;
	public int jumpMax;
	float MaxY;
	float MaxX;

	//Combat Related
	private AmmoBarControl ammoControl;
	public GameObject rockRight, rockLeft;
	private Vector2 rockPos;
	public float fireRate = 0.5f;
	private float fireNext = 0f;
	public bool gotStake;
	public GameObject stakeDisplay;

	//Animation Related
	//public Animator myAnimator;
	float speed;
	public SpriteRenderer mySpriteRenderer;

	void Start () {
		//Movement
		VelX = 0;
		VelY = 0;
		MaxX = 10;
		timerMax = 15;
		timerMult = 2f;
		jumpCharge = 1;
		jumpMax = 1;
		MaxY = 17;

		//Animation
		mySpriteRenderer = GetComponent<SpriteRenderer>();

		//Combat
		ammoControl = FindObjectOfType<AmmoBarControl>();
		gotStake = false;
	}

	void FixedUpdate() {
		// Animation Block
		speed = GetComponent<Rigidbody2D>().velocity.x;
		var roundedSpeed = (Mathf.Round (speed * 100)) / 100.0;

		// moving is false when the speed is 0, and the sprite flips depending on the direction of the speed
		if (roundedSpeed > 0) {
			mySpriteRenderer.flipX = false;
		} if (roundedSpeed < 0) {
			mySpriteRenderer.flipX = true;
		} if (roundedSpeed == 0) {
			//Debug.Log("noFlip");
		}
		// End Animation Block

		// Movement Block
		// increase VelX to go right while D is pressed up to a max
		if (Input.GetKey(KeyCode.RightArrow) && VelX <= MaxX) {
			VelX += 3;
		}

		// decrease VelX to go left while A is pressed up to a max
		if (Input.GetKey(KeyCode.LeftArrow) && VelX >= -MaxX) {
			VelX -= 3;
		}

		// if neither right or left are pressed, VelX should decrease towards 0
		if (!(Input.GetKey(KeyCode.RightArrow)) && !(Input.GetKey(KeyCode.LeftArrow))) {
			if (VelX > 0) {
				VelX--;
			} else if (VelX < 0) {
				VelX++;
			}
		}

		// when space is pressed, seperated for double jump
		if (Input.GetKeyDown(KeyCode.Space)) {
			// if player is allowed to jump, set frames of VelY increase possible
			// then make player unable to jump again
			if (jumpCharge > 0) {
				timer = timerMax;
				jumpCharge--;
				VelY = 3;
			}
		}

		// increase VelY while Space is pressed to simulate jump
		if (Input.GetKey(KeyCode.Space)) {
			// if still in set frames
			if (timer > 1) {
				// and velocity is below max, go up
				if (VelY <= MaxY) {
					VelY += timer / timerMult;
				}
			}
			timer--;
		} if (Input.GetKey(KeyCode.Space) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))) {
			// if still in set frames
			if (timer > 1) {
				// and velocity is below max, go up
				if (VelY <= MaxY) {
					VelY += timer / timerMult;
				}
			}
			timer--;
		}

		// basically gravity
		if (VelY > -15) {
			VelY--;
		}

		// no hax plz
		if (jumpCharge > jumpMax) {
			jumpCharge--;
		}

		// at the end of it all, change your velocity
		GetComponent<Rigidbody2D>().velocity = new Vector2(VelX, VelY);

		// End Movement Block

		// Combat Block

		if (Input.GetKeyDown(KeyCode.S) && (Time.time > fireNext) && ammoControl.gotAmmo) {
			ammoControl.updateAmmoBar();
			fireNext = Time.time + fireRate;
			fire();
		}
		// End Combat Block
	}

	// Interactions Block
	void OnCollisionEnter2D(Collision2D collide) {
		if (collide.gameObject.tag == "ground") {
			jumpCharge = 1;
		}
		if (collide.gameObject.tag == "stake") {
			Destroy (collide.gameObject);
			gotStake = true;
			stakeDisplay.SetActive (true);
		}
	}
	// End of Interactions Block

	public void fire() {
		// used in Combat Block
		rockPos = transform.position;
		if (mySpriteRenderer.flipX) {
			//left
			rockPos += new Vector2(-1.5f, +0.25f);
			Instantiate(rockLeft, rockPos, Quaternion.identity);
		} else {
			//right
			rockPos += new Vector2(+1.5f, +0.25f);
			Instantiate(rockRight, rockPos, Quaternion.identity);
		}

	}

}