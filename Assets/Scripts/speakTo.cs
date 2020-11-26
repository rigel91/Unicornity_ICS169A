using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speakTo : MonoBehaviour
{
    private dialogBox dialogTrigger;
    private string currentDialog = string.Empty;

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
            if (currentDialog != string.Empty)
            {
                if (dialogTrigger && !dialogTrigger.checkRepeatRequest())
                {
                    string[] newClues = dialogTrigger.RequestDialog();
                    foreach (string newClue in newClues)
                    {
                        if (newClue != string.Empty)
                        {
                            if (!checkJournalForClue(newClue))
                            {
                                journal.AddClue(newClue);
                                updateHintInJournal(newClue);
                                dialogTrigger.revealHintSentence();
                            }
                        }
                    }
                    
                }
            }
        }
    }

    private void updateHintInJournal(string newClue)
    {
        foreach (GameObject clueObject in journal.clueWordList)
        {
            if (clueObject.GetComponent<clueWord>().clueText == newClue) //find the appropriate clueWord object
            {
                string hintText = clueObject.GetComponent<clueWord>().hintText; //find the appropriate hint for the given clueWord
                journal.revealHint(hintText);
            }
            
        }
    }

    private bool checkJournalForClue(string newClue) //currently not helpful since the way clues are made to appear in the journal (change the size from 0 to 1) cannot make a clue appear more than once
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
        if (collision.GetComponent<dialogBox>() != null)
        {
            dialogTrigger = collision.GetComponent<dialogBox>();
            currentDialog = "something";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<dialogBox>() != null) //only do this for the speakTo trigger boxes not the walkAway trigger boxes
        {
            dialogTrigger = null;
            currentDialog = string.Empty;
        }
        //Stop focusing
        //RemoveFocus();
    }

    
}
