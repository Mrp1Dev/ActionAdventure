using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveStreamer : MonoBehaviour
{
    public static void SaveGame(SaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(GetPath(), FileMode.OpenOrCreate);
        formatter.Serialize(file, data);
        file.Close();
    }

    public static SaveData LoadGame()
    {
        if(File.Exists(GetPath()))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(GetPath(), FileMode.Open);
            SaveData returnSave = formatter.Deserialize(file) as SaveData;
            file.Close();
            return returnSave;
        }

        return null;
    }

    public static void DeleteSaves()
    {

    }

    private static string GetPath()
    {
        return $"{Application.persistentDataPath}/data";
    }
}
