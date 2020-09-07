using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBar : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject player;
    [SerializeField] private float activationDistance;

    [SerializeField]private GameObject bossBar;

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(boss.transform.position.x - player.transform.position.x) < activationDistance)
        {
            if (!bossBar.activeSelf)
            {
                bossBar.SetActive(true);
            }
        }
    }
}
