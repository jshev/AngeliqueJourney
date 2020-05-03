using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySight : MonoBehaviour {
    // Casey
    public Transform originPoint;
    private Vector2 direction = new Vector2(1, 0);
    public float range;
    public float speed;
    private Rigidbody2D rb;

    //Julie
    private GameObject Player;
    private Vector2 defaultPosition;
    private float elapsedTime;
    public BoxCollider2D myCollider;

    public float movementSpeed;
    public bool moving;
    public SpriteRenderer mySpriteRenderer;
    public float pathSize;

    // Use this for initialization
    void Start() {
        defaultPosition = transform.localPosition;
        movementSpeed = 0.05f;
        elapsedTime = 0f;

        // moving is always true
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        moving = true;
        myCollider = GetComponent<BoxCollider2D>();

        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update(){

        int layerMask = 1 << 9;
        layerMask = ~layerMask;

        Debug.DrawRay(originPoint.position, direction * range);
        RaycastHit2D hit = Physics2D.Raycast(originPoint.position, direction, range, layerMask);
    }

    void FixedUpdate() {
        // flip sprite and change direction if enemy reaches end of its range
        if (elapsedTime <= 0)
        {
            myCollider.enabled = true;
            // makes sprite red and opaque
            // mySpriteRenderer.color = new Color(0.5f, 0f, 0f, 1f);
            if ((transform.position.x > (defaultPosition.x + pathSize)))
            {
                mySpriteRenderer.flipX = true;
                movementSpeed = -movementSpeed;
            }
            if ((transform.position.x < (defaultPosition.x - pathSize)))
            {
                mySpriteRenderer.flipX = false;
                movementSpeed = -movementSpeed;
            }

            // change position of enemy based on movement speed
            Vector2 newPos = new Vector2(transform.position.x + movementSpeed, defaultPosition.y);
            transform.position = newPos;
        }
        else
        {
            myCollider.enabled = false;
            //makes sprite red and slightly trasnparent
            //mySpriteRenderer.color = new Color(0.5f, 0f, 0f, .5f);
        }

        //if (elapsedTime > 0)
        //{
            //elapsedTime -= Time.deltaTime;
        //}
    }

    void OnCollisionEnter2D(Collision2D collide) {
        if (collide.gameObject.tag == "rock") {
            elapsedTime = 10f;
        }
    }
}
