using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    [SerializeField] private AudioSource music = default;

    private void Start()
    {
        GameCompleteTrigger.TriggerEntered += StopMusic;
    }

    private void OnDestroy()
    {
        GameCompleteTrigger.TriggerEntered -= StopMusic;
    }

    private void StopMusic()
    {
        music.Stop();
    }
}