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
    //[System.NonSerialized] public string[] keywordMeaning; //since some npcs can have more than one keyword, they must have more than one meaning; separated by ','
    
    [System.NonSerialized] public List<string> dialogue = new List<string>(); //dialog for journal entries
    [System.NonSerialized] public List<string> fullClue = new List<string>(); //dialog for the npc after solving the puzzle

    //Flag Dialogue
    //TODO: add the many flag dialogues

    //Index	   Language Type	 Word	  Definition
    //[System.NonSerialized] public int indexID;
    //[System.NonSerialized] public string languageType;
    //[System.NonSerialized] public string word;
    //[System.NonSerialized] public string wordDefinition;

    void Awake()
    {
        //data from the character sheet csv file - string: row, object: column
        List<Dictionary<string, object>> data = CSVReader.Read("Unicornity Character Sheet.xlsx - Sheet1 (2)");

        //set the variables for the NPC from the data dictionary
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
                    if (id[j] == "NULL")
                    {
                        keywordID.Add(-1);
                    }
                    else
                    {
                        keywordID.Add(System.Convert.ToInt32(id[j]));
                        //print("Keyword ID: " + System.Convert.ToInt32(id[j]));
                    }
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

    public string GetWordDefinition(int index)
    {
        //dictionary from the word/meaning sheet csv file - string: row, object: column
        List<Dictionary<string, object>> definition = CSVReader.Read("Unicornity Language Translation Sheet.xlsx - Sheet1");

        //set the variables for the dictionary for words/meaning
        for (int j = 0; j < definition.Count; j++)
        {
            if (System.Convert.ToInt32(definition[j]["Index"]) == index)
            {
                return definition[j]["Definition"].ToString();
            }
        }
        return "";
    }

    public string GetWord(int index)
    {
        //dictionary from the word/meaning sheet csv file - string: row, object: column
        List<Dictionary<string, object>> definition = CSVReader.Read("Unicornity Language Translation Sheet.xlsx - Sheet1");

        //set the variables for the dictionary for words/meaning
        for (int j = 0; j < definition.Count; j++)
        {
            if (System.Convert.ToInt32(definition[j]["Index"]) == index)
            {
                return definition[j]["Word"].ToString();
            }
        }
        return "";
    }

    public string GetLanguageType(int index)
    {
        //dictionary from the word/meaning sheet csv file - string: row, object: column
        List<Dictionary<string, object>> definition = CSVReader.Read("Unicornity Language Translation Sheet.xlsx - Sheet1");

        //set the variables for the dictionary for words/meaning
        for (int j = 0; j < definition.Count; j++)
        {
            if (System.Convert.ToInt32(definition[j]["Index"]) == index)
            {
                return definition[j]["Language Type"].ToString();
            }
        }
        return "";
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
