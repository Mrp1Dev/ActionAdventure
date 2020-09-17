using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    [SerializeField] List<GameObject> menuGroups;
    [SerializeField] AudioSource buttonAudio;
    [SerializeField] AudioSource buyClickAudio;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void ShowGroup(GameObject group)
    {
        group.SetActive(true);
        foreach(GameObject menuGroup in menuGroups)
        {
            if(menuGroup != group)
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
