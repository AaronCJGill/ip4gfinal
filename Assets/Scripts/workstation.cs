using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class workstation : MonoBehaviour, IInteractable
{
    public TextMeshProUGUI descriptionText;
    //describes the purpose of what this does
    //takes in planttype
    //returns an ingredient
    private int pointer = 0;

    public workstationRecipe[] possibleRecipes = new workstationRecipe[2];//reduced down to 6
    public void goLeftInList()
    {
        pointer--;
        if (pointer < 0)
        {
            pointer = possibleRecipes.Length - 1;
        }
    }
    public void goRightInList()
    {
        pointer++;
        if (pointer > possibleRecipes.Length -1)
        {
            pointer = 0;
        }
    }

    //leftbutton, interact button, right button
    public void interact()
    {

        //check if player has planttype
        if (PlayerInventory.instance.checkIfPlayerHasPlantType(possibleRecipes[pointer].input))
        {
            PlayerInventory.instance.removePlant(possibleRecipes[pointer].input);
            PlayerInventory.instance.addIngredient(possibleRecipes[pointer].result);
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
        descriptionText.text = "Input: " + possibleRecipes[pointer].input.plantclass+ "\nResult: " + possibleRecipes[pointer].input.plantclass + " "+ possibleRecipes[pointer].result.typeofingredient;

    }

}
[System.Serializable]
public struct workstationRecipe
{
    public GrownPlant input;
    public IngredientType result;
}
//one thing crushes
//one thing wets
//one thing crystallizes
