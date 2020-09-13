using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataLoader : MonoBehaviour
{
    [SerializeField] private UpgradeDataScriptableObject[] upgradeSO;

    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private PlayerRanged playerRanged;

    private SaveData save;
    // Start is called before the first frame update
    void Awake()
    {
        save = SaveManager.SaveData;
        foreach(var data in upgradeSO)
        {
            switch (data.UpgradeType)
            {
                case UpgradeType.Damage:
                    playerCombat.damage = data.DataList[save.GetStatLevel(data.UpgradeType)].value;
                    break;
                case UpgradeType.Health:
                    playerCombat.DefaultHealth = data.DataList[save.GetStatLevel(data.UpgradeType)].value;
                    break;
                case UpgradeType.RangedDamage:
                    playerRanged.ArrowDamage = data.DataList[save.GetStatLevel(data.UpgradeType)].value;
                    break;
            }
        }
    }

}
