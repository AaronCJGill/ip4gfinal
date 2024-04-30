using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantBase : MonoBehaviour, IPlantBehavior, IInteractable
{
    public PlantType plant;
    [SerializeField]
    private MeshFilter plantMeshfilter;
    [SerializeField]
    private Transform plantTransform;
    [SerializeField]
    float sizeMultiplier = 1.2f;
    public float growFactor { get; private set; }//how much this grows by per day, or is grown on the first day

    private float startSize = 1f;
    private float currentGrowth =0;//hard value of amount this has grown by
    float growthDays = 1;//amount of days it has been growing for
    float growthDaysTotal = 100;
    float harvestablePercent = 0;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI growthRateText;
    [SerializeField]
    private TextMeshProUGUI daysTotal;
    bool wateredToday = false;

    private void Update()
    {
        nameText.text = plant.plantName;
        growthRateText.text = "Days So far: " + growthDays.ToString();
        daysTotal.text = "Days Needed: "+ growthDaysTotal.ToString();
    }

    public void Init(PlantType p)
    {
        //take in the settings from whatever we made P
        plant = p;
        plantMeshfilter.sharedMesh = p.mesh.sharedMesh;
        
        growthDaysTotal = p.plantGrowthTime;

        harvestablePercent = p.harvestablePercent;
        //start small
        plantTransform.localScale = Vector3.one * 0.001f;

        //grows at a rate determined by the days its been growing
        growFactor = growthDays / growthDaysTotal;// will always scale to 1

        //start coroutine to do growth animation
        StartCoroutine(firstDayGrowthAnim());
    }
    public void dayPassed()
    {
        //a day passes so it should grow, if it has been watered
        grow();
    }
    public void grow()
    {
        if (wateredToday && growthDays < growthDaysTotal)
        {
            Debug.Log(currentGrowth);
            growthDays++;
            currentGrowth = growFactor * growthDays;
            plantTransform.localScale = currentGrowth * Vector3.one * sizeMultiplier;
            //plantTransform.localScale = growFactor * Vector3.one;
        }
        else
        {
            
        }
        wateredToday = false;
    }
    public void harvest()
    {

        if (growthDays >= growthDaysTotal)
        {
            //allow harvest
            //if at 100% allow full harvest
            //if plant.harvestamount is > 1, 
            //subtract percentage of harvestable percent from current growth and see if it is greater than the amount harvestable subtracted
            //if it is 2, we check to see if we are over 20% of the way to the end, with the start being the harvest percentage
            //SCRAPPED

            //PlayerInventory.instance.addPlant(plant.plantclass);
            if (plant.plantclass == plantclasses.flower)
            {
                PlayerInventory.instance.addPlantFlower();
                Debug.Log("23123destroy this object");
            }
            else
            {
                PlayerInventory.instance.addPlantShrub();
                Debug.Log("destroy this object");
            }
            Destroy(gameObject);
        }
    }
    

    public void water()
    {
        wateredToday = true;
    }

    //on its first day it starts at 0 and animates itself growing to 10
    IEnumerator firstDayGrowthAnim()
    {
        Vector3 startSize = Vector3.one * 0.001f;
        Vector3 endSize = growFactor * Vector3.one * sizeMultiplier;
        float growtimetotal = 0.8f;
        float growtimecurrent = 0;

        yield return new WaitForSeconds(0);
        while (growtimecurrent < growtimetotal)
        {
            plantTransform.localScale = Vector3.Lerp(startSize, endSize, growtimecurrent / growtimetotal);
            growtimecurrent += Time.deltaTime;
            yield return null;
        }

        plantTransform.localScale = growFactor * Vector3.one;
    }


    public void interact()
    {
        //chooses what behavior should happen based on what the player has equipped
        if (PlayerInventory.Equipped == playerEquipped.wateringcan)
        {
            //water the item
            water();
        }
        else if(PlayerInventory.Equipped == playerEquipped.seed)
        {
            harvest();
        }
    }
}

