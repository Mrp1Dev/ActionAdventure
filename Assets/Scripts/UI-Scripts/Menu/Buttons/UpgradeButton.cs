using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UpgradeType { Damage, Health, RangedDamage };
public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private UpgradeDataScriptableObject upgradeSO;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        SaveData save = SaveManager.SaveData;
        UpgradeType type = upgradeSO.UpgradeType;
        if(LevelExists(type, save))
        {
            save.IncrementStatLevel(type, 1);
        }
    }

    bool LevelExists(UpgradeType type, SaveData save)
    {
        int levelLength = upgradeSO.DataList.Count;
        if(save.GetStatLevel(type) + 1 < levelLength)
        {
            return true;
        }
        return false;
    }
}
