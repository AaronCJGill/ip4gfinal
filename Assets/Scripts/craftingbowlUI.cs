using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class craftingbowlUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    craftingBowl parentCraftingBowl;
    public bool leftSide;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (leftSide)
        {
            parentCraftingBowl.goLeftInList();
        }
        else
        {
            parentCraftingBowl.goRightInList();
        }
    }
}

