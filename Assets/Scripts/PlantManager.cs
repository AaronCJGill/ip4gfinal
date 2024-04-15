using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantManager : MonoBehaviour
{
    //keeps track of all of the plants the player has currently growing
    List<PlantBase> playerownedplants = new List<PlantBase>();

    public static PlantManager instance;
    private void Awake()
    {
        //singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if(instance == null)
        {
            instance= this;
        }
    }

    public static void newPlantMade(PlantBase pb)
    {
        instance.playerownedplants.Add(pb);
    }


}
