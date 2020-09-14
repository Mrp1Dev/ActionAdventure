using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    [NonSerialized] private Dictionary<UpgradeType, int> statLevels = new Dictionary<UpgradeType, int>();

    [SerializeField] private List<UpgradeType> upgradeTypes = new List<UpgradeType>();
    [SerializeField] private List<int> levelValues = new List<int>();
    [SerializeField] private int gold = 0;
    [SerializeField] private int levelsUnlocked = 1;
    [SerializeField] private int[] arrowAmount = new int[] { 10000, 10, 2 };

    public event Action DataUpdated;

    public int LevelsUnlocked
    {
        get { return levelsUnlocked; }
        set
        {
            levelsUnlocked = value;
            DataUpdated?.Invoke();
        }
    }

    public int[] ArrowAmount
    {
        get { return arrowAmount; }
    }

    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            DataUpdated?.Invoke();
        }
    }

    public void SetArrowAmount(int index, int increment)
    {
        arrowAmount[index] += increment;
        DataUpdated?.Invoke();
    }

    public void IncrementStatLevel(UpgradeType type, int increment)
    {
        if (!statLevels.ContainsKey(type)) { statLevels.Add(type, 0); }
        statLevels[type] += increment;
        DataUpdated?.Invoke();
    }

    public int GetStatLevel(UpgradeType type)
    {
        if (!statLevels.ContainsKey(type)) { statLevels.Add(type, 0); }
        return statLevels[type];
    }

    public void SerializeFields()
    {
        foreach (UpgradeType type in statLevels.Keys)
        {
            upgradeTypes.Add(type);
        }

        foreach (int level in statLevels.Values)
        {
            levelValues.Add(level);
        }
    }

    public void DeserializeFields()
    {
        for (int i = 0; i < levelValues.Count; i++)
        {
            if (!statLevels.ContainsKey(upgradeTypes[i]))
                statLevels.Add(upgradeTypes[i], levelValues[i]);
            statLevels[upgradeTypes[i]] = levelValues[i];
        }
    }
}
