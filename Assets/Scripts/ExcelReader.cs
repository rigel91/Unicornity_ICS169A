using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExcelReader : MonoBehaviour
{
    private TextAsset characterData;

    // Start is called before the first frame update
    void Start()
    {
        characterData = Resources.Load<TextAsset>("Unicornity Character Sheet.xlsx - Sheet1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //gets the level that each NPC is on
    public int GetLevel(string characterID)
    {
        string[] lines = characterData.text.Split(new char[] { '\n' });
        for (int i = 2; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(new char[] { ',' });

            if (cells[0] == characterID)
            {
                return Convert.ToInt32(cells[1]);
            }
        }
        return 0;
    }

    //gets the languages each NPC has; each different language is separated by  a '~' character in the csv excel file
    public List<string> GetLanguageType(string characterID)
    {
        List<string> totalLanguages = new List<string>();

        string[] lines = characterData.text.Split(new char[] { '\n' });
        for (int i = 2; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(new char[] { ',' });

            if (cells[0] == characterID)
            {
                string[] languages = cells[2].Split(new char[] { '~' });
                foreach (string l in languages)
                {
                    totalLanguages.Add(l);
                }
            }
        }
        return totalLanguages;
    }

    //gets the different dialogues of each NPC has; each different dialogue is separated by  a '~' character in the csv excel file
    public List<string> GetDialgoue(string characterID)
    {
        List<string> totalDialogues = new List<string>();

        string[] lines = characterData.text.Split(new char[] { '\n' });
        for (int i = 2; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(new char[] { ',' });

            if (cells[0] == characterID)
            {
                string[] dialogues = cells[5].Split(new char[] { '~' });
                foreach (string d in dialogues)
                {
                    totalDialogues.Add(d);
                }
            }
        }
        return totalDialogues;
    }

    //gets the different dialogue clues of each NPC has; each different dialogue clue is separated by  a '~' character in the csv excel file
    public List<string> GetFullClues(string characterID)
    {
        List<string> totalClues = new List<string>();

        string[] lines = characterData.text.Split(new char[] { '\n' });
        for (int i = 2; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(new char[] { ',' });

            if (cells[0] == characterID)
            {
                string[] clues = cells[6].Split(new char[] { '~' });
                foreach (string c in clues)
                {
                    totalClues.Add(c);
                }
            }
        }
        return totalClues;
    }
}
