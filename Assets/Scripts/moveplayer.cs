using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //varaible for climbing ladders
    public LayerMask ladderLayer;
    public bool isClimbing;

    //checks if a player gameobject exists in a scene, so it doesnt duplicate when DontDestroyOnLoad() is called
    private static bool playerExists;
    public string startPoint;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (!playerExists)
            {
                playerExists = true;
                DontDestroyOnLoad(transform.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            
            if (GameObject.Find("Player2") != null)
            {
                playerExists = false;
                //Destroy(GameObject.Find("Player3"));
                //Debug.Log("destroy");
                Destroy(GameObject.Find("Player2"));
            }
            //else
            //{
                if (!playerExists)
                {
                    //Debug.Log("dont");
                    playerExists = true;
                    DontDestroyOnLoad(transform.gameObject);
                }
                else
                {
                    Debug.Log("another destroy");
                    Destroy(gameObject);
                }
            //}

        }
        //if (SceneManager.GetActiveScene().buildIndex == 4) //|| SceneManager.GetActiveScene().buildIndex == 7)
        //if (SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 7)
        //{
        //    if (!playerExists)
        //    {
        //        playerExists = true;
        //        DontDestroyOnLoad(transform.gameObject);
        //    }
        //    else
        //    {
        //        //if (SceneManager.GetActiveScene().buildIndex == 7)
        //        //{
        //        if (GameObject.Find("Player2") != null)
        //        {
        //            //Destroy(GameObject.Find("Player3"));
        //            Destroy(GameObject.Find("Player2"));
        //        }
        //        else
        //        {
        //            //Destroy(GameObject.Find("Player2"));
        //            Destroy(gameObject);                                        
        //        }                
        //    }
        //}
        //else if (SceneManager.GetActiveScene().buildIndex == 7)
        //{
        //    if (!playerExists)
        //    {
        //        playerExists = true;
        //        DontDestroyOnLoad(transform.gameObject);
        //    }
        //    else
        //    {
        //        Destroy(GameObject.Find("Player2"));
        //    }
        //}

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
        if (!isClimbing)
        { 
            anim.SetFloat("Speed", Mathf.Abs(translation));
        }

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

        //raycast for climbing ladders
        RaycastHit2D ladderInfo = Physics2D.Raycast(transform.position, Vector2.up, 2f, ladderLayer);
        if (ladderInfo.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isClimbing = true;
            }
        }
        else
        {
            isClimbing = false;
        }
        if (isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, movespeed/2f);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 4f;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move the player
        float translation = Input.GetAxis("Horizontal") * movespeed;
        translation *= Time.deltaTime;
        transform.Translate(translation, 0, 0);
        //rb.velocity = new Vector2(translation, rb.velocity.y);

        //check for slope
        SlopeCheck();
        if (isGrounded && isOnSlope)
        {
            transform.Translate(translation * -slopeNormalPerp.x / 1.5f, 0, 0);
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
