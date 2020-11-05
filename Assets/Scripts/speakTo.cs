using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speakTo : MonoBehaviour
{
    private dialogBox dialogTrigger;
    private string currentDialog = "";

    public DialogueManager DialogueManager;

    //For Interaction
    public Interactable focus;

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
                //currentDialog = dialogTrigger.RequestDialog();
                dialogTrigger.RequestDialog();
                //Debug.Log(currentDialog);

                //DialogueManager.TriggerContinueNPCDialogue();

            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialogTrigger = collision.GetComponent<dialogBox>();
        currentDialog = "something";

        //Base of player/NPC interaction? - DA 11/4 
        Interactable interactable = collision.GetComponent<Interactable>();
        if (interactable != null)
        {
            SetFocus(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogTrigger = null;
        currentDialog = string.Empty;

        //Stop focusing
        RemoveFocus();
    }

    //NPC interaction recoginition
    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;

        //'E' Prompt here for talking NPC's
    }

    //NPC interaction DE-recognition
    void RemoveFocus()
    {
        focus = null;
    }
}
