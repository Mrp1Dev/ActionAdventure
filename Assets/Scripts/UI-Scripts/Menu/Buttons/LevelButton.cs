using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int levelToOpen;
    [SerializeField] private Animator transition;// the black image that slowly goes up in darkness.
    private bool hasTransitionStarted = false;
    public void OnClick()
    {
        transition.SetTrigger("CallTransition");
        hasTransitionStarted = true;
       
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

    private void Update()
    {
        if (hasTransitionStarted)
        {
            if (transition.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                SceneManager.LoadScene(levelToOpen);
            }

        }
    }
}
