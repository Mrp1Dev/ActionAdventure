using UnityEngine;
using UnityEngine.UI;

public class ChangeMenuPanel : MonoBehaviour
{
    [SerializeField] private GameObject toShow;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        MenuManager.instance.ShowGroup(toShow);
        MenuManager.instance.ButtonClickSound();
    }
}