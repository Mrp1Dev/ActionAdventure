using Cinemachine;
using UnityEngine;

public class StarsParallax : MonoBehaviour
{
    [SerializeField] private float parallax = 0.2f;
    private Transform camera;
    private Vector3 lastPlayerPos;

    private void Awake()
    {
        camera = FindObjectOfType<CinemachineVirtualCamera>().transform;
        lastPlayerPos = camera.position;
    }

    private void Update()
    {
        var playerPosDelta = (camera.position - lastPlayerPos) * parallax;
        transform.position += playerPosDelta;
        lastPlayerPos = camera.position;
    }
}