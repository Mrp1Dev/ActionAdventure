using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArrowBuyTextSetter : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private ArrowType type;
    private ArrowBuyButton buyButton;

    // Start is called before the first frame update
    void Awake()
    {
        buyButton = GetComponentInChildren<ArrowBuyButton>();
    }

    // Update is called once per frame
    void Update()
    {
        int currentAmount = SaveManager.SaveData.ArrowAmount[(int)type];
        int increment = buyButton.Increment;
        text.text = $"{currentAmount} + {increment}";
    }
}
