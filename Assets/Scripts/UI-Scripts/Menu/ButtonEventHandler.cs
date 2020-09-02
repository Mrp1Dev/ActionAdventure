using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MenuType { Main, Options, About, LevelSelect}

public class ButtonEventHandler : MonoBehaviour
{

    private event Action<MenuType> DisableOthers;
    [SerializeField] MenuType menuGroup;

    private void Awake()
    {
        this.DisableOthers += MenuTypeCheck;
    }

    void MenuTypeCheck(MenuType menuType)
    {
        if (true) { }
    }

    public void PlayButtonClicked()
    {
        Debug.Log("Clicked play!");
    }

    public void OptionsButtonClicked()
    {
        DisableOthers(menuGroup);
    }

    
}
