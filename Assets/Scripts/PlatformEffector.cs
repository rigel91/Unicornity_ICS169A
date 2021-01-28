using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffector : MonoBehaviour
{
    private GameObject player;
    private PlatformEffector2D platform;

    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<PlatformEffector2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S) && player.transform.position.y > transform.position.y)
        {
            platform.rotationalOffset = 180f;
        }
        else if (player.transform.position.y < transform.position.y)
        {
            platform.rotationalOffset = 0;
        }
    }
}
