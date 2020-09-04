using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int levelToOpen;

    public void OnClick()
    {
        SceneManager.LoadSceneAsync(levelToOpen);
        
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);

        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        if (text)
        {
            text.text = levelToOpen.ToString();
        }
    }
}
