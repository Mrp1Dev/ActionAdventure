using UnityEngine;

public class DisableComponentOnDeath : MonoBehaviour
{
    [SerializeField] private MonoBehaviour componentToDisable;
    [SerializeField] private EnemyCombat enemyCombat;

    private void Update()
    {
        if(enemyCombat.dead)
        {
            componentToDisable.enabled = false;
        }
    }
}
