using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class EnemyCombat : MonoBehaviour
{
    public float damage;
    public float health;
    private Animator animator;
    private Rigidbody2D rb;
    public float deathKnockbackPower;
    public float knockbackPower;
    private GameObject player;
    ParticleSystem bloodParticle;

    private float dist;
    private bool isAttacking = false;
    private bool onSamePlatform;
    public LayerMask playerLayer;
    private bool canAttack=true;
    public float timeBetweenAttacks;


    private bool takingHit=false;
    public bool dead = false;

    public Transform attackPos;
    public float attackRange;


    private void Awake()
    {
        bloodParticle = GetComponentInChildren<ParticleSystem>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        checkHealthStatus();
        if((player.transform.position.y-transform.position.y)< 1.5f)
        {
            onSamePlatform = true;
        }
        else
        {
            onSamePlatform = false;

        }

        dist = player.transform.position.x - transform.position.x;

        if (Mathf.Abs(dist) < 1.5f && !dead && !takingHit && canAttack && onSamePlatform)
        {
            attack();
        }
    }

    public void takeDamage(float damageGiven)
    {
        health -= damageGiven;
        animator.SetTrigger("TakeHit");
        takingHit = true;
        canAttack = true;

        CameraShaker.Instance.ShakeOnce(3f, 2f, 0.1f, 0.1f);
        
        bloodParticle.Play();
        if (checkHealthStatus())
        {
            dead = true;

            GetComponent<EnemyAI>().enabled = false;

            animator.SetBool("Dead", dead);
            deathKnockback();
            
            GetComponent<Collider2D>().enabled = false;
            Debug.Log("Death has seen this enemy :D");
        }
    }

    bool checkHealthStatus()
    {
        if (health <= 0f)
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    public void callDie()
    {
        Invoke("die", 1f);
    }

    private void deathKnockback()
    {
        Vector2 knockbackVector = new Vector2(deathKnockbackPower * (transform.localScale.x * -1f), 0f);
        rb.AddForce(knockbackVector);
        CameraShaker.Instance.ShakeOnce(8f, 2f, 0.1f, 0.1f);
    }

    public IEnumerator knockback()
    {
        Debug.Log("Knockbacked");
        Vector2 knockbackVector = new Vector2(knockbackPower * (transform.localScale.x * -1f), 0f);
        rb.AddForce(knockbackVector);
        CameraShaker.Instance.ShakeOnce(4f, 2f, 0.1f, 0.2f);

        GetComponent<EnemyAI>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<EnemyAI>().enabled = true;

    }

    void die()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void notTakingHit()
    {
        takingHit = false;
    }

    void attack()
    {
        animator.SetTrigger("Attack");
        isAttacking = true;
        canAttack = false;
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    public void stoppedAttacking()
    {
        isAttacking = false;
        StartCoroutine("canAttackCheck");

    }

    private IEnumerator canAttackCheck()
    {
        yield return new WaitForSecondsRealtime(timeBetweenAttacks);
        canAttack = true;
    }

    public IEnumerator poisonEnemy(int cycles, float damage, float timePerCycle)
    {
        for (int i = 0; i < cycles; i++)
        {
            takeDamage(damage);
            yield return new WaitForSecondsRealtime(timePerCycle);
        }
        yield return null;
    }

    public void dealDamage()
    {
        
        Collider2D playerCollider= Physics2D.OverlapCircle(attackPos.position, attackRange, playerLayer);
        if (playerCollider != null)
        {
            playerCollider.gameObject.GetComponent<PlayerCombat>().takeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public bool IsAttacking => isAttacking;
    public bool TakingHit => takingHit;


}
