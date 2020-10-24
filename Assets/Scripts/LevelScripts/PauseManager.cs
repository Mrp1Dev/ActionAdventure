using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }
    public bool DoPauseInput { get; set; } = true;

    private bool paused;
    public bool Paused => paused;

    [SerializeField] private GameObject pauseUIPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"Singleton {this.name} already exsists!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UnPause();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && DoPauseInput)
        {
            if (paused)
            {
                UnPause(true);
            }
            else
            {
                Pause(true);
            }
        }
    }

    public void Pause(bool withUIPanel = false)
    {
        Time.timeScale = 0f;
        paused = true;
        if (withUIPanel) pauseUIPanel.SetActive(true);
    }

    public void UnPause(bool withUIPanel = false)
    {
        Time.timeScale = 1;
        paused = false;
        if (withUIPanel) pauseUIPanel.SetActive(false);
    }
}