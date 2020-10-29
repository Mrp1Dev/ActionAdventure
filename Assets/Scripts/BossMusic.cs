using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    [SerializeField] private AudioSource music = default;

    private void Awake()
    {
        GameCompleteTrigger.TriggerEntered += () => music.Stop();
    }
}