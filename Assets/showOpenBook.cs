using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class showOpenBook : MonoBehaviour
{
    public Sprite closedBook;
    public Sprite openedBook;

    private Image journalImage;

    public JournalUIManage journalManager;

    private void IfMouseOverJournal()
    {
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = Input.mousePosition;

        List<RaycastResult> rrl = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, rrl);
        for (int i = 0; i < rrl.Count; i++)
        {
            if (rrl[i].gameObject.tag != "Journal Button")
            {
                rrl.RemoveAt(i);
                i--;
            }
        }

        if (rrl.Count > 0)
        {
            openBook();
        }
        else
        {
            closeBook();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        journalImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        IfMouseOverJournal();

        print(EventSystem.current.IsPointerOverGameObject());
    }

    private void closeBook()
    {
        if (!JournalUIManage.GameIsPaused)
        {
            journalImage.sprite = closedBook;
        }
    }

    private void openBook()
    {
        journalImage.sprite = openedBook;
    }
}
