﻿using System.Collections;
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

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

    }

    void OnClick()
    {
        SaveData save = SaveManager.SaveData;
        UpgradeType type = upgradeSO.UpgradeType;
        if (LevelExists(type, save))
        {
            save.IncrementStatLevel(type, 1);
        }
    }

    bool LevelExists(UpgradeType type, SaveData save)
    {
        int levelLength = upgradeSO.DataList.Count;
        if (save.GetStatLevel(type) + 1 < levelLength)
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (!IsMaxed())
        {
            button.interactable = true;
            costText.text = $"{upgradeSO.DataList[SaveManager.SaveData.GetStatLevel(upgradeSO.UpgradeType) + 1].cost}G";
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
