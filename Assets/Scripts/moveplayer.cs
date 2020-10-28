using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveplayer : MonoBehaviour
{
    //player speed and jump force
    public float movespeed = 0.5f;
    public float jumpForce;
    //player move Direction
    public SpriteRenderer sprite;
    private bool isFaceRight;
    private float direction;

    //for the players animation
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        isFaceRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Horizontal") * movespeed;
        translation *= Time.deltaTime;
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

        transform.Translate(translation, 0, 0);

        //animates the character based on movement
        anim.SetFloat("Speed", Mathf.Abs(translation));


        if (Input.GetButtonDown("Jump"))
        {
            jump();
        }

        void jump()
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
    
}
