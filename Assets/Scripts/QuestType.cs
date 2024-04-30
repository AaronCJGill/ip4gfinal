using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestType : ScriptableObject
{
    PotionType desiredPotion;
    public void generateRandomDesiredPotion()
    {
        desiredPotion = ScriptableObject.CreateInstance<PotionType>();
    }
    public void generateStartingPotion()
    {
        desiredPotion = ScriptableObject.CreateInstance<PotionType>(); 
        
    }
}
