﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveplayer : MonoBehaviour
{
    //player speed and jump force
    public LayerMask platformLayer;
    private BoxCollider2D col;
    private Rigidbody2D rb;
    public float movespeed = 0.5f;
    public float jumpForce;
    //player move Direction
    private SpriteRenderer sprite;
    public bool isFaceRight;

    //for the players animation
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //gets all of the gameObjects components
        col = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();

        isFaceRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        //move the player
        float translation = Input.GetAxis("Horizontal") * movespeed;
        translation *= Time.deltaTime;
        transform.Translate(translation, 0, 0);

        //flip the player
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            //face right
            sprite.flipX = false;
            isFaceRight = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            //face left
            sprite.flipX = true;
            isFaceRight = false;
        }

        //animates the character based on movement
        anim.SetFloat("Speed", Mathf.Abs(translation));

        //player jump
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        //if (Input.GetButtonDown("Jump"))
        //{
        //    jump();
        //}

        //void jump()
        //{
        //    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        //}
    }

    private bool IsGrounded()
    {
        /*
            This function uses a Box cast that checks if the player is touching the ground 
        */
        RaycastHit2D ray = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, platformLayer);
        if (ray.collider != null)
        {
            return true;
        }
        return false;
    }

}
