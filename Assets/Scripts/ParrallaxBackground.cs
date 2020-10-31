using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrallaxBackground : MonoBehaviour
{
    //moving the sprites to follow the camera
    private Transform cameraObject;
    private Vector3 cameraPos;
    public Vector2 multiplier;

    //getting the sprites size
    private Sprite sprite;
    private Texture2D texture;
    private float textureSizeX;
    private float textureSizeY;

    // Start is called before the first frame update
    void Start()
    {
        cameraObject = Camera.main.transform;
        cameraPos = cameraObject.position;

        sprite = GetComponent<SpriteRenderer>().sprite;
        texture = sprite.texture;
        textureSizeX = texture.width / sprite.pixelsPerUnit;
        textureSizeY = texture.height / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 distance = cameraObject.position - cameraPos;
        transform.position += new Vector3(distance.x * multiplier.x, distance.y * multiplier.y);
        cameraPos = cameraObject.position;

        //if (Mathf.Abs(cameraObject.position.x - transform.position.x) >= textureSizeX)
        //{
        //    float offset = (cameraObject.position.x - transform.position.x) % textureSizeX;
        //    transform.position = new Vector3(cameraObject.position.x + offset, cameraObject.position.y);
        //}
        //if (Mathf.Abs(cameraObject.position.y - transform.position.y) >= textureSizeY)
        //{
        //    float offset = (cameraObject.position.y - transform.position.y) % textureSizeY;
        //    transform.position = new Vector3(cameraObject.position.x, cameraObject.position.y + offset);
        //}
    }
}
