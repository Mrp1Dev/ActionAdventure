using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct UpgradeData
{
    public int value;
    public int cost;
}

[CreateAssetMenu(fileName = "UpgradeData", menuName = "ScriptableObjects/UpgradeDataScriptableObject")]
public class UpgradeDataScriptableObject : ScriptableObject
{
    [SerializeField] UpgradeData[] data;
    public IReadOnlyList<UpgradeData> DataList => data;
}
