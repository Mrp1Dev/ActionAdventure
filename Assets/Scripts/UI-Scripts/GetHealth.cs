using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetHealth : MonoBehaviour
{
    private PlayerCombat playerCombat;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (playerCombat.Health / playerCombat.DefaultHealth);
    }
}
