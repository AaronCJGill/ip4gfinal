using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : MonoBehaviour, IInteractable
{
    [SerializeField]
    bool seller = true;
    [SerializeField]
    public PotionType potiontobuy;
    [SerializeField]
    public plantclasses planttosell;
    public int cost;
    public void interact()
    {
        if (seller && PlayerInventory.instance.money -cost >= 0)
        {
            PlayerInventory.instance.money -= cost;
            if (planttosell == plantclasses.flower)
            {
                PlayerInventory.instance.playerFlowerSeeds++;
            }
            else
            {
                Debug.Log("Adding plant seed");

                PlayerInventory.instance.playerShrubSeeds++;
            }
        }
        else if(PlayerInventory.instance.playerOwnedPotions.Count > 0)
        {
            PlayerInventory.instance.removeAnyPotion();
            cost = Random.Range(13,45);
            PlayerInventory.instance.money += cost;
        }
    }
}
