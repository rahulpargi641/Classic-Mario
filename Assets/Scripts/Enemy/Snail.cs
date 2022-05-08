using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D rigidbody;
    Animator animator;

    bool moveLeft;
    [SerializeField] Transform downCollision;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft)
        {
            rigidbody.velocity = new Vector2(-moveSpeed, rigidbody.velocity.y);
        }
        else
        {
            rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);

        }
        CheckCollision();
    }

    private void CheckCollision()
    {

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
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
        }

        transform.localScale = tempScale;
    }
}
