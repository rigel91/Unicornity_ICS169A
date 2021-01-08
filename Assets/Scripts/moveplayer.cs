using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveplayer : MonoBehaviour
{
    //player speed and jump force
    public LayerMask platformLayer;
    private BoxCollider2D col;
    private Rigidbody2D rb;
    public float movespeed = 0.5f;
    [System.NonSerialized]
    public float translation;
    public float jumpForce;
    //player move Direction
    private SpriteRenderer sprite;
    public bool isFaceRight;
    public bool isGrounded = true;

    //for the players animation
    private Animator anim;

    //For Random Jumping Sound Purposes
    public AudioClip jumpSound1;
    public AudioClip jumpSound2;
    public AudioClip jumpSound3;

    //variables for climbing slopes
    private Vector2 colliderSize; //gets the size of the collider
    private Vector2 slopeNormalPerp;
    public float slopeCheckDistance;
    private float downAngle;
    public bool isOnSlope;
    private float slopeDownAngleOld;
    private float slopeSideAngle;

    public PhysicsMaterial2D noFriction;
    public PhysicsMaterial2D fullFriction;

    // Start is called before the first frame update
    void Start()
    {
        //gets all of the gameObjects components
        col = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();

        isFaceRight = true;

        colliderSize = col.size;
    }

    void Update()
    {
        translation = Input.GetAxis("Horizontal") * movespeed;
        //animates the character based on movement
        anim.SetFloat("Speed", Mathf.Abs(translation));

        //player jump
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("Jump");

            //HumanJumpSound -- Instance of Jump happens here.
            AudioManager.instance.RandomizeSfx(jumpSound1, jumpSound2, jumpSound3);
        }
        if (isGrounded)
        {
            anim.SetBool("IsJumping", false);

        }
        else
        {
            anim.SetBool("IsJumping", true);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move the player
        float translation = Input.GetAxis("Horizontal") * movespeed;
        translation *= Time.deltaTime;
        transform.Translate(translation, 0, 0);

        //check for slope
        SlopeCheck();
        if (isGrounded && isOnSlope)
        {
            transform.Translate(translation * -slopeNormalPerp.x / 2, 0, 0);
        }


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
    }

    private bool IsGrounded()
    {
        /*
            This function uses a Box cast that checks if the player is touching the ground 
        */
        RaycastHit2D ray = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, platformLayer);
        if (ray.collider != null)
        {
            isGrounded = true;
            return true;
        }
        isGrounded = false;
        return false;
    }
    private void WalkAudio()
    {
        AudioManager.instance.Play("Walk");
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y / 2);
        SlopeCheckHoriz(checkPos);
        SlopeCheckVert(checkPos);
    }

    private void SlopeCheckHoriz(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, platformLayer);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, platformLayer);

        if (slopeHitFront)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }

    }

    private void SlopeCheckVert(Vector2 pos)
    {
        RaycastHit2D hit;
        if (isFaceRight)
        {
             hit = Physics2D.Raycast(new Vector2(pos.x + 0.5f, pos.y), Vector2.down, slopeCheckDistance, platformLayer);
        }
        else
        {
            hit = Physics2D.Raycast(new Vector2(pos.x - 0.5f, pos.y), Vector2.down, slopeCheckDistance, platformLayer);
        }
        
        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            downAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (downAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
            }
            slopeDownAngleOld = downAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        if (isOnSlope)
        {
            rb.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
    }
} 
