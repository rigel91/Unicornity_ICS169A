using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogBox : MonoBehaviour
{
    public string Dialog1;
    public string Dialog2;
    public string Dialog3;
    public string Dialog4;
    public string Dialog5;
    public string Dialog6;
    public string Dialog7;
    public string Dialog8;
    public string Dialog9;
    public string Dialog10;

    private List<string> AllDialog;
    private string currentDialog;
    private int dialogIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        AllDialog = new List<string> { Dialog1, Dialog2, Dialog3, Dialog4, Dialog5, Dialog6, Dialog7, Dialog8, Dialog9, Dialog10 };
        currentDialog = AllDialog[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    private string GetCurrentDialog()
    {
        return currentDialog;
    }

    private void advanceDialog()
    {
        dialogIndex += 1;
        if (dialogIndex >= AllDialog.Count)
        {
            dialogIndex = 0;
        }
        else if (AllDialog[dialogIndex].Length <= 0)
        {
            dialogIndex = 0; //if reached end of dialog, loop back to first message
        }

        currentDialog = AllDialog[dialogIndex];

    }


    public string RequestDialog()
    {
        string returnDialog = GetCurrentDialog();
        advanceDialog();
        return returnDialog;

    }
}
