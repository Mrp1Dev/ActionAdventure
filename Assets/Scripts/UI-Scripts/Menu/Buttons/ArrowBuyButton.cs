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

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {     
        SaveManager.SaveData.SetArrowAmount((int)arrowType, increment);
        Debug.Log($"{SaveManager.SaveData.ArrowAmount[(int)arrowType]} {arrowType}");
    }

    private void Update()
    {
        costText.text = $"{cost}G";
    }

    public int Increment => increment;
}
