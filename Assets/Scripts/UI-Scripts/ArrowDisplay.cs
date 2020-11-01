using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArrowDisplay : MonoBehaviour
{
    [SerializeField] private Color[] imageColors;
    private PlayerRanged playerRanged;
    private TMP_Text amountText;
    [SerializeField] private Image arrowImage;

    private void Awake()
    {
        amountText = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        playerRanged = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRanged>();
    }

    private void Update()
    {
        ArrowType selectedArrow = playerRanged.SelectedArrow;
        arrowImage.color = imageColors[(int)selectedArrow];

        if (selectedArrow == ArrowType.Normal)
        {
            amountText.text = "999+";
        }
        else
        {
            amountText.text = playerRanged.ArrowAmount[(int)selectedArrow].ToString("D3");
        }
    }
}