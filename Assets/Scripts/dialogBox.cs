using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogBox : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;

    [Header("Dialogue TMP text")]
    public TextMeshProUGUI npcDialogueText;

    [Header("Animaton Controllers")]
    public Animator npcSpeechBubbleAnimator;

    [Header("Dialogue sentences")]
    [TextArea]
    public string[] npcDialogueSentences;

    [Header("Journal clues")]
    [TextArea]
    public string[] npcDialogueClues;

    public int npcIndex;

    private float speechBubbleAnimationDelay = 0.6f;

    private bool dialogClosed;
    public bool dialogTransitioning;
    private bool bubbleTransitioning;

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
            if (dialogTransitioning == true)
            {
                npcDialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        dialogTransitioning = false;
        

    }

    private void ContinueNPCDialogue()
    {
        if (npcIndex < npcDialogueSentences.Length - 1)
        {
            npcIndex++;
            if (!dialogClosed)
            {
                //StartCoroutine(nextDialogBox());
                npcDialogueText.text = string.Empty;
                dialogTransitioning = true;
                StartCoroutine(TypeNPCDialogue());
            }
            else
            {
                if (!bubbleTransitioning)
                {
                    requestOpen();
                }
            }
        }
        else
        {
            requestClose();
        }
    }

    private void requestOpen()
    {
        bubbleTransitioning = true;
        StartCoroutine(openDialogBox());
        bubbleTransitioning = false;
    }

    private void requestClose()
    {
        npcIndex = -1;
        bubbleTransitioning = true;
        StartCoroutine(closeDialogBox());
        bubbleTransitioning = false;
    }

    private IEnumerator openDialogBox()
    {
        dialogTransitioning = true;

        npcSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        npcDialogueText.text = string.Empty;
        StartCoroutine(TypeNPCDialogue());

        dialogClosed = false;
        //dialogTransitioning = false;
    }

    private IEnumerator nextDialogBox()
    {
        dialogTransitioning = true;
        npcDialogueText.text = string.Empty;
        npcSpeechBubbleAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        npcSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        npcDialogueText.text = string.Empty;
        
        StartCoroutine(TypeNPCDialogue());

        dialogClosed = false;
    }

    private IEnumerator closeDialogBox()
    {
        dialogTransitioning = true;
        npcDialogueText.text = string.Empty;

        npcSpeechBubbleAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        
        dialogClosed = true;
        dialogTransitioning = false;
    }

    public void onWalkAway()
    {
        requestClose();
    }

    // Start is called before the first frame update
    void Start()
    {
        npcIndex = -1;
        dialogClosed = true;
    }

    void Update()
    {

    }

    public string RequestDialog()
    {
        ContinueNPCDialogue();
        if (npcIndex >= 0)
        {
            return npcDialogueClues[npcIndex];
        }
        return "";
    }

    public bool checkRepeatRequest()
    {
        return dialogTransitioning;
    }

}
