using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DirectGoldShower : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    void Update()
    {
        text.text = SaveManager.SaveData.Gold + "G";
    }
}
