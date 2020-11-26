using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAwayTrigger : MonoBehaviour
{
    public dialogBox db;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        db.onWalkAway();
    }
}
