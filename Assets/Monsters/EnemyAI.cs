using System.Collections;
using System.Collections.Generic;

using Unity.Mathematics;
using UnityEngine;

using UnityEngine.Assertions.Must;
using UnityEngine.SocialPlatforms;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    private bool hasSeenPlayer;
    public bool defaultFacingRight;
    private bool facingRight;
    public float maxDistance;
    public float followDistance;
    private Vector2 playerPos;
    public float yLevelDif=0.5f;
    private float dist;
    public Rigidbody2D rb;
    public float moveSpeed;
    private Animator animator;
    private EnemyCombat enemyCombat;

    public float fallDamageHeight;
    public float fallDamagePerMetre;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponentInChildren<Animator>();
        enemyCombat = GetComponent<EnemyCombat>();

        if (!defaultFacingRight)
        {
            transform.localScale = new Vector3(transform.localScale.x*-1f, transform.localScale.y, transform.localScale.z);
        }
        facingRight = defaultFacingRight;
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        dist = playerPos.x - transform.position.x;
        checkForPlayer();
        if (hasSeenPlayer && Mathf.Abs(playerPos.y - transform.position.y) < yLevelDif && dist<followDistance)
        {
            followPlayer();
            
        }
        else
        {
            stopfollowingPlayer();
        }

        checkFlip();

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    void checkForPlayer()
    {
               
        if(Mathf.Abs(dist)<maxDistance && Mathf.Abs(playerPos.y - transform.position.y)<yLevelDif)
        {
            
            if(!facingRight && dist < 0)
            {
                hasSeenPlayer = true;

                
            }else if(facingRight && dist > 0)
            {
                hasSeenPlayer = true;

            }
        }
    }

    void followPlayer()
    {
        if (Mathf.Abs(dist) > 1.5f && !enemyCombat.IsAttacking && !enemyCombat.TakingHit )
        {
            if (dist < 0)
            {
                rb.velocity = new Vector2(moveSpeed * -1f, rb.velocity.y);
            }
            else if (dist > 0)
            {
                rb.velocity = new Vector2(Mathf.Abs(moveSpeed), rb.velocity.y);
            }

        }
        else
        {
            stopfollowingPlayer();
        }
    }
    void stopfollowingPlayer()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }
    
    void checkFlip()
    {
        if ((rb.velocity.x > 0 && !facingRight) || (rb.velocity.x < 0 && facingRight))
        {
            facingRight = !facingRight;
            Vector3 localscale = transform.localScale;
            transform.localScale = new Vector3(localscale.x * -1f, localscale.y, localscale.z);

        }
    }

    float startHeight;
    float endHeight;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Platform")
        {
           
            endHeight = transform.position.y;
        }

        if (startHeight - endHeight > fallDamageHeight && startHeight != 0)
        {
            GetComponent<EnemyCombat>().takeDamage(((startHeight - endHeight) - fallDamageHeight) * fallDamagePerMetre);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Platform")
        {
            
            startHeight = transform.position.y;
        }
    }

    public bool HasSeenplayer
    {
        get { return hasSeenPlayer; }
        set { hasSeenPlayer = value; }
    }
}
