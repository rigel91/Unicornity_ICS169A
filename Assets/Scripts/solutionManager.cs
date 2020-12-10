using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class solutionManager : MonoBehaviour
{
    public solutionBox[] solutionBoxes;
    public dialogBox[] dialogBoxesOnLevel;

    private bool puzzleSolved = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkAllSolutions()
    {
        bool allCorrect = true;
        foreach (solutionBox box in solutionBoxes)
        {
            if (!box.isCorrect())
            {
                allCorrect = false;
            }
        }
        if (allCorrect)
        {
            puzzleSolved = true;

            foreach (solutionBox box in solutionBoxes)
            {
                box.lockPosition();
                revealAllText();
            }
            //Debug.Log("puzzle solved!");
        }
    }

    private void revealAllText()
    {
        foreach (dialogBox db in dialogBoxesOnLevel)
        {
            db.translateText();
        }
    }

    public bool isPuzzleSolved()
    {
        return puzzleSolved;
    }

    
}
