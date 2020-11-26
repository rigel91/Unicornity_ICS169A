using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    //CharacterID	Level	Languages	KeywordID	KeywordMeaning	Dialog	Full Clue
    public string characterID; //unique id for each NPC; used for the pixeldudesmaker website
    [System.NonSerialized] public int level; //the level that each NPC is located on
    [System.NonSerialized] public List<string> languages = new List<string>(); //the number of languages a NPC can speak; one NPC can have multiple languages

    //might need to change this
    [System.NonSerialized] public List<int> keywordID = new List<int>(); //can have multiple keywords; separated by ','
    [System.NonSerialized] public string[] keywordMeaning; //since some npcs can have more than one keyword, they must have more than one meaning; separated by ','
    
    [System.NonSerialized] public List<string> dialogue = new List<string>(); //dialog for journal entries
    [System.NonSerialized] public List<string> fullClue = new List<string>(); //dialog for the npc after solving the puzzle

    //set the values from excel
    private ExcelReader reader;

    //array of language gameobjects...

    void Start()
    {
        reader = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ExcelReader>();

        //gets the level
        level = reader.GetLevel(characterID);
        //gets the languages
        languages = reader.GetLanguageType(characterID);

        //TODO: figure out keywordID and keywordMeaning

        //gets the dialog
        dialogue = reader.GetDialgoue(characterID);
        //gets full clues
        fullClue = reader.GetFullClues(characterID);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ////prints level
            //Debug.Log("NPC: " + characterID + "; level: " + level);
            ////prints each language
            //foreach(string l in languages)
            //{
            //    Debug.Log("NPC: " + characterID + "; languages: " + l);
            //}
            //prints each dialogue
            //foreach (string d in dialogue)
            //{
            //    Debug.Log("NPC: " + characterID + "; dialgoue: " + d);
            //}
            foreach (string c in fullClue)
            {
                Debug.Log("NPC: " + characterID + "; clue: " + c);
            }
        }
    }
}
