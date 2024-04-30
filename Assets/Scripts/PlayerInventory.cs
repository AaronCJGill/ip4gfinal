using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;
    public int money = 0;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }


    }
    [SerializeField]
    private GameObject wateringCan, trowel;
    private playerEquipped currentEquipped = playerEquipped.wateringcan; 
    public static playerEquipped Equipped { get { return instance.currentEquipped; } }
    [SerializeField]
    public PlantType equippedPlantSeed;

    public List<IngredientType> playerIngredients = new List<IngredientType>();
    public List<GrownPlant> playerOwnedHarvestedPlants = new List<GrownPlant>();
    public List<PotionType> playerOwnedPotions = new List<PotionType>();

    public int playerShrubSeeds = 1;
    public int playerFlowerSeeds = 1;

    public bool playershrubequipped = true;
    public bool playerflowerequipped = false;

    public GameObject shrubEquippedUI;
    public GameObject flowerEquippedUI;

    public TextMeshProUGUI flowerseedsText;
    public TextMeshProUGUI shrubSeedsText;
    public GameObject flowerImageUI;
    public GameObject shrubImageUI;

    public TextMeshProUGUI ingredientsText;

    [SerializeField]
    GrownPlant shrubref;
    [SerializeField]
    GrownPlant flowerref;

    [SerializeField]
    public PlantType shrubSeedref;
    [SerializeField]
    public PlantType flowerSeedref;

    [SerializeField]
    public TextMeshProUGUI moneyText;

    [SerializeField]
    public TextMeshProUGUI dayText;
    private void Update()
    {
        handleEquipped();

        flowerseedsText.text = "Flower seeds left: " + playerFlowerSeeds;
        shrubSeedsText.text = "Shrub seeds left: " + playerShrubSeeds;

        moneyText.text = "Money: " + money;
        dayText.text = "Day: " + DayManager.instance.DAYCOUNT;
        
        string igt = "";
        for (int i = 0; i < playerIngredients.Count; i++)
        {
            igt += playerIngredients[i].plantIngredient + " " + playerIngredients[i].typeofingredient + "\n";
        }
        //igt += "----------------\n";
        for (int i = 0; i < playerOwnedHarvestedPlants.Count; i++)
        {
            igt += "Fully grown " + playerOwnedHarvestedPlants[i].plantclass + "\n";
        }
        //igt += "----------------\n";
        for (int i = 0; i < playerOwnedPotions.Count; i++)
        {
            igt += "Potion of " + playerOwnedPotions[i].result + "\n";
        }
        ingredientsText.text = igt;

    }
    public void removeAnyPotion()
    {
        if (playerOwnedPotions.Count > 0)
        {
            playerOwnedPotions.RemoveAt(0);
        }
    }
    void handleEquipped()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //watering can
            currentEquipped = playerEquipped.wateringcan;
            trowel.SetActive(false);
            wateringCan.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //trowel
            currentEquipped = playerEquipped.seed;
            trowel.SetActive(true);
            wateringCan.SetActive(false);

                playerflowerequipped = false;
                playershrubequipped = true;
                
                flowerseedsText.gameObject.SetActive(false);
                shrubImageUI.SetActive(true);
                shrubSeedsText.gameObject.SetActive(true);
                flowerImageUI.SetActive(false);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //nothing equipped
            currentEquipped = playerEquipped.nothing;
            trowel.SetActive(false);
            wateringCan.SetActive(false);
        }

        if (currentEquipped == playerEquipped.seed && Input.GetKeyDown(KeyCode.Tab))
        {
            if (playerflowerequipped == true)
            {
                playerflowerequipped = false;
                playershrubequipped = true;
                flowerseedsText.gameObject.SetActive(false);
                shrubImageUI.SetActive(true);
                shrubSeedsText.gameObject.SetActive(true);
                flowerImageUI.SetActive(false);
            }
            else
            {
                playerflowerequipped = true;
                playershrubequipped = false;
                flowerseedsText.gameObject.SetActive(true);
                shrubImageUI.SetActive(false);
                shrubSeedsText.gameObject.SetActive(false);
                flowerImageUI.SetActive(true);
            }
        }

    }

    
    public void removePlant(GrownPlant gp)
    {

        for (int i = 0; i < playerOwnedHarvestedPlants.Count; i++)
        {
            if (playerOwnedHarvestedPlants[i].plantclass == gp.plantclass)
            {
                playerOwnedHarvestedPlants.RemoveAt(i);
                break;
            }
        }
    }

    public void addPlantShrub()
    {
        playerOwnedHarvestedPlants.Add(shrubref);
    }
    public void addPlantFlower()
    {
        playerOwnedHarvestedPlants.Add(flowerref);
    }
    public void addPlant(plantclasses pc)
    {

        GrownPlant x = ScriptableObject.CreateInstance<GrownPlant>();
        x.plantclass = pc;
        playerOwnedHarvestedPlants.Add(x);
    }

    public bool checkIfPlayerHasPlantType(GrownPlant gp)
    {
        for (int i = 0; i < playerOwnedHarvestedPlants.Count; i++)
        {
            if (playerOwnedHarvestedPlants[i].plantclass == gp.plantclass)
            {
                return true;
            }
        }
        return false;
    }
    public bool checkIfPlayerHasIngredient(IngredientType it)
    {
        for (int i = 0; i < playerIngredients.Count; i++)
        {
            if (playerIngredients[i].plantIngredient == it.plantIngredient && playerIngredients[i].typeofingredient == it.typeofingredient)
            {
                return true;
            }
        }
        return false;
    }
    public void removeIngredient(IngredientType ig)
    {
        for (int i = 0; i < playerIngredients.Count; i++)
        {
            if (playerIngredients[i].plantIngredient == ig.plantIngredient && playerIngredients[i].typeofingredient == ig.typeofingredient)
            {
                playerIngredients.RemoveAt(i);
                break;
            }
        }
    }
    public void removeIngredients(IngredientType ig)
    {
        int count = 0;
        int pos1 = 50000;
        int pos2 = 50000;
        for (int i = 0; i < playerIngredients.Count; i++)
        {
            if (playerIngredients[i].plantIngredient == ig.plantIngredient && playerIngredients[i].typeofingredient == ig.typeofingredient)
            {
                count++;
                if (pos1 == 50000)
                {
                    pos1 = i;
                }
                else if (pos2 == 50000)
                {
                    pos2 = i;
                }
            }
        }
        if (count > 1 && pos1 != pos2)
        {
            playerIngredients.RemoveAt(pos2);
            playerIngredients.RemoveAt(pos1);
        }
    }
    public void addIngredient(IngredientType ig)
    {
        IngredientType x = ScriptableObject.CreateInstance<IngredientType>();
        x.plantIngredient = ig.plantIngredient;
        x.typeofingredient = ig.typeofingredient;
        playerIngredients.Add(x);
    }
    public bool checkIfPlayerHasPotion(PotionType pt)
    {
        for (int i = 0; i < playerOwnedPotions.Count; i++)
        {
            if (playerOwnedPotions[i].result == pt.result)
            {
                return true;
            }
        }
        return false;
    }

    public void removePotion(PotionType pt)
    {
        for (int i = 0; i < playerOwnedPotions.Count; i++)
        {
            if (playerOwnedPotions[i].result == pt.result)
            {
                playerOwnedPotions.RemoveAt(i);
                break;
            }
        }
    }
    public void addPotion(PotionType pt)
    {
        playerOwnedPotions.Add(pt);
    }
}
public enum playerEquipped
{
    nothing, 
    wateringcan,
    seed
}