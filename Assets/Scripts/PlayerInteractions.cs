using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField]
    GameObject emptyPlantObj;
    float interactDistance = 5f;
    //sends out a ray to the main camera going for about 1 unit, 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                //Debug.Log("Hit object" + hit.collider.gameObject);
                //Debug.Log(hit);
                interact(hit.transform.gameObject, hit.point);
                
            }
        }
    }
    void interact(GameObject objectToInteractWith)
    {
        //simple check if the object has the interface
        if (objectToInteractWith.TryGetComponent(out IInteractable interaction))
        {
            //didnt originally know it can be called like this, but it can!
            interaction.interact();
        }
    }

    void interact(GameObject objectToInteractWith, Vector3 point)
    {

        //simple check if the object has the interface
        if (objectToInteractWith.TryGetComponent(out IInteractable interaction))
        {
            //didnt originally know it can be called like this, but it can!
            //Debug.Log(hit.point + "hitt");
            interaction.interact();
        }
        else if (objectToInteractWith.CompareTag("flowerbed") && PlayerInventory.Equipped == playerEquipped.seed)
        {
            //Debug.Log(hit.point);

            GameObject g;
            PlantBase pb;
            //Debug.Log(g.name);
            if (PlayerInventory.instance.playerflowerequipped && PlayerInventory.instance.playerFlowerSeeds > 0)
            {
                g = Instantiate(emptyPlantObj, point, Quaternion.identity);
                pb = g.GetComponent<PlantBase>();
                //PlayerInventory.instance.addPlantFlower();
                pb.Init(PlayerInventory.instance.flowerSeedref);
                PlantManager.newPlantMade(pb);
                PlayerInventory.instance.playerFlowerSeeds--;

            }
            else if(PlayerInventory.instance.playershrubequipped && PlayerInventory.instance.playerShrubSeeds> 0)
            {
                Debug.Log("spawn");
                g = Instantiate(emptyPlantObj, point, Quaternion.identity);
                pb = g.GetComponent<PlantBase>();
                //PlayerInventory.instance.addPlantShrub();
                PlayerInventory.instance.playerShrubSeeds--;
                pb.Init(PlayerInventory.instance.shrubSeedref);
                PlantManager.newPlantMade(pb);

            }
        }

    }

    private void OnDrawGizmos()
    {

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Gizmos.DrawRay(ray);
    }
}
