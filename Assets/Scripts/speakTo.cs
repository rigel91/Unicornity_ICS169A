using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speakTo : MonoBehaviour
{
    private dialogBox dialogTrigger;
    private string currentDialog = "";

    public DialogueManager DialogueManager;


    

    // Start is called before the first frame update
    void Start()
    {
        dialogTrigger = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (currentDialog != "")
            {
                if (!dialogTrigger.checkRepeatRequest())
                {
                    dialogTrigger.RequestDialog();
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialogTrigger = collision.GetComponent<dialogBox>();
        currentDialog = "something";

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogTrigger = null;
        currentDialog = string.Empty;

        //Stop focusing
        //RemoveFocus();
    }

    
}
