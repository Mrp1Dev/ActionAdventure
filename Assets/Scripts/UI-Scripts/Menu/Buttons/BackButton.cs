using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
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
