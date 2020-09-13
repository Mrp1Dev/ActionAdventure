using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeTextSetter : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private UpgradeDataScriptableObject upgradeSO;

    // Update is called once per frame
    void Update()
    {
        UpgradeType type = upgradeSO.UpgradeType;
        float currentValue = upgradeSO.DataList[SaveManager.SaveData.GetStatLevel(type)].value;
        if (IsMaxed())
        {
            text.text = currentValue.ToString();
        }
        else
        {
            float upgradedValue = upgradeSO.DataList[SaveManager.SaveData.GetStatLevel(type) + 1].value;
            text.text = $"{currentValue} > {upgradedValue}";
        }

    }

    bool IsMaxed()
    {
        if (SaveManager.SaveData.GetStatLevel(upgradeSO.UpgradeType) + 1
            >= upgradeSO.DataList.Count)
        {
            return true;
        }
        return false;
    }
}
