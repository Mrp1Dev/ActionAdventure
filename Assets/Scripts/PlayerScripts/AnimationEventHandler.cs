using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    private PlayerCombat playerCombat;
    private PlayerMovement playerMovement;
    private PlayerRanged playerRanged;
    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GetComponentInParent<PlayerCombat>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerRanged = GetComponentInParent<PlayerRanged>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void callAttackEnded()
    {
        playerCombat.AttackEnded();
    }

    public void callDoDamage(int getDoKnockback)
    {
        bool doKnockback = false;
        if (getDoKnockback > 0)
        {
            doKnockback = true;
        }
        playerCombat.doDamage(doKnockback);
    }

    public void notTakingHit()
    {
        playerCombat.TakingHit = false;
        playerMovement.canMove = true;
        Debug.Log("AnimationeventHandled");
    }

    public void callSpawnBullet()
    {
        playerRanged.SpawnBullet(playerRanged.SelectedArrow);
    }

    public void notShooting()
    {
        playerRanged.IsShooting = false;
        playerCombat.AttackEnded();
        Debug.Log("notShootingEvent");
    }
}
