using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayIntroButton : MonoBehaviour
{
    [SerializeField] private GameObject gameIntroPanel;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        SaveManager.SaveData.IntroDone = false;
        gameIntroPanel.SetActive(true);
    }
}