using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using UnityEngine;

public enum ArrowType
{
    Normal = 0,
    Poison = 1,
    Bomb = 2
}

public class PlayerRanged : MonoBehaviour
{


    private ArrowType selectedArrow;
    [SerializeField] private int[] arrowAmount;

    private PlayerCombat playerCombat;
    private PlayerMovement playerMovement;
    private Animator animator;

    private bool isShooting=false;
    private bool canShoot = true;
    [SerializeField] float defaultShootTimer;
    private float shootTimer;
    [SerializeField] private float arrowDamage;
    [SerializeField] private GameObject[] arrowPrefabs;


    private void Awake()
    {
        playerCombat = GetComponent<PlayerCombat>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        selectedArrow = ArrowType.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        arrowSelectCheck();
        shootCheck();

        animator.SetBool("Shooting", isShooting);
    }

    private void arrowSelectCheck()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedArrow = ArrowType.Normal;
            Debug.Log(selectedArrow);
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedArrow = ArrowType.Poison;
            Debug.Log(selectedArrow);
        }else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedArrow = ArrowType.Bomb;
            Debug.Log(selectedArrow);
        }
    }

    private void shootCheck()
    {
        if (Input.GetMouseButtonDown(1) && shootTimer <= 0f)
        {
            if (arrowAmount[(int)selectedArrow] > 0)
            {
                startShooting();
                arrowAmount[(int)selectedArrow]--;
                shootTimer = defaultShootTimer;
            }
        }
        else if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
    }

    public void increaseArrow(ArrowType arrowType, int amount)
    {
        arrowAmount[(int)arrowType] += amount;
    }

    private void startShooting()
    {
        isShooting = true;
        playerMovement.canMove = false;
    }

    public void spawnBullet(ArrowType arrowType)
    {
        Transform attackPos = playerCombat.AttackPos;
        Instantiate(arrowPrefabs[(int)arrowType], attackPos.position, attackPos.rotation);
    }

    public bool IsShooting
    {
        get { return isShooting; }
        set { isShooting = value; }
    }

    public float ArrowDamage => arrowDamage;

    public ArrowType SelectedArrow => selectedArrow;

    public int[] ArrowAmount => arrowAmount;
}
