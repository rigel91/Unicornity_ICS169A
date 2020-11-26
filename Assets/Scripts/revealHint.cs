using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class revealHint : MonoBehaviour
{
    public string hintText;
    public TMP_InputField hintBoxText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevealHint()
    {
        hintBoxText.text = hintText;
    }
}
