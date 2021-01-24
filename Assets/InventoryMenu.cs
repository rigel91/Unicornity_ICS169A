using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{

    private string invOpenKey = "i";
    private string invCloseKey = "escape";

    private bool invIsOpen;

    private Animator invAnimator;

    // Start is called before the first frame update
    void Start()
    {
        invIsOpen = false;

        invAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(invOpenKey) ) //open the inventory panel when
        {
            if (invIsOpen)
            {
                inventoryClose();
            }
            else
            {
                inventoryOpen();
            }
        }

        if (Input.GetKeyDown(invCloseKey)) //close the inventory panel
        {
            inventoryClose();
        }
    }

    private void inventoryOpen()
    {
        invAnimator.SetBool("invIsOpen", true);
        invIsOpen = true;


    }

    private void inventoryClose()
    {
        invAnimator.SetBool("invIsOpen", false);
        invIsOpen = false;
    }
}
