using UnityEngine;

public class LevelEndScreen : MonoBehaviour
{
    private void OnEnable()
    {
        PauseManager.Instance.DoPauseInput = false;
        PauseManager.Instance.Pause();
    }
}