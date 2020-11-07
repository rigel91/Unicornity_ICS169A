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

    // Tracking if game is paused.. ACCESSIBLE.
    public static bool GameIsPaused = false;

    // Reference to Journal Pause UI, brings up UI when appropriately used. (JournalUIPause)
    public GameObject pauseMenuUI;
   
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

    //Removing Clues (Once level completed...?)
    public void Remove (Clue clue)
    {
        clueList.Remove(clue);
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
}
