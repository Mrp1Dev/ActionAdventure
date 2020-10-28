using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialComplete : MonoBehaviour
{
    [SerializeField] private GameObject levelCompletePanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PauseManager.Instance.Pause(false);
            levelCompletePanel.SetActive(true);
        }
    }
}