using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataLoader : MonoBehaviour
{
    [SerializeField] private UpgradeDataScriptableObject damageUpgrades;
    [SerializeField] private UpgradeDataScriptableObject maxHealthUpgrades;
    [SerializeField] private UpgradeDataScriptableObject rangedUpgrades;

    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private PlayerRanged playerRanged;

    private SaveManager save;
    // Start is called before the first frame update
    void Awake()
    {
        save = SaveManager.instance;

        playerCombat.damage =
            damageUpgrades.DataList[save.SaveData.DamageLevel].value;

        playerCombat.DefaultHealth =
            maxHealthUpgrades.DataList[save.SaveData.MaxHealthLevel].value;

        playerRanged.ArrowDamage =
            rangedUpgrades.DataList[save.SaveData.RangedDamageLevel].value;
    }

}
