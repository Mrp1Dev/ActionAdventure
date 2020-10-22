using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventHandler : MonoBehaviour
{
    private EnemyCombat enemyCombat;

    private void Awake()
    {
        enemyCombat = GetComponentInParent<EnemyCombat>();
    }

    public void callDealDamage()
    {
        enemyCombat.dealDamage();
    }

    public void callStoppedAttacking()
    {
        enemyCombat.StoppedAttacking();
    }

    public void callNotTakingHit()
    {
        enemyCombat.notTakingHit();
    }

    public void callDie()
    {
        enemyCombat.CallDie();
    }

    
}
