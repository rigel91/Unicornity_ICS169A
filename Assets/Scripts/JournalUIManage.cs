﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalUIManage : MonoBehaviour
{
    #region Singleton
    public static JournalUIManage instance;

    //Constant same access to the Journal UI without creating a new one (SINGLETON).
    //And avoiding long method calls.
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of a Journal FOUND!");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    //used for the notification badge on the journal button
    public GameObject journalNotificationBadge;


    //Journal Screen Data
    public List<Clue> clueList = new List<Clue>();
    public int space = 6;

    public List<string> foundClueList = new List<string>();

    //draggable objects that appear on the journal
    public List<GameObject> clueWordList = new List<GameObject>();

    //idk
    public List<GameObject> hintSentenceList = new List<GameObject>();

    public List<string> translateHintList = new List<string>();
    public List<GameObject> hintBoxList = new List<GameObject>();

    public float clueScale = .6f;

    // Tracking if game is paused.. ACCESSIBLE.
    public static bool GameIsPaused = false;

    // Reference to Journal Pause UI, brings up UI when appropriately used. (JournalUIPause)
    public GameObject pauseMenuUI;

    //List of NPC data panels for the associated level.
    public List<GameObject> npcPanel = new List<GameObject>();

    //Func that will pause gameplay and bring up UI for Journal/Puzzle Solving
    public void JournalUIPause()
    {
        if (GameIsPaused)
        {
            ResumeGame();

        }
        else
        {
            pauseMenuUI.SetActive(true);
            journalNotificationBadge.GetComponent<journalNotificationManager>().resetNewClues();
            //Time.timeScale = 0f;  Changing the time scale to 0 causes box colliders in the Journal UI to not register. Cannot freeze time
            GameIsPaused = true;
        }
        
    }

    //Func that will resume time in-game, leaving player exactly where they left off.
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        //Time.timeScale = 1f; Don't freeze and un-freeze time; see comment above in JournalUIPause() function
        GameIsPaused = false;
    }

    //Adding Clues to JUI

    // This is Daniel's version of a clue-adding function
    public void Add(Clue clue)
    {
        if (clue.clueKnown == false)
        {
            if (clueList.Count >= space)
            {
                Debug.Log("Maximum Clues Reached!");
                return;
            }

            clueList.Add(clue);
            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();

            //Testing
            Debug.Log("Clue ACQUIRED!!");
        }
        else
            Debug.Log("Clue already given!");
    }


    //Erol's version of a clue-adding function
    public void AddClue(string clueWord)
    {
        int clueIndex = 0;
        foreach (GameObject clueObject in clueWordList)
        {
            if (clueObject.GetComponent<clueWord>().clueText == clueWord)
            {
                spawnClueInJournal(clueIndex);
                foundClueList.Add(clueWord);


            }
            clueIndex++;
        }
    }

    public void AddNpcPanel(int npcID)
    {
        if (npcID - 1 < npcPanel.Capacity) //if the npcID provided is actually in the list (if it was not found in the list, the npcID provided will be equal to the capacity of the list)
        {
            //find the appropriate npcPanel with this npcID
            int panelIndex = 0;
            foreach (GameObject panel in npcPanel)
            {
                if (panel.GetComponent<npcSpeakingTag>().npcSpeakingID == npcID)
                {
                    spawnNpcPanelInJournal(panelIndex);
                }
                panelIndex++;
            }
        }
    }

    //Removing Clues (Once level completed...?)
    public void Remove(Clue clue)
    {
        clueList.Remove(clue);
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }

    private void spawnClueInJournal(int clueIndex)
    {
        clueWordList[clueIndex].GetComponent<RectTransform>().localScale = new Vector3(clueScale, clueScale, 1);
        
    }

    private void spawnNpcPanelInJournal(int panelIndex)
    {
        if (!npcPanel[panelIndex].activeSelf)
        {
            //increments newClue tracker in journalNotificationBadge so that the notification badge will appear
            journalNotificationBadge.GetComponent<journalNotificationManager>().incrementNewClues();

            npcPanel[panelIndex].SetActive(true);
        }
        
    }

    public void revealHint(string hintText)
    {
        foreach (GameObject hintBox in hintBoxList)
        {
            if (hintText == hintBox.GetComponent<revealHint>().hintText)
            {
                hintBox.GetComponent<revealHint>().RevealHint();
            }
        }
    }
}
