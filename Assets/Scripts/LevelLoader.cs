﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public solutionManager puzzleComplete;

    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
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
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator AnimationLevel(int levelIndex)
    {
        yield return new WaitForSeconds(14);

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        //check for intro level
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            LoadNextLevel();
        }

        if (collision.gameObject.tag == "Player" && puzzleComplete.isPuzzleSolved())
        {
            LoadNextLevel();
        }
    }
}