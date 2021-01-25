using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private moveplayer move;
    private GameManager gm;
    //camera duplicates
    private static bool cameraExists;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        

        //if (!cameraExists)
        //{
        //    cameraExists = true;
        //    DontDestroyOnLoad(transform.gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        move = target.GetComponent<moveplayer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + gm.offset;
        if (move.translation == 0)
        {
            targetPosition = target.position + gm.offset;
        }
        else if (move.isFaceRight)
        {
            targetPosition.x += gm.xOffset;
        }
        else
        {
            targetPosition.x -= gm.xOffset;
        }
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, gm.smoothness * Time.deltaTime);
        transform.position = smoothPosition;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, gm.leftBound, gm.rightBound), Mathf.Clamp(transform.position.y, gm.bottomBound, gm.topBound), transform.position.z);
    }
}
