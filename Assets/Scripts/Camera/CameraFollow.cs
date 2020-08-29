using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    public Vector3 offset;
    private Vector3 newPositon;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float x = playerTransform.position.x;
        float y = playerTransform.position.y;
        newPositon= new Vector3(x, y, transform.position.z)+ offset;

        transform.position = newPositon;
    }
}
