using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArrowBuyButton : MonoBehaviour
{
    [SerializeField] private ArrowType arrowType;
    [SerializeField] private int cost;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private int increment;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        SaveManager.SaveData.SetArrowAmount((int)arrowType, increment);
        SaveManager.SaveData.Gold -= cost;
        MenuManager.instance.BuyClickSound();
        Debug.Log($"{SaveManager.SaveData.ArrowAmount[(int)arrowType]} {arrowType}");
    }

    private void Update()
    {
        button.interactable = SaveManager.SaveData.Gold >= cost;
        costText.text = $"{cost}G";
    }

    public int Increment => increment;
}
