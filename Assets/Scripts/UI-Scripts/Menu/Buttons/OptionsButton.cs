using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] GameObject toShow;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        MenuManager.instance.ShowGroup(toShow);
    }
}
