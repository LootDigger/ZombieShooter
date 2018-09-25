using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tod;
public class Battery : ConsumableObject
{

    public override void Consume()
    {

        EventController.InvokeEvent(Consts.Events.events.fillFlashLight);
        gameObject.GetComponent<UnityPoolObject>().Push();
        
    }



}
