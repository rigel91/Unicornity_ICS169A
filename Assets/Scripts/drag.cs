using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class drag : EventTrigger
{
    private bool dragging;
    private bool needToSnap;

    private Vector2 newPos = new Vector2();
    private bool posLocked;

    // Start is called before the first frame update
    void Start()
    {
        needToSnap = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!posLocked)
        {
            dragging = true;
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!posLocked)
        {
            finalizePosition();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        snapPosition S = collision.GetComponent<snapPosition>();
        if (S) //checks that the object collided with has a snap position before assuming this needs to be snapped to
        {
            Vector3 snapPos = S.getSnapPosition().position;
            newPos.x = snapPos.x;
            newPos.y = snapPos.y;
            needToSnap = true;
        }
        solutionBox B = collision.GetComponent<solutionBox>();
        if (B)
        {
            B.check(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        snapPosition S = collision.GetComponent<snapPosition>();
        if (S) //checks that the object collided with has a snap position before assuming this needs to be snapped to
        {
            needToSnap = false;
        }
        solutionBox B = collision.GetComponent<solutionBox>();
        if (B)
        {
            B.emptyBox();
        }
    }

    private void finalizePosition()
    {
        dragging = false;

        if (needToSnap)
        {
            transform.position = newPos;
        }
    }

    public void LockPosition()
    {
        finalizePosition();
        posLocked = true;
    }



}
