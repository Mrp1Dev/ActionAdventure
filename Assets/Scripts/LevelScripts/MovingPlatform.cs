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

    private List<GameObject> objectsOnTop = new List<GameObject>();
    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 targetPos;

    // Start is called before the first frame update
    private void Start()
    {
        pos1 = pos1Ref.position;
        pos2 = pos2Ref.position;
        targetPos = pos1;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (timeToWait <= 0)
        {
            Vector3 moveDelta = Vector3
                .MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime)
                - transform.position;

            transform.position += moveDelta;

            objectsOnTop.ForEach(go => go.transform.position += moveDelta);

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
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb && rb.bodyType != RigidbodyType2D.Static)
        {
            objectsOnTop.Add(collision.gameObject);
            Debug.Log($"Added {collision.gameObject}");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (objectsOnTop.Contains(collision.gameObject))
        {
            objectsOnTop.Remove(collision.gameObject);
        }
    }
}