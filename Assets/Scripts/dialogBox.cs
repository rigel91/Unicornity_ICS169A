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

    //npc reads the full clue dialogue without translations
    [Header("Dialogue sentences")]
    [TextArea]
    //public string[] npcDialogueSentences;
    //rigel edited this so that the NPC can type the letters in the speech bubble text
    private List<string> npcDialogueSentences;

    [Header("Journal clues")]
    [TextArea]
    public string[] npcDialogueClues;

    public GameObject hintSentence;

    private int npcIndex;

    public string translatedText;
    //private List<string> translatedText;

    public List<GameObject> foreignWordPopups;

    private float speechBubbleAnimationDelay = 0.6f;

    private bool dialogClosed;
    public bool dialogTransitioning;
    private bool bubbleTransitioning;

    //Getting NPC data
    private NPCData npcData;

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
        if (npcIndex < npcDialogueSentences.Count - 1)
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
        if (!dialogClosed)
        {
            npcIndex = -1;
            bubbleTransitioning = true;
            StartCoroutine(closeDialogBox());
            bubbleTransitioning = false;
        }

    }

    private float getTypingDelay()
    {
        return typingSpeed * npcDialogueSentences[npcIndex].ToCharArray().Length;
    }

    private float waitForTyping(float timer) //not in use
    {
        return timer - Time.deltaTime;
    }

    private IEnumerator waitBubbleAnimation()
    {
        yield return new WaitForSeconds(speechBubbleAnimationDelay);
    }

    private IEnumerator openDialogBox()
    {
        foreach (GameObject fwp in foreignWordPopups)
        {
            fwp.GetComponent<animatePopup>().playAnimation();
        }
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
        //rigel edited this part so that this script can get the NPCs dialogue
        //Getting NPC data
        npcData = gameObject.GetComponent<NPCData>();

        //set npc dialogue before puzzle
        List<string> sentenceTotal = new List<string>();
        for (int i = 0; i < npcData.fullClue.Count; i++)
        {
            string total = npcData.fullClue[i];
            for (int j = 0; j < npcData.keywordID.Count; j++)
            {
                if (npcData.keywordID[j] == -1)
                {
                    total = "...";
                }
                else
                {
                    //npcData.GetWordDefinition(npcData.keywordID[j]), npcData.GetWord(npcData.keywordID[j])
                    total = total.Replace(npcData.GetWordDefinition(npcData.keywordID[j]), npcData.GetWord(npcData.keywordID[j]));
                    //print("Replace word: " + npcData.GetWordDefinition(npcData.keywordID[j]) + " with: " + npcData.GetWord(npcData.keywordID[j]));
                }
            }
            //print(total);
            sentenceTotal.Add(total);
        }
        npcDialogueSentences = sentenceTotal;

        npcIndex = -1;
        dialogClosed = true;
    }

    void Update()
    {

    }

    public string[] RequestDialog()
    {
        ContinueNPCDialogue();
        if (npcIndex >= 0)
        {
            return npcDialogueClues;
        }
        return new string[] { };
    }

    public bool checkRepeatRequest()
    {
        return dialogTransitioning;
    }

    public void translateText() //currently this will not work if there is more than one sentence in the same speech bubble
    {
        npcDialogueSentences[0] = translatedText;
    }

    //this puts the symbol and hint word on the right side of the journal(ex: skfh --- the sky)
    public void revealHintSentence()
    {
        hintSentence.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 50);
    }
}
