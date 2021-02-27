using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroLevelCutscene : MonoBehaviour
{
    public GameObject lights;
    public GameObject player;
    public AudioManager audio;
    public GameObject dialogue;
    public TextMeshProUGUI text;
    private bool flag;
    public bool endConversation;
    // Start is called before the first frame update
    void Start()
    {
        lights.SetActive(false);
        flag = false;
        endConversation = false;
    }

    // Update is called once per frame
    void Update()
    {
        endConversation = dialogue.GetComponent<dialogBox>().close;
        if (endConversation)
        {
            lights.SetActive(false);
            dialogue.SetActive(false);
            audio.GetComponent<AudioSource>().Play();
            player.GetComponent<moveplayer>().enabled = true;
            player.GetComponent<moveplayer>().movespeed = 10;
            //TODO: add conversation with god
            endConversation = false;
        }
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
            lights.SetActive(true);
            //need to mute player move sound
            audio.GetComponent<AudioSource>().Stop();
            player.GetComponent<moveplayer>().movespeed = 0;
            text.enabled = false;

            StartCoroutine(cutscene());
        }
    }
}
