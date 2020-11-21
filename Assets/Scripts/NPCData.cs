using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    //CharacterID	Level	Languages	KeywordID	KeywordMeaning	Dialog	Full Clue
    public string characterID; //unique id for each NPC; used for the pixeldudesmaker website
    [System.NonSerialized]public int level; //the level that each NPC is located on
    [System.NonSerialized] public List<string> languages = new List<string>(); //the number of languages a NPC can speak; one NPC can have multiple languages

    [System.NonSerialized] public List<int> keywordID = new List<int>(); //can have multiple keywords; separated by ','
    [System.NonSerialized] public string[] keywordMeaning; //since some npcs can have more than one keyword, they must have more than one meaning; separated by ','
    [System.NonSerialized] public string[] dialog; //dialog for journal entries
    [System.NonSerialized] public string fullClue; //dialog for the npc after solving the puzzle

    //set the values from excel
    private ExcelReader reader;

    //array of language gameobjects...

    void Start()
    {
        reader = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ExcelReader>();

        keywordID.Add(reader.GetLevel(characterID));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log(characterID + ", " + keywordID[0]);
        }
    }
}
