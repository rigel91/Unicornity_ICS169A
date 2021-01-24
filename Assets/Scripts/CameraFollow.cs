using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private moveplayer move;

    public float xOffset;
    [Range(1, 10)]
    public float smoothness;

    //set bounds for the camera
    public float leftBound;
    public float rightBound;
    public float topBound;
    public float bottomBound;

    //camera duplicates
    private static bool cameraExists;

    // Start is called before the first frame update
    void Start()
    {
        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        move = target.GetComponent<moveplayer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        if (move.translation == 0)
        {
            targetPosition = target.position + offset;
        }
        else if (move.isFaceRight)
        {
            targetPosition.x += xOffset;
        }
        else
        {
            targetPosition.x -= xOffset;
        }
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
        transform.position = smoothPosition;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBound, rightBound), Mathf.Clamp(transform.position.y, bottomBound, topBound), transform.position.z);
    }

    void OnDrawGizmos()
    {
        //drawing boundaries for camera
        Gizmos.color = Color.green;

        //top line
        Gizmos.DrawLine(new Vector2(leftBound, topBound), new Vector2(rightBound, topBound));
        //right line
        Gizmos.DrawLine(new Vector2(rightBound, topBound), new Vector2(rightBound, bottomBound));
        //bottom line
        Gizmos.DrawLine(new Vector2(rightBound, bottomBound), new Vector2(leftBound, bottomBound));
        //left line
        Gizmos.DrawLine(new Vector2(leftBound, bottomBound), new Vector2(leftBound, topBound));
    }
}
