using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowBuyButton : MonoBehaviour
{
    [SerializeField] private ArrowType arrowType;
    [SerializeField] private int cost;
    [SerializeField] private int increment;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        SaveManager saveManager = SaveManager.instance;
        saveManager.SaveData.SetArrowAmount((int)arrowType, increment);
        Debug.Log($"{saveManager.SaveData.ArrowAmount[(int)arrowType]} {arrowType}");
    }
}
