
using System.Diagnostics;
using UnityEngine;

public class Clue : Interactable
{
    //"Blueprint" for all clues given.
    // Start is called before the first frame update.

    //Clue Data.
    public string cName = "New Clue";
    public Sprite icon = null;
    public bool clueKnown = false;
    //public Clue clue;

    //Override
    /*public override void Interact()
    {
        base.Interact();
        acquireClue();
    }*/

}
