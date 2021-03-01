using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onPuzzleComplete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("test the thing!");
        GameObject[] puzzle2Things = GameObject.FindGameObjectsWithTag(this.tag);
        foreach (GameObject g in puzzle2Things)
        {
            if (g.GetComponent<solutionManager>() != null)
            {
                g.GetComponent<solutionManager>().revealTextIfSolved();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
