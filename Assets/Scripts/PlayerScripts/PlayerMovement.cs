using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float movementHorizontal;
    public float moveSpeed = 20;

    public int jumpForce = 750;
    public float fallDamageHeight;
    public float fallDamagePerMetre;
    [SerializeField] private AudioSource jumpAudio;

    private Rigidbody2D rigidbody2D;
    private Animator sprite_Animator;
    private GameObject playerSprite;

    private bool facingRight = true;
    public bool isGrounded = false;
    public bool canMove = true;

    [SerializeField] private LayerMask platform;
    [SerializeField] private Transform groundHitPos;
    [SerializeField] private Vector2 groundHitSize;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 size = new Vector3(groundHitSize.x, groundHitSize.y, 0f);
        Gizmos.DrawCube(groundHitPos.position, size);
    }

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerSprite = transform.GetChild(0).gameObject;
        sprite_Animator = playerSprite.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (canMove && !GetComponent<PlayerCombat>().TakingHit && !GetComponent<PlayerRanged>().IsShooting) { movePlayer(); }
    }

    private void Update()
    {
        groundCheck();
        if (Input.GetButtonDown("Jump") && isGrounded && !PauseManager.Instance.Paused)
        {
            Jump();
        }

        if (movementHorizontal > 0 && facingRight == false)
        {
            flipPlayer();
        }
        else if (movementHorizontal < 0 && facingRight == true)
        {
            flipPlayer();
        }
    }

    private void movePlayer()
    {
        movementHorizontal = Input.GetAxis("Horizontal") * 0.4f + Input.GetAxisRaw("Horizontal") * 0.6f;
        rigidbody2D.velocity = new Vector2(movementHorizontal * moveSpeed, rigidbody2D.velocity.y);
        sprite_Animator.SetFloat("Speed", Mathf.Abs(movementHorizontal));
    }

    private void groundCheck()
    {
        RaycastHit2D groundHit = Physics2D.BoxCast(groundHitPos.position, groundHitSize, 0f, Vector2.down, groundHitSize.y, platform);

        if (groundHit.collider == null)
        {
            isGrounded = false;
        }
        else { isGrounded = true; }

        sprite_Animator.SetBool("IsGrounded", isGrounded);
    }

    private void Jump()
    {
        rigidbody2D.AddForce(Vector2.up * jumpForce);
        GetComponent<PlayerCombat>().AttackEnded();
        AudioSource[] otherAudio = GetComponents<AudioSource>();
        foreach (var other in otherAudio) { other.Stop(); }
        jumpAudio.Play();
        GetComponent<PlayerCombat>().TakingHit = false;
    }

    private float startHeight = 0f;
    private float endHeight = 0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collision.contacts[0].point.y >
            transform.position.y - collider.bounds.extents.y)
        {
            return;
        }
        if (collision.collider.gameObject.tag == "Platform")
        {
            endHeight = transform.position.y;
        }

        if (startHeight - endHeight > fallDamageHeight)
        {
            GetComponent<PlayerCombat>()
                .takeDamage(((startHeight - endHeight) - fallDamageHeight) * fallDamagePerMetre);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Platform")
        {
            startHeight = transform.position.y;
        }
    }

    private void flipPlayer()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}