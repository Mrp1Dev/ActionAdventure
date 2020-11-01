using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDialoguesScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> dialogues = default;
    [SerializeField] private AnimationClip dialogueAnim;
    [SerializeField] private float startDelay;

    public event Action IntroDone;

    private void OnEnable()
    {
        StartCoroutine(PlayDialoguesAfterDelay());
    }

    private IEnumerator PlayDialoguesAfterDelay()
    {
        yield return new WaitForSecondsRealtime(startDelay);
        for (int i = 0; i < dialogues.Count; i++)
        {
            dialogues[i].SetActive(true);
            yield return new WaitForSecondsRealtime(dialogueAnim.length);
            dialogues[i].SetActive(false);
        }
        IntroDone?.Invoke();
    }
}