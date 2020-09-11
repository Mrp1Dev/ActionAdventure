using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UpgradeType { Damage, Health, RangedDamage };
public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private UpgradeDataScriptableObject upgradeData;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if(upgradeData.DataList.Count > SaveManager.instance.SaveData.DamageLevel)
        {
            switch (upgradeType)
            {
                case UpgradeType.Damage:
                    SaveManager.instance.SaveData.DamageLevel++;
                    break;
                case UpgradeType.Health:
                    SaveManager.instance.SaveData.MaxHealthLevel++;
                    break;
                case UpgradeType.RangedDamage:
                    SaveManager.instance.SaveData.RangedDamageLevel++;
                    break;
            }
        }


    }
}
