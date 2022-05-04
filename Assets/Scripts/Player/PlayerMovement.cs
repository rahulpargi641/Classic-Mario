using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public Rigidbody2D rigidbody;
    public Animator animator; 
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Walk();
    }

    void Walk()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if(x>0)
        {
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
            
        }   
        else if(x<0)
        {
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
        }
        print("Value is :" + x);
    }
}
