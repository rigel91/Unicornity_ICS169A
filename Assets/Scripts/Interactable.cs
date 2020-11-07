
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Base Class for all things "Interactable."
    // As Of Now... Just NPC's who give clues.
    //Apply script to any NPC that will update the Journal.

    //NPC distance of recognition
    public float radius = 3f;

    //For Interaction
    public Rigidbody2D focus;

    //Player
    private Rigidbody2D player;

    //Access to Clue data
    public Clue clue;

    public virtual void Interact ()
    {
        //Meant for later use with other "Interactables."
        //Meant to be overwritten.
    }

    //Visible Wire-frame
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Base of player/NPC interaction? - DA 11/6 
        player = collision.GetComponent<Rigidbody2D>();

        if (player != null)
        {
            SetFocus(player);
            //Testing Add() clue here.. not final 11/6
            acquireClue();
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        //Stop focusing
        RemoveFocus();
    }

    //NPC interaction recoginition
    void SetFocus(Rigidbody2D newFocus)
    {
        focus = newFocus;

        //'E' Prompt here for talking NPC's
    }

    //NPC interaction DE-recognition
    void RemoveFocus()
    {
        focus = null;

        //Also stop "Press e" notification.?

    }

    //Clue Behaviour
    void acquireClue()
    {
        Debug.Log("Learning about " + clue.cName);
        JournalUIManage.instance.Add(clue);
        clue.clueKnown = true;
    }
}
