using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    /*
    This script gets the info from each excel(csv file) sheet.  There is one excel sheet called: "Unicornity Character Sheet.xlsx - Sheet1 (2)" which is the csv 
    file that reads each of the characters information and puts them in the public variables like below.  For some NPC's they can have multiple sentences, keywwords, 
    definitions of those keywords, etc so I made them into a list of strings or ints and at run time(when you first hit the start button) each of these 
    public variables gets assigned from the character csv file.

    Another script that this file reads is the "Unicornity Language Translation Sheet.xlsx - Sheet1" file and this file is like the dictionary of the languages;
    they provide an index for each language, a language type, the untranslated word(the symbols of some language), and the defined words of each symbol.  Instead
    of assigning these to an individual variable, the main purpose is to use this script to use the functions to get certain data from the csv file.  Funtions like
    GetWord(index number) gets the undefined/untranslated word from the translation sheet file and needs the words unique index number to get access to the word.

    Each of these csv files are located in the Resources folder under Assets.
     */

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
        //List<Dictionary<string, object>> data = CSVReader.Read("Unicornity Character Sheet.xlsx - Sheet1 (2)"); //old csv file
        List<Dictionary<string, object>> data = CSVReader.Read("Unicornity Character Sheet.xlsx - Sheet1 (2).xlsx - Unicornity Character Sheet.xlsx (1)");

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
                    //some of the NPC's dont use an untranslated word in their dialogue, so the keywordID variable gets set to -1
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
                    //print("Code: " + data[i]["CharacterID"].ToString() + " Full Clues: " + clues[j]);
                }
            }
        }        
    }

    //gets the defined word from the unique index number
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
    //gets the defined word from the untranslated word/symbol
    public string GetWordDefinition(string word)
    {
        //dictionary from the word/meaning sheet csv file - string: row, object: column
        List<Dictionary<string, object>> definition = CSVReader.Read("Unicornity Language Translation Sheet.xlsx - Sheet1");

        //set the variables for the dictionary for words/meaning
        for (int j = 0; j < definition.Count; j++)
        {
            if (definition[j]["Word"].ToString() == word)
            {
                return definition[j]["Definition"].ToString();
            }
        }
        return "";
    }

    //gets the translated word from the unique index number
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

    //gets the language type from the unique index number of each word
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
