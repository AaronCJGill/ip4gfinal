using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class craftingBowl : MonoBehaviour, IInteractable
{
    //takes in a type of ingredient
    //gives a type of potion
    private int pointer = 0;

    public TextMeshProUGUI descriptionText;

    public potionRecipes[] recipes = new potionRecipes[9];
    public void goLeftInList()
    {
        pointer--;
        if (pointer < 0)
        {
            pointer = recipes.Length - 1;
        }
    }
    public void goRightInList()
    {
        pointer++;
        if (pointer > recipes.Length - 1)
        {
            pointer = 0;
        }
    }
    public void interact()
    {
        if ((PlayerInventory.instance.checkIfPlayerHasIngredient(recipes[pointer].ingredient1) && 
            PlayerInventory.instance.checkIfPlayerHasIngredient(recipes[pointer].ingredient2)) && 
            recipes[pointer].ingredient1 != recipes[pointer].ingredient2)
        {
            PlayerInventory.instance.removeIngredient(recipes[pointer].ingredient1);
            PlayerInventory.instance.removeIngredient(recipes[pointer].ingredient2);

            PotionType x = ScriptableObject.CreateInstance<PotionType>();
            PlayerInventory.instance.addPotion(recipes[pointer].result);
        }else if (PlayerInventory.instance.checkIfPlayerHasIngredient(recipes[pointer].ingredient1) && 
            PlayerInventory.instance.checkIfPlayerHasIngredient(recipes[pointer].ingredient2) && 
                recipes[pointer].ingredient1 == recipes[pointer].ingredient2)
        {
            Debug.Log("these equal ");
            //remove both
            PlayerInventory.instance.removeIngredients(recipes[pointer].ingredient1);

            PotionType x = ScriptableObject.CreateInstance<PotionType>();
            PlayerInventory.instance.addPotion(recipes[pointer].result);
        }
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            goRightInList();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            goLeftInList();
        }
        descriptionText.text = "Input 1: " + recipes[pointer].ingredient1.plantIngredient.plantclass + " "+ recipes[pointer].ingredient1.typeofingredient + "\nInput 2: " + recipes[pointer].ingredient2.plantIngredient.plantclass + " " + recipes[pointer].ingredient2.typeofingredient + "\nResult: " + recipes[pointer].result.result;

    }
}

//bowl has recipe that is linked to potion
[System.Serializable]
public struct potionRecipes
{
    public PotionType result;
    public IngredientType ingredient1;
    public IngredientType ingredient2;
}
[System.Serializable]
public enum potionResults
{
    depression,//crushed shrub, crushed flower
    sadness,//crushed shrub, powdered flower
    grief,//crushed shrub, essence of flower
    creation,//essence of shrub, crushed flower
    life,//essence of shrub, powdered flower
    healing, //essence of shrub, essence of flower
    time,//powdered shrub, crushed flower
    energy,//powdered shrub, essence of flower
    equivalence, //powdered shrub, essence of flower
}