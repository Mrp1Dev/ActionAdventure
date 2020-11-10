using UnityEngine;

public class StarsParallax : MonoBehaviour
{
    [SerializeField] private float parallax = 0.2f;
    private Transform player;
    private PlayerCombat playerCombat;
    private Vector3 lastPlayerPos;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        playerCombat = player.GetComponent<PlayerCombat>();
        lastPlayerPos = player.position;
    }

    private void Update()
    {
        bool dead = playerCombat.IsDead;
        if (!dead)
        {
            var playerPosDelta = (player.position - lastPlayerPos) * parallax;
            transform.position += playerPosDelta;
            lastPlayerPos = player.position;
        }
    }
}