using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class solutionManager : MonoBehaviour
{
    public solutionBox[] solutionBoxes;

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
            foreach (solutionBox box in solutionBoxes)
            {
                box.lockPosition();
            }
            Debug.Log("puzzle solved!");
        }
    }

    
}
