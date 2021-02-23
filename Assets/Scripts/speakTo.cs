using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speakTo : MonoBehaviour
{
    private dialogBox dialogTrigger;
    private string currentDialog = string.Empty;

    public JournalUIManage journal;

    public List<string> npcIDs = new List<string>{};

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
                //if (dialogTrigger && !dialogTrigger.checkRepeatRequest())
                if (dialogTrigger)
                {
                    string[] newClues = dialogTrigger.RequestDialog();
                    foreach (string newClue in newClues)
                    {
                        if (newClue != string.Empty)
                        {
                            //checkJournalForClue(newClue);
                            if(true)
                            {
                                journal.AddClue(newClue);
                                updateHintInJournal(newClue);
                                

                                //find the npcIndex of the npc so that we know which panel to use
                                string npcID = dialogTrigger.GetComponent<NPCData>().characterID;
                                int panelIndex = 0;
                                foreach (string id in npcIDs)
                                {
                                    if (npcID == id)
                                    {
                                        break;
                                    }
                                    panelIndex++;
                                }
                                print(panelIndex);
                                journal.AddNpcPanel(panelIndex);

                                
                                
                                dialogTrigger.revealHintSentence();
                            }
                        }
                    }
                    
                }
            }
        }
    }

    //adds the draggable object in the journal
    //this gets called in update basically when the speakTo function detects that the player has collided with the trigger for a dialogBox and 
    //the player has pressed e, meaning that the player is interacting with an NPC with dialog
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

    private bool checkJournalForClue(string newClue) //being modified so that if the word is already in the journal, it will instead look for a second copy of the word. Currently only works for 1 duplicate but I can make this work recursively
    {
        foreach (string clueCheck in journal.foundClueList)
        {
            if (newClue == clueCheck)
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
            dialogTrigger.showSpeechPrompt();
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
