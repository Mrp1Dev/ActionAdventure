using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData 
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float rangedDamage = 8f;
    [SerializeField] private int levelsUnlocked = 1;
    [SerializeField] private int[] arrowAmount = new int[] { 10000, 10, 2 };

    public event Action DataUpdated;

    public float Damage
    {
        get { return damage; }
        set
        {
            damage = value;
            DataUpdated?.Invoke();
        }
    }
    public float MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
            DataUpdated?.Invoke();
        }
    }
    public float RangedDamage
    {
        get { return rangedDamage; }
        set
        {
            rangedDamage = value;
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
