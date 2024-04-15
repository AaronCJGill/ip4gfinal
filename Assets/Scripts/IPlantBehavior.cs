using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlantBehavior 
{
    //should grow, have a grow size
    //public void interact();//gives information on whether or not it is able to be harvested
    public void grow();//grows every day
    public void harvest();//is able to be harvested
    public void water();


}
