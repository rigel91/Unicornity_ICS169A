using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speakTo : MonoBehaviour
{
    private string currentDialog;

    // Start is called before the first frame update
    void Start()
    {
        currentDialog = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (currentDialog != "")
            {
                Debug.Log(currentDialog);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        string dialog = collision.GetComponent<dialogBox>().RequestDialog();
        currentDialog = dialog;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string dialog = collision.GetComponent<dialogBox>().RequestDialog();
        currentDialog = "";
    }
}
