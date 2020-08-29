using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventHandler : MonoBehaviour
{
    private GameObject enemyParent;
    private EnemyCombat enemyCombat;

    private void Awake()
    {
        enemyParent = GetComponentInParent<Transform>().gameObject;
        enemyCombat = GetComponentInParent<EnemyCombat>();
    }

    public void callDealDamage()
    {
        enemyCombat.dealDamage();
    }

    public void callStoppedAttacking()
    {
        enemyCombat.stoppedAttacking();

    }

    public void callNotTakingHit()
    {
        enemyCombat.notTakingHit();
    }

    public void callDie()
    {
        enemyCombat.callDie();
    }

    
}
