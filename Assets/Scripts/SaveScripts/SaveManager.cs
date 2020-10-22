using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public static class SaveManager
{
    [HideInInspector] public static SaveData SaveData = new SaveData();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Awake()
    { 
        Load();
        SaveData.DataUpdated += Save;
    }


    public static void Save()
    {
        Debug.Log("Saved!");
        SaveData.SerializeFields();
        SaveStreamer.SaveGame(SaveData);
    }

    public static void Load()
    {
        SaveData data = SaveStreamer.LoadGame();
        if (data != null)
        {
            SaveData = data;
            SaveData.DeserializeFields();
        }

    }

    public static void Delete()
    {
        SaveStreamer.DeleteSaves();
        SaveData = new SaveData();
        SaveData.DataUpdated += Save;
    }
}
