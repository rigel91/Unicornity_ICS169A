using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Data for camera
    public Vector3 offset;
    //offset for the front of the player
    public float xOffset;
    [Range(1, 10)]
    public float smoothness;

    //set bounds for the camera
    public float leftBound;
    public float rightBound;
    public float topBound;
    public float bottomBound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //drawing lines for camera
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
