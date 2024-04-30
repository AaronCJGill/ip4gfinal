using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BuySpot : MonoBehaviour, IInteractable
{
    //is a spot to buy items

    //each thing has a price
    [SerializeField]
    int price;
    [SerializeField]
    GameObject furniture;
    bool bought = false;
    [SerializeField]
    TextMeshProUGUI buyitemtext;
    [SerializeField]
    GameObject uiprompt;
    private void Awake()
    {
        buyitemtext.text = "Buy this for: " + price.ToString();
    }
    public void interact()
    {
        if (!bought)
        {
            buyFurniture();
        }
    }
    private void Update()
    {
        uiprompt.transform.LookAt(2 * transform.position - Camera.main.transform.position);
    }
    public void buyFurniture()
    {
        if (price < PlayerInventory.instance.money)
        {
            PlayerInventory.instance.money -= price;
            bought = true;
            uiprompt.SetActive(false);
            furniture.SetActive(true);
        }
    }

}
