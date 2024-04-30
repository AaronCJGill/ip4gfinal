using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class IngredientType : ScriptableObject
{
    public ingredientstage typeofingredient;
    public PlantType plantIngredient;
}

public enum ingredientstage
{
    crushed,
    crystallized,
    water
}