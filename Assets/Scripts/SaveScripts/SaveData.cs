using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData 
{
    [SerializeField] private int damageLevel = 1;
    [SerializeField] private int maxHealthLevel = 1;
    [SerializeField] private int rangedDamageLevel = 1;
    [SerializeField] private int levelsUnlocked = 1;
    [SerializeField] private int[] arrowAmount = new int[] { 10000, 10, 2 };

    public event Action DataUpdated;

    public int DamageLevel
    {
        get { return damageLevel; }
        set
        {
            damageLevel = value;
            DataUpdated?.Invoke();
        }
    }
    public int MaxHealthLevel
    {
        get { return maxHealthLevel; }
        set
        {
            maxHealthLevel = value;
            DataUpdated?.Invoke();
        }
    }
    public int RangedDamageLevel
    {
        get { return rangedDamageLevel; }
        set
        {
            rangedDamageLevel = value;
            DataUpdated?.Invoke();
        }
    }
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

    public void SetArrowAmount(int index, int increment)
    {
        arrowAmount[index] += increment;
        DataUpdated?.Invoke();
    }


}
