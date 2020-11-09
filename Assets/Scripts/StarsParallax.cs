using UnityEngine;

public class StarsParallax : MonoBehaviour
{
    [SerializeField] private float parallax = 0.2f;
    private Transform player;
    private Vector3 lastPlayerPos;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        lastPlayerPos = player.position;
    }

    private void Update()
    {
        var playerPosDelta = (player.position - lastPlayerPos) * parallax;
        transform.position += playerPosDelta;
        lastPlayerPos = player.position;
    }
}