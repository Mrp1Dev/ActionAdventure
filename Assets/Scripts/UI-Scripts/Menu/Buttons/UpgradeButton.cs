using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum UpgradeType { Damage, Health, RangedDamage };
public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private UpgradeDataScriptableObject upgradeSO;
    [SerializeField] private TMP_Text costText;
    private Button button;
    private int cost;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        cost = upgradeSO.DataList[SaveManager.SaveData.GetStatLevel(upgradeSO.UpgradeType) + 1].cost;
    }

    void OnClick()
    {
        SaveData save = SaveManager.SaveData;
        UpgradeType type = upgradeSO.UpgradeType;
        save.IncrementStatLevel(type, 1);
        save.Gold -= cost;
    }

    private void Update()
    {
        if (!IsMaxed())
        {            
            button.interactable = SaveManager.SaveData.Gold >= cost;
            costText.text = $"{cost}G";
        }
        else
        {
            button.interactable = false;
            costText.text = "MAX";
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
