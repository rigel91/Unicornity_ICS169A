using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private moveplayer player;
    private CameraFollow camera;

    public string pointName;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<moveplayer>();

        if (player.startPoint == pointName)
        {
            player.transform.position = transform.position;

            camera = FindObjectOfType<CameraFollow>();
            camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
