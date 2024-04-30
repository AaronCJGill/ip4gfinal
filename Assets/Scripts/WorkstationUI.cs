using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class WorkstationUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    workstation parentworkstation;
    public bool leftSide;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (leftSide)
        {
            parentworkstation.goLeftInList();
            Debug.Log("This is found");
        }
        else
        {
            parentworkstation.goRightInList();
        }
    }
}
