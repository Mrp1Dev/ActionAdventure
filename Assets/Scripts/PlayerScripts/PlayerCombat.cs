﻿using EZCameraShake;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
   
    private GameObject playerSprite;
    private Animator animator;
    private int attackNumber=0;
    AnimatorStateInfo animStateInfo;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    public float damage;
    public float attackRange;
    private Transform attackPos;
    public LayerMask enemyLayer;


    [SerializeField]private float defaultHealth;
    private float health;
    private bool takingHit=false;

    // Start is called before the first frame update
    void Start()
    {
        playerSprite = transform.GetChild(0).gameObject;
        animator = playerSprite.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();

        health = defaultHealth;
    }

    // Update is called once per frame
    void Update()
    {
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
        if (Input.GetMouseButtonDown(0)) 
        {
            if (playerMovement.isGrounded) {
                tryAttackSequence();
            }
            
        }

        attackPos = transform.GetChild(1);
        checkMovement();

    }

    void tryAttackSequence()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        if (attackNumber == 0)
        {
            attackNumber = 1;
            updateAnimtor();
        }
        else if (attackNumber == 1)
        {
            updateAnimtor();

            if (animStateInfo.IsName("Attack-1") && animStateInfo.normalizedTime > 0.6f)
            {
                attackNumber = 2;
                updateAnimtor();
            }
        }
        else if (attackNumber == 2)
        {
            updateAnimtor();
            if (animStateInfo.IsName("Attack-2") && animStateInfo.normalizedTime > 0.6f)
            {
                attackNumber = 3;
                updateAnimtor();
            }
        }

        
    }

    public void AttackEnded()
    {
        attackNumber = 0;
        updateAnimtor();
    }

    public void doDamage(bool doKnockback)
    {
       Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyCombat>().takeDamage(damage);
            if (doKnockback)
            {
                enemies[i].GetComponent<EnemyCombat>().StartCoroutine("knockback");
            }
            Debug.Log("Overlapped enemy" + i.ToString());
        }
    }

    void updateAnimtor()
    {
        animator.SetInteger("AttackNumber", attackNumber);

    }

    void checkMovement()
    {
        if (attackNumber > 0)
        {
            playerMovement.canMove = false;

        }
        else
        {
            playerMovement.canMove = true;
        }

    }

    public void takeDamage(float damageRecieved)
    {
        health -= damageRecieved;
        animator.SetTrigger("TakeHit");
        takingHit = true;
        rb.velocity = new Vector2(0f, rb.velocity.y);

        CameraShaker.Instance.ShakeOnce(10f, 2f, 0.1f, 0.1f);

        StartCoroutine("checkHealthStatus");
        StartCoroutine("spawnBlood");
    }

    IEnumerator checkHealthStatus()
    {
        if (health <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            
            GetComponent<PlayerMovement>().enabled = false;
            Camera.current.transform.GetComponentInParent<CameraFollow>().enabled = false;
            Vector2 knockbackVector = new Vector2(2500 * (transform.localScale.x * -1f), 0f);
            rb.AddForce(knockbackVector);
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
    }

    IEnumerator spawnBlood()
    {
        ParticleSystem bloodParticle = GetComponentInChildren<ParticleSystem>();
        bloodParticle.Play();
        yield return null;
    }

    public bool TakingHit
    {
        get { return takingHit; }
        set { takingHit = value; }
    }



    public float DefaultHealth => defaultHealth;
    public float Health => health;

    public Transform AttackPos => attackPos;
    public float AttackRanage => attackRange;
}
