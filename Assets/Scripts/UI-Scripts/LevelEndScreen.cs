using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndScreen : MonoBehaviour
{
    private void OnEnable()
    {
        PauseManager.Instance.DoPauseInput = false;
        PauseManager.Instance.Pause();
    }
}