using TMPro;
using UnityEngine;

public class GoldAmountTracker : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float goldAnimationSpeed;
    private int currentGold;
    private int shownGold;
    void Update()
    {
        currentGold = SaveManager.SaveData.Gold;
        shownGold = Mathf.CeilToInt(Mathf.MoveTowards(shownGold,
            currentGold,
            ((currentGold - shownGold)/0.4f) * goldAnimationSpeed * Time.deltaTime));

        text.text = $"{shownGold}G";
    }
}
