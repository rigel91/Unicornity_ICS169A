using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalLevelCutScene : MonoBehaviour
{
    public GameObject player;
    public GameObject dialogue;
    public Animator anim;
    private bool flag;
    private bool endConversation;

    // Start is called before the first frame update
    void Start()
    {
        flag = false;
        endConversation = false;
    }

    // Update is called once per frame
    void Update()
    {
        endConversation = dialogue.GetComponent<dialogBox>().close;
        if (endConversation)
        {
            //Debug.Log("End");
            StartCoroutine(delay());
            endConversation = false;
        }
    }

    IEnumerator delay()
    {
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(11);
    }

    IEnumerator cutscene()
    {
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<moveplayer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag)
        {
            flag = true;
            player.GetComponent<moveplayer>().movespeed = 0;
            StartCoroutine(cutscene());
        }
    }
}
