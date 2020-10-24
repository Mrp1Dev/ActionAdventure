using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        PauseManager.Instance.UnPause(true);
    }
}