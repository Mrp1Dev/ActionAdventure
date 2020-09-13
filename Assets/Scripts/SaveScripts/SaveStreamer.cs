using System.Collections;
using System.IO;
using UnityEngine;

public static class SaveStreamer
{
    public static void SaveGame(SaveData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(GetPath(), jsonData);
    }

    public static SaveData LoadGame()
    {
        if(File.Exists(GetPath()))
        {
            return JsonUtility.FromJson<SaveData>(File.ReadAllText(GetPath()));
        }

        return null;
    }

    public static void DeleteSaves()
    {
        File.Delete(GetPath());
    }

    private static string GetPath()
    {
        return $"{Application.persistentDataPath}/data";
    }
}
