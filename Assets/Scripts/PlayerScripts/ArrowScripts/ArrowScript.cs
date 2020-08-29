using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private float dieTime=0.7f;
    [SerializeField] ArrowType arrowType;
    [SerializeField] int poisonCycles;

    [SerializeField] float explosionRange;
    [SerializeField] Vector3 tipOffset;
    [SerializeField] float bombDamage;
    [SerializeField] private GameObject particlePrefab;

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
            enemyCombat.takeDamage(damage);

            switch (arrowType)
            {
                case ArrowType.Normal:
                    break;

                case ArrowType.Poison:
                    Debug.Log("Poisoned");
                    float poisonDamage = damage * 0.8f;
                    enemyCombat.StartCoroutine(enemyCombat.poisonEnemy(poisonCycles, poisonDamage, 1.2f));
                    break;

                case ArrowType.Bomb:
                    Debug.Log("Bombed");
                    Vector2 tip = transform.position + tipOffset;
                    CameraShaker.Instance.ShakeOnce(10f, 7f, 0.1f, 0.2f);
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(tip, explosionRange, enemyLayer);

                    Instantiate(particlePrefab, tip, Quaternion.identity);

                    for (int i = 0; i < hitEnemies.Length; i++)
                    {

                        hitEnemies[i].GetComponent<EnemyCombat>().takeDamage(bombDamage);
                        hitEnemies[i].GetComponent<EnemyCombat>().StartCoroutine(hitEnemies[i].GetComponent<EnemyCombat>().knockback());
                    }
                    break;
            }
            GameObject.Destroy(this.gameObject);
        }
    }
}
