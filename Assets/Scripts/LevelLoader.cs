using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public solutionManager puzzleComplete;

    public float transitionTime = 1f;

    //for moving inbetween scences
    public string exitPoint;
    private moveplayer player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<moveplayer>();
    }

    // Update is called once per frame
    void Update()
    {
        //check for animation level
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            StartCoroutine(AnimationLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    public void LoadNextLevel()
    {
        
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //player.startPoint = exitPoint;
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadLevelWOScene(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator AnimationLevel(int levelIndex)
    {
        //player.startPoint = exitPoint;
        yield return new WaitForSeconds(14);

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        //check for intro level
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(LoadLevelWOScene(2));
        }
        else if (collision.gameObject.tag == "Player" && puzzleComplete.isPuzzleSolved())
        {
            LoadNextLevel();
        }

        //delete later, this is only for the unfinished second level
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            LoadNextLevel();
        } 
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                player.startPoint = exitPoint;
                StartCoroutine(LoadLevel(6));
            }
            else if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                player.startPoint = exitPoint;
                StartCoroutine(LoadLevel(4));
            }
        }
    }
}
