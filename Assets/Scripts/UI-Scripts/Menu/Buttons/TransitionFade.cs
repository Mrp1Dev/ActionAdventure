using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionFade : MonoBehaviour
{
    [SerializeField] private bool reverse;
    [SerializeField] private float delay;
    private bool hasTransitionStarted = false;
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (delay <= 0)
        {
            if (!hasTransitionStarted)
            {
                DoTransitionFade(reverse);
                hasTransitionStarted = true;
            }
        }
    }

    void DoTransitionFade(bool doReverse)
    {
        if (animator)
        {
            if (doReverse)
            {
                animator.SetTrigger("CallTransitionReverse");
                Debug.Log("normal  was called");
            }
            else
            {
                animator.SetTrigger("CallTransition");
            }
        }

    }
}
