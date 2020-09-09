using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public SaveData SaveData = new SaveData()
    {
        maxHealth = 100f,
        damage = 20f,
        rangedDamage = 8f,
        levelsUnlocked = 1,
        arrowAmount = new int[] { 10000, 10, 2 }
    };

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    private void Start()
    {

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
}
