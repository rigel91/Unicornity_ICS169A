using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTowerZoom : MonoBehaviour
{
    //current position is x: 402, y: 11, camera size is 15
    //target position is x: 413, y: 53, and camera size is 60
    public GameObject zoomCamera;
    private GameObject player;

    public GameObject playerCamera;

    public bool isZoom;

    private Vector3 start;
    private Vector3 end;

    // Start is called before the first frame update
    void Start()
    {
        

        start = new Vector3(zoomCamera.transform.position.x, zoomCamera.transform.position.y, zoomCamera.transform.position.z);
        end = new Vector3(270, 53, -15);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (isZoom && player.transform.position.x <= 288)
        {
            isZoom = false;
        }
        else if(isZoom == false)
        {
            if (player != null && player.transform.position.x > 288)
            {
                isZoom = true;
            }
        }

        if (isZoom == true)
        {
            zoomCamera.SetActive(true);
            playerCamera.SetActive(false);
            //player position from x: 402 to x: 459
            float distance = player.transform.position.x - 288;
            float t = distance / 57;           
            zoomCamera.transform.position = Vector3.Lerp(start, end, t);
            if (t * 60 <= 15)
            {
                zoomCamera.GetComponent<Camera>().orthographicSize = 15;
            }
            else
            {
                zoomCamera.GetComponent<Camera>().orthographicSize = t * 60;
            }            
        }
        else
        {
            playerCamera.SetActive(true);
            zoomCamera.SetActive(false);            
        }
    }
}
