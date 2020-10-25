using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteTrigger : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteUI = default;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelCompleteUI.SetActive(true);
            if (!(SaveManager.SaveData.LevelsUnlocked > SceneManager.GetActiveScene().buildIndex))
            {
                SaveManager.SaveData.LevelsUnlocked++;
            }
        }
    }
}