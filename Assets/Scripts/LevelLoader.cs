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
        if (SceneManager.GetActiveScene().buildIndex != 2 && SceneManager.GetActiveScene().buildIndex != 9)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<moveplayer>();
        }
        
        //check for animation level
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            //falling animation level
            StartCoroutine(AnimationLevel(14));
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 9)
        {
            StartCoroutine(AnimationLevel(5));
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

    IEnumerator AnimationLevel(int time)
    {
        //player.startPoint = exitPoint;
        yield return new WaitForSeconds(time);

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        //check for intro level
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(LoadLevelWOScene(2));
        }
        //delete later, this is only for the unfinished second level
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            StartCoroutine(LoadLevelWOScene(7));
            //SceneManager.LoadScene(7);
            //LoadNextLevel();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            StartCoroutine(LoadLevelWOScene(9));
        }
        else if (collision.gameObject.tag == "Player" && puzzleComplete.isPuzzleSolved())
        {
            Debug.Log("here");
            LoadNextLevel();
        }               
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKey(KeyCode.W))
        {
            if (exitPoint == "Doctor House Enter")
            {
                player.startPoint = exitPoint;
                StartCoroutine(LoadLevel(6));
            }
            else if (exitPoint == "Doctor House Exit")
            {
                player.startPoint = exitPoint;
                StartCoroutine(LoadLevel(4));
            }

            if (exitPoint == "Church Enter")
            {
                player.startPoint = exitPoint;
                StartCoroutine(LoadLevel(5));
            }
            else if (exitPoint == "Church Exit")
            {
                player.startPoint = exitPoint;
                StartCoroutine(LoadLevel(4));
            }

            if (exitPoint == "Tavern Enter")
            {
                player.startPoint = exitPoint;
                StartCoroutine(LoadLevel(8));
            }
            else if (exitPoint == "Tavern Exit")
            {
                player.startPoint = exitPoint;
                StartCoroutine(LoadLevel(7));
            }

            //if (SceneManager.GetActiveScene().buildIndex == 4)
            //{
            //    player.startPoint = exitPoint;
            //    StartCoroutine(LoadLevel(6));
            //}
            //else if (SceneManager.GetActiveScene().buildIndex == 6)
            //{
            //    player.startPoint = exitPoint;
            //    StartCoroutine(LoadLevel(4));
            //}
        }
        else if (collision.tag == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex == 8 && player.transform.position.x > 450)
            {
                Debug.Log("here");
                //tower animation level
                StartCoroutine(AnimationLevel(6));
            }
        }
    }
}
