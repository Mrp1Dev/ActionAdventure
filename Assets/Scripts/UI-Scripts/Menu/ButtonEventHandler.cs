using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MenuGroup { Menu, Options, About, LevelSelect, Shop}

public class ButtonEventHandler : MonoBehaviour
{
    public static List<ButtonEventHandler> allMenus = new List<ButtonEventHandler>();
    [SerializeField] private MenuGroup currentMenuGroup;

    private void OnEnable()
    {
        allMenus.Add(this);
    }

    private void OnDisable()
    {
        allMenus.Remove(this);
    }


    public void PlayButtonClicked()
    {
        Debug.Log("Clicked play!");
    }

    public void OptionsButtonClicked()
    {
        DisableOthers(MenuGroup.Options);
    }

    private void DisableOthers(MenuGroup except)
    {
        foreach(ButtonEventHandler menu in allMenus)
        {
            if (menu.currentMenuGroup != except)
            {
                menu.gameObject.SetActive(false);
            }
        }
    }
}
