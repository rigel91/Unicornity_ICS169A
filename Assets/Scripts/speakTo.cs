using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speakTo : MonoBehaviour
{
    private dialogBox dialogTrigger;
    private string currentDialog = "";

    public JournalUIManage journal;

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
                    string newClue = dialogTrigger.RequestDialog();
                    if (!checkJournalForClue(newClue))
                    {
                        journal.AddClue(newClue);
                        //journal.Add(newClue);
                    }
                }
            }
        }
    }

    private bool checkJournalForClue(string newClue)
    {
        foreach (Clue clue in journal.clueList)
        {
            if (newClue == clue.cName)
            {
                return true;
            }
        }
        return false;
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
