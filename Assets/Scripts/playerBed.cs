using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBed : MonoBehaviour, IInteractable
{
    public void interact()
    {
        DayManager.instance.advanceDay();
    }
}
