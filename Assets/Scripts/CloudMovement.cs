using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float cloudSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(cloudSpeed * Time.deltaTime, 0, 0));
    }
}
