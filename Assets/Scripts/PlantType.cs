using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlantType : ScriptableObject
{
    public MeshFilter mesh;
    public string plantName;
    public int plantGrowthTime;
    [Range(1,100)]
    public float harvestablePercent;//at what percentage is this plant ready to be harvested 
    public int harvestAmount;
}
