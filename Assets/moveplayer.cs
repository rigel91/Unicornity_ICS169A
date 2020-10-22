using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveplayer : MonoBehaviour
{
    public float movespeed = 0.5f;
    public Transform t;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Horizontal") * movespeed;
        translation *= Time.deltaTime;

        transform.Translate(translation, 0, 0);


        if (Input.GetButtonDown("Jump"))
        {
            jump();
        }

        void jump()
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
        }
    }
}
