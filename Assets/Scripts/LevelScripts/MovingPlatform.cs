using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pos1Ref;
    [SerializeField] private Transform pos2Ref;
    [SerializeField] private float timeToWaitDefault;
    private float timeToWait;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private GameObject player;
    private bool playerOnPlatform;

    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        pos1 = pos1Ref.position;
        pos2 = pos2Ref.position;
        targetPos = pos1;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToWait <= 0)
        {

            transform.position= Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (playerOnPlatform)
            {
                Vector3 moveDelta = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime) - transform.position;
                player.transform.position += moveDelta;
            }

            if (transform.position == targetPos)
            {
                if (targetPos == pos1) { targetPos = pos2; }
                else if (targetPos == pos2) { targetPos = pos1; }
                timeToWait = timeToWaitDefault;
            }
        }
        else 
        {
            timeToWait -= Time.deltaTime; 
        }

    }
    // thanks to xahellz

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer== player.layer)
        {
            playerOnPlatform = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == player.layer)
        {
            playerOnPlatform = false;
        }
    }
}
