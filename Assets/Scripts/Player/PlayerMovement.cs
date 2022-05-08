using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform groundCheckPos;
    public LayerMask groundLayer;

    Rigidbody2D rigidbody;
    Animator animator;

    bool isGrounded;
    bool jump;

    [SerializeField] float speed = 7.0f;
    float jumpPower = 8f;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        PlayeJump();
    }

    void FixedUpdate()
    {
        Walk();
    }

    void Walk()
    {
        float input = Input.GetAxisRaw("Horizontal");
        if(input>0)
        {
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
            ReverseDirection(1);
            
        }   
        else if(input<0)
        {
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
            ReverseDirection(-1);
        }
        else
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y); // prevent from sliding and also natural flow in y direction 
        }
        //print("Value is :" + inputV);

        animator.SetInteger("Velocity", Mathf.Abs((int)rigidbody.velocity.x));
    }

    void ReverseDirection(int direction)
    {
        Vector3 tempScale = transform.localScale; // you need to store temporary variable in unity
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

  /*  private void OnCollisionEnter2D(Collision2D collision)
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
    }   */

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPos.position, Vector2.down, 0.1f, groundLayer);
        if (isGrounded)
        {
            if(jump)
            {
                jump = false;
                animator.SetBool("Jump", false);
            }
        }
    }

    void PlayeJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jump = true;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpPower);
                animator.SetBool("Jump", true);
            }
        }
    }
 
}
