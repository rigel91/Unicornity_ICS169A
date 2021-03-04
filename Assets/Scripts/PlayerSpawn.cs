using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private moveplayer player;
    //private CameraFollow camera;

    public string pointName;

    private float time = 0.5f;
    private bool once;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<moveplayer>();

        time = 0.2f;
        once = false;

        //if (player.startPoint == pointName)
        //{
        //    Debug.Log("great!");
        //    player.transform.position = transform.position;

        //    //camera = FindObjectOfType<CameraFollow>();
        //    //camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<moveplayer>();

        if (Time.time > time && !once)
        {
            Debug.Log("great!");
            once = true;
            if (player.startPoint == pointName)
            {
                player.transform.position = transform.position;

                //camera = FindObjectOfType<CameraFollow>();
                //camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
            }
        }
    }
}
