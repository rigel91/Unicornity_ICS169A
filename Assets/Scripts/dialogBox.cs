using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogBox : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;

    public GameObject pressEPrompt;
    public bool hasUnreadDialog;



    [Header("Dialogue TMP text")]
    public TextMeshProUGUI npcDialogueText;

    //public RectTransform OverlayDBRectTransform;

    private RectTransform OverlayDBRectTransform;

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
    [SerializeField] private string[] npcDialogueClues = new string[10]; //max 10 hints per NPC

    public string[] workaroundDialogClues = new string[10]; //this is a temporary workaround i am using to let me spawn duplicate words

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
    public bool close;

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
        close = false;

        bubbleTransitioning = true;
        StartCoroutine(openDialogBox());
        bubbleTransitioning = false;
    }

    private void requestClose()
    {
        //close = true;

        hideSpeechPrompt();

        if (npcSpeechBubbleAnimator == null)
        {
            npcIndex = -1;
            //close box immediately (no animation right now)
            OverlayDBRectTransform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            dialogClosed = true;
        }
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
            //fwp.GetComponent<animatePopup>().playAnimation(); disabled for now
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
        close = false;

        hasUnreadDialog = true;
        hideSpeechPrompt();

        OverlayDBRectTransform = GameObject.FindWithTag("Bottom Dialog Box").GetComponent<RectTransform>();

        //rigel edited this part so that this script can get the NPCs dialogue
        //Getting NPC data
        npcData = gameObject.GetComponent<NPCData>();


        //set the clue word automatically by reading from NPCData
        List<Dictionary<string, object>> definition = CSVReader.Read("Unicornity Language Translation Sheet.xlsx - Sheet1");

        int clueIndex = 0;
        for (int i = 0; i < npcData.keywordID.Count; i++)
        {
            for (int j = 0; j < definition.Count; j++)
            {
                if (System.Convert.ToInt32(definition[j]["Index"]) == npcData.keywordID[i])
                {
                    npcDialogueClues[clueIndex] = definition[j]["Word"].ToString();
                    clueIndex++;
                }
            }
        }

        //set npc dialogue before puzzle
        string dot = "...";
        List<string> sentenceTotal = new List<string>();
        for (int i = 0; i < npcData.fullClue.Count; i++)
        {
            string total = npcData.fullClue[i];

            if (npcData.keywordID[i] == -1)
            {
                total = "...";
            }
            sentenceTotal.Add(total);
            

                //for (int j = 0; j < npcData.keywordID.Count; j++)
                //{

                //    if (npcData.keywordID[j] == -1)
                //    {
                //        total = "...";
                //    }
                //    else
                //    {
                //        //npcData.GetWordDefinition(npcData.keywordID[j]), npcData.GetWord(npcData.keywordID[j])
                //        if (j == 0)
                //        {
                //            total = dot + npcData.GetWord(npcData.keywordID[j]) + dot;
                //        }
                //        if (j != 0)
                //        {
                //            total += dot + npcData.GetWord(npcData.keywordID[j]) + dot; //Erol edited this to add rather than assigning
                //        }
                //        //print("Replace word: " + npcData.GetWordDefinition(npcData.keywordID[j]) + " with: " + npcData.GetWord(npcData.keywordID[j]));
                //    }
                //}
                //print(total);
            
        }
        if (npcData.characterID == "God")
        {
            sentenceTotal.Add("Hello, I am God!");
            sentenceTotal.Add("Investigate the Tower and its power.");
            sentenceTotal.Add("You will come across different languages and people.");
            sentenceTotal.Add("Take this journal to help you find out who is behind the Towers power.");
            sentenceTotal.Add("Jump off the edge when you are done.");
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
        if (dialogTransitioning)
        {
            skipDialog();
        }

        else
        {
            if (npcSpeechBubbleAnimator == null)
            {
                ContinueNPCDialogue2();
                if (npcIndex >= 0)
                {
                    //return npcDialogueClues;
                    return workaroundDialogClues;
                }
            }
            else
            {
                ContinueNPCDialogue();
                if (npcIndex >= 0)
                {
                    //return npcDialogueClues;
                    return workaroundDialogClues;
                }
            }

        }

        return new string[] { };



    }

    private void ContinueNPCDialogue2()
    {
        if (npcIndex < npcDialogueSentences.Count - 1)
        {
            npcIndex++;
            if (dialogClosed)
            {
                //open box immediately (no animation right now)
                OverlayDBRectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                dialogClosed = false;
            }
             //StartCoroutine(nextDialogBox());
             npcDialogueText.text = string.Empty;
             dialogTransitioning = true;
             StartCoroutine(TypeNPCDialogue());
        }
        else
        {
            hasUnreadDialog = false;
            hideSpeechPrompt();

            npcIndex = -1;
            //close box immediately (no animation right now)
            OverlayDBRectTransform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            dialogClosed = true;

            close = true;
        }
    }

    public bool checkRepeatRequest()
    {
        return dialogTransitioning;
    }

    public void translateText() //currently this will not work if there is more than one sentence in the same speech bubble
    {
        resetSpeechPrompt();

        List<Dictionary<string, object>> definition = CSVReader.Read("Unicornity Character Sheet.xlsx - Sheet1 (2)");

        for (int i = 0; i < definition.Count; i++)
        {
            if (definition[i]["CharacterID"].ToString() == npcData.characterID)
            {
                //definition[i]["Dialogue"].ToString();
                string solvedDialog = definition[i]["Dialogue"].ToString();
                string[] subs = solvedDialog.Split('~');
                int j = 0;
                foreach (string sub in subs)
                {
                    if(j < npcDialogueSentences.Count)
                    {
                        npcDialogueSentences[j] = sub;
                    }
                    else
                    {
                        npcDialogueSentences.Add(sub);
                    }
                    j++;
                }
                //print(solvedDialog);
                //npcDialogueSentences[0] = solvedDialog;

                //int j = 0;
                //foreach (var subDialog in solvedDialog)
                //{
                //    npcDialogueSentences[j] = subDialog;
                //    j++;
                //}
                //for (int j = 0; j < solvedDialog.Length; j++)
                //{
                //    npcDialogueSentences[j] = solvedDialog[j];
                //}
            }
        }
    }

    //this puts the symbol and hint word on the right side of the journal(ex: skfh --- the sky)
    public void revealHintSentence()
    {
        if (hintSentence != null)
        {
            hintSentence.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 0.2426f);
        }
    }

    private void skipDialog()
    {
        dialogTransitioning = false;
        requestClose();
        npcIndex += 1;

        if (npcIndex < npcDialogueSentences.Count - 1)
        {
            RequestDialog();
        }
        else{
            npcIndex = -1;
        }
    }

    private void hideSpeechPrompt()
    {
        if (pressEPrompt != null)
        {
            pressEPrompt.SetActive(false);
        }   
    }

    private void resetSpeechPrompt()
    {
        if (pressEPrompt != null)
        {
            pressEPrompt.SetActive(true);
            hasUnreadDialog = true;
        }
    }

    //called when the player walks in range of the NPC
    public void showSpeechPrompt()
    {
        if (pressEPrompt != null)
        {
            if (hasUnreadDialog)
            {
                pressEPrompt.SetActive(true);
            }
        }
    }
}
