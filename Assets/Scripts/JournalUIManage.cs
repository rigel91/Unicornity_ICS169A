using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class JournalUIManage : MonoBehaviour
{
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
}
