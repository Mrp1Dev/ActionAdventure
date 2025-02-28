﻿using EZCameraShake;
using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float damage;
    public float health;
    [SerializeField] private bool isSuper;
    public bool IsSuper => isSuper;
    [SerializeField] private int superMultiplier = 3;
    private SpriteRenderer sprite;
    public float StartHealth { get; set; }
    private Animator animator;
    private Rigidbody2D rb;
    public float deathKnockbackPower;
    public float knockbackPower;
    private GameObject player;
    private ParticleSystem bloodParticle;
    [SerializeField] private AudioSource bloodEffect;

    private float dist;
    private bool isAttacking = false;
    private bool onSamePlatform;
    public LayerMask playerLayer;
    private bool canAttack = true;
    public float timeBetweenAttacks;

    private bool takingHit = false;
    public bool dead = false;
    [SerializeField] private int goldToIncrement = 0;
    public Transform attackPos;
    public float attackRange;
    [SerializeField] private float attackDistance = 1.5f;

    [SerializeField] private bool bossMode = false;

    private void Awake()
    {
        bloodParticle = GetComponentInChildren<ParticleSystem>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartHealth = health;
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (isSuper)
        {
            sprite.color = Color.magenta;
            goldToIncrement *= superMultiplier;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        CheckHealthStatus();
        if ((player.transform.position.y - transform.position.y) < 1.5f)
        {
            onSamePlatform = true;
        }
        else
        {
            onSamePlatform = false;
        }

        dist = player.transform.position.x - transform.position.x;

        if (Mathf.Abs(dist) < attackDistance && !dead && !takingHit && canAttack && onSamePlatform)
        {
            attack();
        }
    }

    public void TakeDamage(float damageGiven, bool isPoison = false)
    {
        isAttacking = false;
        GetComponent<EnemyAI>().HasSeenPlayer = true;
        health -= damageGiven;
        if (!isPoison)
        {
            animator.SetTrigger("TakeHit");
            takingHit = true;

            bloodEffect.PlayDelayed(0.05f);
            canAttack = true;
            CameraShaker.Instance.ShakeOnce(3f, 4f, 0.1f, 0.1f);
        }


        bloodParticle.Play();
        if (CheckHealthStatus())
        {
            dead = true;

            GetComponent<EnemyAI>().enabled = false;

            animator.SetBool("Dead", dead);
            if (bossMode)
            {
                rb.isKinematic = true;
            }
            else
            {
                DeathKnockback();
            }
            GetComponent<Collider2D>().enabled = false;
            Debug.Log("Death has seen this enemy :D");
        }
    }

    private bool CheckHealthStatus()
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

    public void CallDie()
    {
        if (!bossMode)
        {
            Invoke(nameof(die), 1f);
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }

    private void DeathKnockback()
    {
        Vector2 knockbackVector =
            new Vector2(deathKnockbackPower * -Mathf.Sign(player.transform.position.sqrMagnitude
            - transform.position.sqrMagnitude)
            , 0f);
        rb.AddForce(knockbackVector);
    }

    public IEnumerator Knockback()
    {
        Debug.Log("Knockbacked");
        Vector2 knockbackVector =
            new Vector2(knockbackPower * -Mathf.Sign(player.transform.position.sqrMagnitude
            - transform.position.sqrMagnitude)
            , 0f);
        rb.AddForce(knockbackVector);
        GetComponent<EnemyAI>().enabled = false;
        CameraShaker.Instance.ShakeOnce(4f, 2f, 0.1f, 0.2f);
        yield return new WaitForSeconds(1f);
        if (!dead)
            GetComponent<EnemyAI>().enabled = true;
    }

    private void die()
    {
        SaveManager.SaveData.Gold += goldToIncrement;
        GameObject.Destroy(this.gameObject);
    }

    public void notTakingHit()
    {
        takingHit = false;
    }

    private void attack()
    {
        if (bossMode && health < (0.5 * StartHealth))
        {
            animator.SetTrigger("Attack2");
        }
        else
        {
            animator.SetTrigger("Attack");
        }

        isAttacking = true;
        canAttack = false;
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    public void StoppedAttacking()
    {
        isAttacking = false;
        StartCoroutine(CanAttackCheck());
    }

    private IEnumerator CanAttackCheck()
    {
        yield return new WaitForSecondsRealtime(timeBetweenAttacks);
        canAttack = true;
    }

    public IEnumerator PoisonEnemy(int cycles, float damage, float timePerCycle)
    {
        for (int i = 0; i < cycles; i++)
        {
            TakeDamage(damage, true);
            yield return new WaitForSecondsRealtime(timePerCycle);
        }
        yield return null;
    }

    public void dealDamage()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(attackPos.position, attackRange, playerLayer);
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