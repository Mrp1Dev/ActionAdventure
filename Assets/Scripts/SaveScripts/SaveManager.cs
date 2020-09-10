using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    [HideInInspector] public SaveData SaveData = new SaveData();

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
        SaveData.DataUpdated += Save;

    }

    void OnDestroy()
    {
        instance = null;
    }

    public void Save()
    {
        SaveStreamer.SaveGame(SaveData);
    }

    public void Load()
    {
        SaveData data = SaveStreamer.LoadGame();
        if (data != null)
        {
            SaveData = data;

        }

    }

    public void Delete()
    {
        SaveStreamer.DeleteSaves();
        SaveData = new SaveData();
    }
}
