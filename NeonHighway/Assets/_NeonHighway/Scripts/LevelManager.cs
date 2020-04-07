using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    ///  this manager does the following 
    ///  checks if this level already exsists in local save data 
    ///  if it does great do nothing else 
    ///  if it does not add this level to the list levelDataSets 
    ///  
    /// 
    /// 
    /// </summary>
    // Start is called before the first frame update
    public SaveDataScript.LevelScoreData myLevelScoreData = new SaveDataScript.LevelScoreData();

    void Start()
    {
        // get the name of this level 
         myLevelScoreData.LevelName = ActiveSceneManager.GetSceneName();

        // load score and level data 
        SaveDataScript.Load();

        // chech if this level already exsists in the list of levels 
        bool foundLevel = false;
        
        foreach (SaveDataScript.LevelScoreData levelData in SaveDataScript.MySaveData.levelScoreDataSet)
        {
            if (myLevelScoreData.LevelName == levelData.LevelName)
            {
                foundLevel = true;
                myLevelScoreData = levelData;
                break;
            }
            
        }

        // if we dont find this levels data create a new one
        if (!foundLevel)
        {
            SaveDataScript.MySaveData.levelScoreDataSet.Add(myLevelScoreData);
        }
        // set this level to last completed * change this to last attempted?*
        SaveDataScript.MySaveData.LastCompletedLevel = myLevelScoreData.LevelName;

        // save data 
        SaveDataScript.Save();
    }
}
