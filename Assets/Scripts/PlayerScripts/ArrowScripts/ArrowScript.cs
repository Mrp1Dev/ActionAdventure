using EZCameraShake;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private float dieTime=0.7f;
    [SerializeField] ArrowType arrowType;
    [SerializeField] int poisonCycles;

    [SerializeField] private float explosionRange;
    [SerializeField] private Vector3 tipOffset;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private AudioSource arrowHitSound;

    [SerializeField] LayerMask enemyLayer;

    private void OnDrawGizmos()
    {
        Vector3 tip = transform.position + tipOffset;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(tip, explosionRange);
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * 20f;
        if (transform.right.x < 0)
        {
            tipOffset *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dieTime <= 0f)
        {
            GameObject.Destroy(this.gameObject);
        }
        else { dieTime -= Time.deltaTime; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            float damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRanged>().ArrowDamage;
            EnemyCombat enemyCombat = collision.gameObject.GetComponent<EnemyCombat>();
            arrowHitSound.PlayDelayed(0.05f);
                 enemyCombat.TakeDamage(damage);

            switch (arrowType)
            {
                case ArrowType.Normal:
                    break;

                case ArrowType.Poison:
                    Debug.Log("Poisoned");
                    float poisonDamage = damage * 0.8f;
                    enemyCombat.StartCoroutine(enemyCombat.PoisonEnemy(poisonCycles, poisonDamage, 1.2f));
                    break;

                case ArrowType.Bomb:
                    Debug.Log("Bombed");
                    Vector2 tip = transform.position + tipOffset;
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(tip, explosionRange, enemyLayer);

                    Instantiate(particlePrefab, tip, Quaternion.identity);

                    for (int i = 0; i < hitEnemies.Length; i++)
                    {

                        hitEnemies[i].GetComponent<EnemyCombat>().TakeDamage(damage*10);
                        hitEnemies[i].GetComponent<EnemyCombat>().StartCoroutine(hitEnemies[i].GetComponent<EnemyCombat>().Knockback());
                    }
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
