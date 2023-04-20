using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D rigidbody;
    Animator animator;

    public LayerMask playerLayer;

    bool moveLeft;
    bool canMove;
    bool stunned;
    [SerializeField] Transform downCollision, topCollision, leftCollision, rightCollision;
    Vector3 leftCollisionPos, rightCollisionPos;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        leftCollisionPos = leftCollision.position;
        rightCollisionPos = rightCollision.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if(moveLeft)
        {
                rigidbody.velocity = new Vector2(-moveSpeed, rigidbody.velocity.y);
            }
        else
            {
                rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);

            }
        }

        CheckCollision();
    }

    private void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftCollision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightCollision.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(topCollision.position, .2f, playerLayer);

        if (topHit != null)
        {
            if (topHit.gameObject.tag == Tags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    canMove = false;
                    rigidbody.velocity = new Vector2(0, 0);
                    animator.Play("Stunned");
                    stunned = true;

                    // Beetle code here
                    if(tag == Tags.BEETLE_TAG)
                    {
                        Debug.Log("Bettle should be stunned");
                        animator.Play("Bettle Stunned");
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }

        if (leftHit) // player pushing from the left, snail will be pushed to the right
        {
            if(leftHit.collider.gameObject.tag == Tags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    // Apply damgae to the player
                }
                else
                {
                    if(tag != Tags.BEETLE_TAG)
                    {
                        rigidbody.velocity = new Vector2(15f, rigidbody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                    
                }
            }
        }

        if (rightHit)
        {
            if(rightHit.collider.gameObject.tag == Tags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    // Apply damage to the player
                }
                else
                {
                    if(tag != Tags.BEETLE_TAG)
                    {
                        rigidbody.velocity = new Vector2(-15f, rigidbody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                    
                }
            }
        }

        if (!Physics2D.Raycast(downCollision.position, Vector2.down, 0.5f)) 
        {
            print("not detecting collision");
            ReverseDirection();
        }
    }

    private void ReverseDirection()
    {
        moveLeft = !moveLeft;

        Vector3 tempScale = transform.localScale;
        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);

            leftCollision.position = leftCollisionPos;
            rightCollision.position = rightCollisionPos;
            

        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);

            rightCollision.position = leftCollisionPos;
            leftCollision.position = rightCollisionPos;
        }

        transform.localScale = tempScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            animator.Play("Stunned");
        }
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}
