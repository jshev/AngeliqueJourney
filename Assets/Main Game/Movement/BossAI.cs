using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {

	private GameObject Player;
	private PlayerMovement playMove;
	private Vector2 defaultPosition;

	//public Animator myAnimator;
	public float movementSpeed;
	public float kamikazeRun;
	public bool moving;
	public SpriteRenderer mySpriteRenderer;
	public float pathSize;

	void Start() {
		// localPosition of the position of object when game first starts
		defaultPosition = transform.localPosition;
		movementSpeed = 0.05f;
		kamikazeRun = 0.15f;

		// moving is always true
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		moving = true;

		Player = GameObject.FindWithTag ("Player");
		playMove = FindObjectOfType<PlayerMovement>();
	}

	void FixedUpdate() {
		// flip sprite and change direction if enemy reaches end of its range
		if ((Player.transform.position.x >= (transform.position.x - 10)) && (Player.transform.position.x <= (transform.position.x + 10))) {
			if (Player.transform.position.x > transform.position.x) {
				mySpriteRenderer.flipX = false;
				Vector2 newPos = new Vector2(transform.position.x + kamikazeRun, defaultPosition.y);
				transform.position = newPos;
			}
			if (Player.transform.position.x < transform.position.x) {
				mySpriteRenderer.flipX = true;
				Vector2 newPos = new Vector2(transform.position.x - kamikazeRun, defaultPosition.y);
				transform.position = newPos;
			}
		}
		}

	void OnCollisionEnter2D(Collision2D collide) {
		if (collide.gameObject.tag == "Player") {
			if (playMove.gotStake) {
				gameObject.SetActive (false);
				playMove.stakeDisplay.SetActive (false);
				Debug.Log ("WINNER WINNER!");
			} else {
				Debug.Log ("You got... I don't have a catchy phrase for this.");
		
			}
		}
	}
}
