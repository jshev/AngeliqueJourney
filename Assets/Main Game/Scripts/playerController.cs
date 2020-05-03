using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public float jumpForce; //sets up a variable for how high our player jumps
    private Collider2D myCollider;
    private Rigidbody2D myRigidbody;
    public LayerMask WhatIsGround; //creates a LayerMask for us to attach what layer is specifically ground
    public bool grounded; //creates a true or false statment for grounded variable

    // Movement variables
    float VelX;
    float VelY;
    public int jumpCharge;
    int timer;
    int timerMax;
    float timerMult;
    public int jumpMax;
    float MaxY;
    float MaxX;

    // Combat variables
    private AmmoBarControl ammoControl;
    public GameObject rockRight, rockLeft;
    private Vector2 rockPos;
    public float fireRate = 0.5f;
    private float fireNext = 0f;
    public bool gotStake;
    public GameObject stakeDisplay;
    public SpriteRenderer mySpriteRenderer;
    public float speed;
    private HealthBarControl healthControl;
    private int bossHealth;
    public GameObject stake2;
    public GameObject stake3;
    public GameObject stake4;

    // Other variables
    public Text noDoorText;
    public Text noKeyText;
    Scene m_Scene;
    string sceneName;

    // Use this for initialization
    void Start()
    {
        VelX = 0;
        VelY = 0;
        MaxX = 6;
        timerMax = 15;
        timerMult = 2f;
        jumpCharge = 1;
        jumpMax = 1;
        MaxY = 10;

        myCollider = GetComponent<Collider2D>(); //sets a variable (myCollider) to the objects Collider
                                                 //so we do not have to keep getting the component below
        myRigidbody = GetComponent<Rigidbody2D>();//sets a variable (myRigidbody) to the objects Rigidbody
                                                  //so we do not have to keep getting the component below

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        ammoControl = FindObjectOfType<AmmoBarControl>();
        healthControl = FindObjectOfType<HealthBarControl>();
        gotStake = false;
        bossHealth = 4;

        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;

    }

    // Update is called once per frame
    void Update()
    {
        // Movement Block
        speed = GetComponent<Rigidbody2D>().velocity.x;
        var roundedSpeed = (Mathf.Round(speed * 100)) / 100.0;

        // moving is false when the speed is 0, and the sprite flips depending on the direction of the speed
        if (roundedSpeed > 0)
        {
            mySpriteRenderer.flipX = false;
        }
        if (roundedSpeed < 0)
        {
            mySpriteRenderer.flipX = true;
        }
        if (roundedSpeed == 0)
        {
            //Debug.Log("noFlip");
        }
        // End Animation Block

        // Movement Block
        // increase VelX to go right while D is pressed up to a max
        if (Input.GetKey(KeyCode.RightArrow) && VelX <= MaxX)
        {
            VelX += 2;
        }

        // decrease VelX to go left while A is pressed up to a max
        if (Input.GetKey(KeyCode.LeftArrow) && VelX >= -MaxX)
        {
            VelX -= 2;
        }

        // if neither right or left are pressed, VelX should decrease towards 0
        if (!(Input.GetKey(KeyCode.RightArrow)) && !(Input.GetKey(KeyCode.LeftArrow)))
        {
            if (VelX > 0)
            {
                VelX--;
            }
            else if (VelX < 0)
            {
                VelX++;
            }
        }

        // when space is pressed, seperated for double jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // if player is allowed to jump, set frames of VelY increase possible
            // then make player unable to jump again
            if (jumpCharge > 0)
            {
                timer = timerMax;
                jumpCharge--;
                VelY = 3;
            }
        }

        // increase VelY while Space is pressed to simulate jump
        if (Input.GetKey(KeyCode.Space))
        {
            // if still in set frames
            if (timer > 1)
            {
                // and velocity is below max, go up
                if (VelY <= MaxY)
                {
                    VelY += timer / timerMult;
                }
            }
            timer--;
        }
        if (Input.GetKey(KeyCode.Space) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            // if still in set frames
            if (timer > 1)
            {
                // and velocity is below max, go up
                if (VelY <= MaxY)
                {
                    VelY += timer / timerMult;
                }
            }
            timer--;
        }

        // basically gravity
        if (VelY > -15)
        {
            VelY--;
        }

        // no hax plz
        if (jumpCharge > jumpMax)
        {
            jumpCharge--;
        }

        // at the end of it all, change your velocity
        GetComponent<Rigidbody2D>().velocity = new Vector2(VelX, VelY);

        // End of Movement Block

        // determine whether player is grounded
        grounded = Physics2D.IsTouchingLayers(myCollider, WhatIsGround); //determines what ground is and when the collider collides with it

        if (grounded) {
            jumpCharge = 1;
        }

        // Combat Block
        if (sceneName == "Level")
        {
            if (Input.GetKeyDown(KeyCode.S) && (Time.time > fireNext) && ammoControl.gotAmmo)
            {
                ammoControl.updateAmmoBar();
                fireNext = Time.time + fireRate;
                fire();
            }
        }

        if (sceneName == "Boss")
        {
            if (!healthControl.gotHealth)
            {
                SceneManager.LoadScene("BossDeathScene");
            }
        }
        // End of Combat Block
    }

    public void fire()
    {
        // used in Combat Block
        rockPos = transform.position;
        if (mySpriteRenderer.flipX)
        {
            //left
            rockPos += new Vector2(-1.5f, +0.25f);
            Instantiate(rockLeft, rockPos, Quaternion.identity);
        }
        else
        {
            //right
            rockPos += new Vector2(+1.5f, +0.25f);
            Instantiate(rockRight, rockPos, Quaternion.identity);
        }

    }

    // Interaction Block
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("key"))
        {
            noDoorText.gameObject.SetActive(true);
            Invoke("DeactivateDoorText", 2);
            Destroy(other.gameObject); //destroy key item when player collides with it

            GameManager.keyCount++; //add 1 to keyCount value
        }

        if (other.gameObject.CompareTag("door") && GameManager.keyCount < 1)
        {
            noKeyText.gameObject.SetActive(true);
            Invoke("DeactivateKeyText", 2);
        }

        if (other.gameObject.CompareTag("door") && GameManager.keyCount >= 1)
        {
            SceneManager.LoadScene("bossdialogue");
        }

        if (other.gameObject.CompareTag("enemy"))
        {
            SceneManager.LoadScene("DeathScene");
        }

        if (other.gameObject.tag == "stake")
        {
            other.gameObject.SetActive(false);
            gotStake = true;
            stakeDisplay.SetActive(true);
        }

        if (other.gameObject.CompareTag("boss"))
        {
            if (gotStake)
            {
                stakeDisplay.SetActive(false);
                gotStake = false;
                Debug.Log(bossHealth);
                if (bossHealth == 4)
                {
                    bossHealth -= 1;
                    stake2.SetActive(true);
                }
                else if (bossHealth == 3)
                {
                    bossHealth -= 1;
                    stake3.SetActive(true);
                }
                else if (bossHealth == 2)
                {
                    bossHealth -= 1;
                    stake4.SetActive(true);
                }
                else if (bossHealth == 1)
                {
                    SceneManager.LoadScene("postcombatdialogue");
                }
            }
            else
            {
                healthControl.updateHealthBar();
            }
        }
    }
    // End of Interaction Block

    private void DeactivateDoorText() {
        noDoorText.gameObject.SetActive(false);
    }

    private void DeactivateKeyText()
    {
        noKeyText.gameObject.SetActive(false);
    }


}