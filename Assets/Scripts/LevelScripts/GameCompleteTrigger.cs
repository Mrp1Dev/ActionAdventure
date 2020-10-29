using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleteTrigger : MonoBehaviour
{
    [SerializeField] private GameObject completeUIPanel = default;

    public static Action TriggerEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PauseManager.Instance.Pause();
        TriggerEntered?.Invoke();
        completeUIPanel.SetActive(true);
    }
}