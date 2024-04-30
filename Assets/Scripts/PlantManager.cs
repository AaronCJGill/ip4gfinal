using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantManager : MonoBehaviour
{
    //keeps track of all of the plants the player has currently growing
    [SerializeField]
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
        //get list of every plant in scene
        PlantBase[] pba = FindObjectsOfType<PlantBase>();
        for (int i = 0; i < pba.Length; i++)
        {
            playerownedplants.Add(pba[i]);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            for (int i = 0; i < instance.playerownedplants.Count; i++)
            {
                //for every plant allow them to grow
                instance.playerownedplants[i].water();
            }
        }
    }
    public static void newPlantMade(PlantBase pb)
    {
        instance.playerownedplants.Add(pb);
    }

    public static void updateplants()
    {
        for (int i = 0; i < instance.playerownedplants.Count; i++)
        {
            //for every plant allow them to grow
            instance.playerownedplants[i].grow();
        }
    }

}
