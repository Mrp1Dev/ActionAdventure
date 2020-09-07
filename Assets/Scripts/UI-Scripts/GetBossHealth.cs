using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetBossHealth : MonoBehaviour
{
    [SerializeField] EnemyCombat enemyCombat;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (enemyCombat.health / enemyCombat.StartHealth);
    }
}
