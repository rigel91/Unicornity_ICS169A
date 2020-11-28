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

    //Flag Dialogue
    //TODO: add the many flag dialogues

    //array of language gameobjects...

    void Awake()
    {
        //data from the csv file - string: row, object: column
        List<Dictionary<string, object>> data = CSVReader.Read("Unicornity Character Sheet.xlsx - Sheet1 (1)");

        //set the variables for the NPC
        for (var i = 0; i < data.Count; i++)
        {
            if ((data[i]["CharacterID"]).ToString() == characterID)
            {
                //set level
                level = (int)data[i]["Level"];
                //print("Level: " + data[i]["Level"]);

                //set language
                string[] language = data[i]["Languages"].ToString().Split(new char[] { '~' });
                for (int j = 0; j < language.Length; j++)
                {
                    languages.Add(language[j]);
                    //print("Languages: " + language[j]);
                }

                //set keywordID
                string[] id = data[i]["KeywordID"].ToString().Split(new char[] { '~' });
                for (int j = 0; j < id.Length; j++)
                {
                    keywordID.Add(System.Convert.ToInt32(id[j]));
                    //print("Keyword ID: " + System.Convert.ToInt32(id[j]));
                }

                //set the dialogue
                string[] sentences = data[i]["Dialogue"].ToString().Split(new char[] { '~' });
                for (int j = 0; j < sentences.Length; j++)
                {
                    dialogue.Add(sentences[j]);
                    //print("Dialogue: " + sentences[j]);
                }

                //set the dialogue
                string[] clues = data[i]["Full Clue"].ToString().Split(new char[] { '~' });
                for (int j = 0; j < clues.Length; j++)
                {
                    fullClue.Add(clues[j]);
                    //print("Full Clues: " + clues[j]);
                }
            }
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
