using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class journalNotificationManager : MonoBehaviour
{
    //tracks how many unread clues are present in the journal (this is the number shown on the red badge)
    private int newClues;

    public GameObject redBadge;
    public TextMeshProUGUI numIndicator;


    // Start is called before the first frame update
    void Start()
    {
        newClues = 0;
        hideBadge();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function called by journalUIManage whenever a new clue is added to the journal
    public void incrementNewClues()
    {
        newClues++;
        showBadge();
    }

    //call this the new clues are read (when the journal is opened)
    public void resetNewClues()
    {
        newClues = 0;
        hideBadge();
    }


    //makes the badge appear (called anytime newClues increments above zero)
    private void showBadge()
    {
        redBadge.SetActive(true);
        numIndicator.text = newClues.ToString();
    }

    //makes the badge disappear (called after the journal is opened and the new clues have been seen)
    private void hideBadge()
    {
        redBadge.SetActive(false);
        numIndicator.text = "";
    }


}
