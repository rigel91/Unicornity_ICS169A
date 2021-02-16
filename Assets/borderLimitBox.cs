using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class borderLimitBox : MonoBehaviour
{
    public Transform snapPosition;

    public Transform boxToSnap;

    public BoxCollider2D b;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BoxCollider2D>() == b)
        {
            print("true");
            boxToSnap.position = snapPosition.position;
        }
    }

}
