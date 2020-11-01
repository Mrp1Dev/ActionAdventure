using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntro : MonoBehaviour
{
    [SerializeField] private IntroDialoguesScript introDialogues = default;

    private void OnEnable()
    {
        if (SaveManager.SaveData.IntroDone) IntroCompleted();

        introDialogues.IntroDone += IntroCompleted;
    }

    private void OnDisable()
    {
        introDialogues.IntroDone -= IntroCompleted;
    }

    private void IntroCompleted()
    {
        SaveManager.SaveData.IntroDone = true;
        gameObject.SetActive(false);
    }
}