using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialLevelButton : MonoBehaviour
{
    [SerializeField] private int levelToOpen;
    [SerializeField] private GameObject transition;// the black image that slowly goes up in darkness.
    private bool hasTransitionStarted = false;

    public void OnClick()
    {
        transition.GetComponent<Animator>().SetTrigger("CallTransition");
        hasTransitionStarted = true;
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void Update()
    {
        if (hasTransitionStarted)
        {
            if (transition.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                SceneManager.LoadScene(levelToOpen);
            }
        }
    }
}