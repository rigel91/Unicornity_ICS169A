using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogBox : MonoBehaviour
{
    //public string Dialog1;
    //public string Dialog2;
    //public string Dialog3;
    //public string Dialog4;
    //public string Dialog5;
    //public string Dialog6;
    //public string Dialog7;
    //public string Dialog8;
    //public string Dialog9;
    //public string Dialog10;

    //private List<string> AllDialog;
    //private string currentDialog;
    //private int dialogIndex = 0;

    [SerializeField] private float typingSpeed = 0.05f;

    [Header("Dialogue TMP text")]
    public TextMeshProUGUI npcDialogueText;

    [Header("Animaton Controllers")]
    public Animator npcSpeechBubbleAnimator;

    [Header("Dialogue sentences")]
    [TextArea]
    public string[] npcDialogueSentences;

    private int npcIndex;

    private float speechBubbleAnimationDelay = 0.6f;

    public IEnumerator StartDialogue()
    {
        npcSpeechBubbleAnimator.SetTrigger("Open");

        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        StartCoroutine(TypeNPCDialogue());
    }

    private IEnumerator TypeNPCDialogue()
    {

        foreach (char letter in npcDialogueSentences[npcIndex].ToCharArray())
        {
            npcDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    private IEnumerator ContinueNPCDialogue()
    {
        if (npcIndex < npcDialogueSentences.Length - 1)
        {
            npcIndex++;
            if (npcIndex == 0)
            {
                StartCoroutine(StartDialogue());
            }
            else
            {
                npcDialogueText.text = string.Empty;
                npcSpeechBubbleAnimator.SetTrigger("Close");

                npcSpeechBubbleAnimator.SetTrigger("Open");
                yield return new WaitForSeconds(speechBubbleAnimationDelay);

                npcDialogueText.text = string.Empty;
                StartCoroutine(TypeNPCDialogue());
            }
        }
        else
        {
            npcIndex = -1;

            npcDialogueText.text = string.Empty;
            npcSpeechBubbleAnimator.SetTrigger("Close");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //AllDialog = new List<string> { Dialog1, Dialog2, Dialog3, Dialog4, Dialog5, Dialog6, Dialog7, Dialog8, Dialog9, Dialog10 };
        //currentDialog = AllDialog[0];

        npcIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //private string GetCurrentDialog()
    //{
    //    return currentDialog;
    //}
    /*
   // private void advanceDialog()
    //{
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
*/

    public void RequestDialog()
    {
        //string returnDialog = GetCurrentDialog();
        //advanceDialog();
        //return returnDialog;
        StartCoroutine(ContinueNPCDialogue());

    }

}
