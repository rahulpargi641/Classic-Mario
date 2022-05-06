using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 7.0f;
     Rigidbody2D rigidbody;
     Animator animator;
    public Transform groundCheckPos;
     public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.Raycast(groundCheckPos.position, Vector2.down, 0.5f, groundLayer))
        {
            print("Collide With Ground");
        }
    }

    void FixedUpdate()
    {
        Walk();
    }

    void Walk()
    {
        float inputV = Input.GetAxisRaw("Horizontal");
        if(inputV>0)
        {
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
            ChangeDirection(1);
            
        }   
        else if(inputV<0)
        {
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
            ChangeDirection(-1);
        }
        else
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y); // prevent from sliding and also natural flow in y direction 
        }
        print("Value is :" + inputV);

        animator.SetInteger("Velocity", Mathf.Abs((int)rigidbody.velocity.x));
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
           // print("Collision has happened");

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
           // print("Collision has happened");

        }
    }
 
}
