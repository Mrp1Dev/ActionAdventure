using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private List<Button> levelButtons;

    private void OnEnable()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            levelButtons[i].interactable = false;
        }
        int levelsUnlocked = SaveManager.SaveData.LevelsUnlocked;
        for (int i = 0; i < levelsUnlocked; i++)
        {
            levelButtons[i].interactable = true;
        }
    }
}