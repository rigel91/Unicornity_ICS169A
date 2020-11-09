using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Journal Screen Data
    public List<Clue> clueList = new List<Clue>();
    public int space = 6;

    //This is Erol, creating an alternate clue list using different word objects instead of clue objects
    public List<GameObject> clueWordList = new List<GameObject>();

    public float clueScale = .75f;

    // Tracking if game is paused.. ACCESSIBLE.
    public static bool GameIsPaused = false;

    // Reference to Journal Pause UI, brings up UI when appropriately used. (JournalUIPause)
    public GameObject pauseMenuUI;

    public GameObject clueToSetActive;
   
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
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        
    }

    //Func that will resume time in-game, leaving player exactly where they left off.
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    //Adding Clues to JUI

    // This is Daniel's version of a clue-adding function
    public void Add (Clue clue)
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
            }
            clueIndex++;
        }
    }

    //Removing Clues (Once level completed...?)
    public void Remove (Clue clue)
    {
        clueList.Remove(clue);
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }

    private void spawnClueInJournal(int clueIndex)
    {
        //clueWordList[clueIndex].GetComponent<RectTransform>().sizeDelta = new Vector2(clueScale, clueScale);
        clueWordList[clueIndex].GetComponent<RectTransform>().localScale = new Vector3(clueScale, clueScale, 1);
        Debug.Log("size should have changed");
    }
}
