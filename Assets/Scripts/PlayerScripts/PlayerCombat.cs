using EZCameraShake;
using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private GameObject playerSprite;
    private Animator animator;
    private int attackNumber = 0;
    private AnimatorStateInfo animStateInfo;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    public float damage;
    public float attackRange;
    private Transform attackPos;
    public LayerMask enemyLayer;

    [SerializeField] private AudioSource slashAudio;

    [SerializeField] private float defaultHealth;
    private float health;
    private bool takingHit = false;

    [SerializeField] private GameObject deathUIScreen;

    // Start is called before the first frame update
    private void Start()
    {
        playerSprite = transform.GetChild(0).gameObject;
        animator = playerSprite.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();

        health = defaultHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetMouseButtonDown(0))
        {
            if (playerMovement.isGrounded)
            {
                tryAttackSequence();
            }
        }

        attackPos = transform.GetChild(1);
        checkMovement();
    }

    private void tryAttackSequence()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        if (attackNumber == 0)
        {
            attackNumber = 1;
            slashAudio.Play();
            updateAnimtor();
        }
        else if (attackNumber == 1)
        {
            updateAnimtor();

            if (animStateInfo.IsName("Attack-1") && animStateInfo.normalizedTime > 0.6f)
            {
                attackNumber = 2;
                slashAudio.PlayDelayed(0.05f);
                updateAnimtor();
            }
        }
        else if (attackNumber == 2)
        {
            updateAnimtor();
            if (animStateInfo.IsName("Attack-2") && animStateInfo.normalizedTime > 0.6f)
            {
                attackNumber = 3;
                slashAudio.PlayDelayed(0.05f);
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
            enemies[i].GetComponent<EnemyCombat>().TakeDamage(damage);
            if (doKnockback)
            {
                enemies[i].GetComponent<EnemyCombat>().StartCoroutine("Knockback");
            }
            Debug.Log("Overlapped enemy" + i.ToString());
        }
    }

    private void updateAnimtor()
    {
        animator.SetInteger("AttackNumber", attackNumber);
    }

    private void checkMovement()
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
        CameraShaker.Instance.ShakeOnce(10f, 4f, 0.1f, 0.1f);

        StartCoroutine("checkHealthStatus");
        StartCoroutine("spawnBlood");
    }

    private IEnumerator checkHealthStatus()
    {
        if (health <= 0)
        {
            GetComponent<Collider2D>().enabled = false;

            GetComponent<PlayerMovement>().enabled = false;
            FindObjectOfType<CameraFollow>().enabled = false;
            Vector2 knockbackVector = new Vector2(2500 * (transform.localScale.x * -1f), 0f);
            rb.AddForce(knockbackVector);
            yield return new WaitForSeconds(1f);
            deathUIScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator spawnBlood()
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

    public float DefaultHealth
    {
        get { return defaultHealth; }
        set { defaultHealth = value; }
    }

    public float Health => health;

    public Transform AttackPos => attackPos;
    public float AttackRanage => attackRange;
}