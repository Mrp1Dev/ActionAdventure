using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ShineWhenSuper : MonoBehaviour
{
    [SerializeField] private EnemyCombat enemyCombat;

    private void Start()
    {
        GetComponent<Light2D>().enabled = enemyCombat.IsSuper;
    }
}