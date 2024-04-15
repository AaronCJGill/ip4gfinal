using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantBase : MonoBehaviour, IPlantBehavior
{
    public PlantType plant;
    private MeshFilter plantModel;
    private Transform plantTransform;

    public float growFactor { get; private set; }//how much this grows by per day, or is grown on the first day

    private float startSize = 0.1f;
    private float currentGrowth;//hard value of amount this has grown by
    float growthDays = 1;//amount of days it has been growing for
    float growthDaysTotal = 100;
    float harvestablePercent = 0;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI growthRateText;
    [SerializeField]
    private TextMeshProUGUI daysTotal;

    public void Init(PlantType p)
    {
        //take in the settings from whatever we made P
        plant = p;
        plantModel = p.mesh;
        growthDaysTotal = p.plantGrowthTime;

        harvestablePercent = p.harvestablePercent;

        //start small
        plantTransform.localScale = Vector3.one * startSize;

        //grows at a rate determined by the days its been growing
        growFactor = growthDays / growthDaysTotal;// will always scale to 1

        //start coroutine to do growth animation
        StartCoroutine(firstDayGrowthAnim());
    }

    public void grow()
    {
        growthDays++;
        currentGrowth = growFactor * growthDays;
        plantTransform.localScale = currentGrowth * Vector3.one;
        //plantTransform.localScale = growFactor * Vector3.one;
    }
    public void harvest()
    {
        if (harvestablePercent <= currentGrowth)
        {
            //allow harvest
            //if at 100% allow full harvest
            //if plant.harvestamount is > 1, 
            //subtract percentage of harvestable percent from current growth and see if it is greater than the amount harvestable subtracted
            //if it is 2, we check to see if we are over 20% of the way to the end, with the start being the harvest percentage
            if (plant.harvestAmount > 1)
            {
                float x = currentGrowth - harvestablePercent;
                
                int hamount = 0;
            }
        }
    }
    public void water()
    {

    }

    //on its first day it starts at 0 and animates itself growing to 10
    IEnumerator firstDayGrowthAnim()
    {
        Vector3 startSize = Vector3.one * 0.001f;
        Vector3 endSize = growFactor * Vector3.one;
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

}

