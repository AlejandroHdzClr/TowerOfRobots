using Tower.Actions;
using UnityEngine;

public class TowerCardsUpgrade : MonoBehaviour
{

    [SerializeField] private TowerHealingSystem towerHealthSystem;
    private bool isPerkActivated = false;
    public void ActivatePerk()
    {
        if (!isPerkActivated)
        {
            towerHealthSystem.enabled = true;
            isPerkActivated = true;
            Debug.Log("Perk Activated");
        }
        else {
            towerHealthSystem.enabled = false;
            isPerkActivated = false;
            Debug.Log("Perk Deactivated");
        }
    }
}
