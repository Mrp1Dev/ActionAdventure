using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class FinalStopper : MonoBehaviour
{
    private GameObject Player;
    bool hasCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Player.transform.position.x - transform.position.x;
        
        if (Mathf.Abs(dist) < 6f && !hasCompleted)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if(enemies.Length <= 0)
            {
                hasCompleted = true;
                StartCoroutine(AllowPass());
            }
        }
    }

    IEnumerator AllowPass()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSecondsRealtime(1f);

        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 15);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSecondsRealtime(2f);
        Destroy(this.gameObject);
    } 
}
