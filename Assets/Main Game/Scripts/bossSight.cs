using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSight : MonoBehaviour
{
    // Casey
    public Transform originPoint;
    private Vector2 direction = new Vector2(1, 0);
    public float range;
    public float speed;
    private Rigidbody2D rb;

    //Julie
    private GameObject Player;
    private Vector2 defaultPosition;
    public BoxCollider2D myCollider;

    public float movementSpeed;
    public float kamikazeRun;
    public bool moving;
    public SpriteRenderer mySpriteRenderer;
    public float pathSize;

    // Use this for initialization
    void Start()
    {
        defaultPosition = transform.localPosition;
        movementSpeed = 0.05f;
        kamikazeRun = 0.15f;

        // moving is always true
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        moving = true;
        myCollider = GetComponent<BoxCollider2D>();

        Player = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {

        int layerMask = 1 << 9;
        layerMask = ~layerMask;

        Debug.DrawRay(originPoint.position, direction * range);
        RaycastHit2D hit = Physics2D.Raycast(originPoint.position, direction, range, layerMask);
    }

    void FixedUpdate()
    {
        if ((Player.transform.position.y >= (transform.position.y - 0.75)) && (Player.transform.position.y <= (transform.position.y + 0.75)))
        {
            if (Player.transform.position.x > transform.position.x)
            {
                mySpriteRenderer.flipX = false;
                Vector2 newPos = new Vector2(transform.position.x + kamikazeRun, defaultPosition.y);
                transform.position = newPos;
            }
            if (Player.transform.position.x < transform.position.x)
            {
                mySpriteRenderer.flipX = true;
                Vector2 newPos = new Vector2(transform.position.x - kamikazeRun, defaultPosition.y);
                transform.position = newPos;
            }
        }
        else
        {
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
    }

    void OnCollisionEnter2D(Collision2D collide)
    {
      
    }
}
