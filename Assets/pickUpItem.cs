using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpItem : MonoBehaviour
{
    public GameObject itemInWorld;
    public GameObject itemInInventory;


    public bool inPickupRange;

    // Start is called before the first frame update
    void Start()
    {
        inPickupRange = false;
        itemInInventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (inPickupRange)
            {
                moveToInventory();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks that the thing collided with was the player
        if (collision.GetComponent<speakTo>() != null)
        {
            inPickupRange = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //checks that the thing collided with was the player
        if (collision.GetComponent<speakTo>() != null)
        {
            inPickupRange = false;
        }
    }

    private void moveToInventory()
    {
        itemInWorld.SetActive(false);
        itemInInventory.SetActive(true);
    }


}
