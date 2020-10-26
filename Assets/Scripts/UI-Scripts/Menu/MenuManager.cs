using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    [SerializeField] private List<GameObject> menuGroups;
    [SerializeField] private AudioSource buttonAudio;
    [SerializeField] private AudioSource buyClickAudio;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void ShowGroup(GameObject group)
    {
        group.SetActive(true);
        foreach (GameObject menuGroup in menuGroups)
        {
            if (menuGroup != group)
            {
                menuGroup.SetActive(false);
            }
        }
    }

    public void ButtonClickSound()
    {
        buttonAudio.Play();
    }

    public void BuyClickSound()
    {
        buyClickAudio.Play();
    }
}