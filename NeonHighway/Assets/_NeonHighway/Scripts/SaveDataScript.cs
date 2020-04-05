using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveDataScript : MonoBehaviour
{

    [System.Serializable]
    public class SaveData
    {
        public SaveData()
        {
            LastCompletedLevel = "none";
            //scoreDataset = new List<ScoreData>();
            levelScoreDataSet = new List<LevelScoreData>();

        }
        
        public string LastCompletedLevel;
        //public List<ScoreData> scoreDataset;
        public List<LevelScoreData> levelScoreDataSet;

    }
    [System.Serializable]
    public class ScoreData
    {
        public string scoreName;
        public int scoreValue;
    }
    [System.Serializable]
    public class LevelScoreData
    {
        public string LevelName;
        public List<ScoreData> scoreDataset;
    }
    const string SAVE_DATA_FILENAME = "NeonHighway.json";
    const string SAVE_DATA_BACKUP_FILENAME = "NeonHighwayBackupData.json";

    
    public static SaveData MySaveData;

    public static void Load()
    {
        LoadFromPath(Application.persistentDataPath + "/" + SAVE_DATA_FILENAME);
        if (MySaveData != null)
        {
            //Save succeeded, so save loaded file as backup
            SaveToPath(Application.persistentDataPath + "/" + SAVE_DATA_BACKUP_FILENAME);
        }
        else
        {
            //Couldn't load primary save file, try loading backup
            LoadFromPath(Application.persistentDataPath + "/" + SAVE_DATA_BACKUP_FILENAME);
        }

        if (MySaveData == null)
        {
            //Couldn't load primary or backup save data, create a default save
            MySaveData = new SaveData();
        }
    }

    public static void Save()
    {
        SaveToPath(Application.persistentDataPath + "/" + SAVE_DATA_FILENAME);
    }
    public static void Delete()
    {
        DeleteFromPath(Application.persistentDataPath + "/" + SAVE_DATA_FILENAME);
        DeleteFromPath(Application.persistentDataPath + "/" + SAVE_DATA_BACKUP_FILENAME);
    }
    static void LoadFromPath(string path)
    {
        if (File.Exists(path))
        {
            try
            {
                string jsonString = File.ReadAllText(path);
                MySaveData = JsonUtility.FromJson<SaveData>(jsonString);
            }
            catch (System.Exception e)
            {
                Debug.Log("Failed to load save: " + e.ToString());
                MySaveData = null;
            }
        }
    }

    static void SaveToPath(string path)
    {
        try
        {
            string jsonString = JsonUtility.ToJson(MySaveData);
            File.WriteAllText(path, jsonString);
            Debug.Log("Save Complete! File: " + path);
        }
        catch (System.Exception e)
        {
            Debug.Log("Error saving file: " + e.ToString());
        }
    }

    static void DeleteFromPath(string path)
    {
        try
        {
            File.Delete(path);
            Debug.Log("Delete File Complete!");

        }
        catch (System.Exception e)
        {
            Debug.Log("Error Deleting file" + e.ToString());
        }
    }
}
