using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class tutorialTextManager : MonoBehaviour
{
    private BoxCollider2D bc;

    private RectTransform rt;

    public bool openAtStart;

    public int fontSize;
    private TextMeshProUGUI tmp;


    // Start is called before the first frame update
    void Start()
    {
        //bc = this.GetComponent<BoxCollider2D>();
        rt = this.GetComponent<RectTransform>();
        tmp = this.GetComponent<TextMeshProUGUI>();

        if (openAtStart)
        {
            tmp.fontSize = fontSize;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //hide the tutorial text
        if (!openAtStart && collision.tag == "Player")
        {
            //rt.localScale = new Vector3(1, 1, 1);
            tmp.fontSize = fontSize;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //hide the tutorial text
            tmp.fontSize = 0;
        }

    }

}
