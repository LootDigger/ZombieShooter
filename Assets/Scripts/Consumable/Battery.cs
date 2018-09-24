using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : ConsumableObject
{

    public override void Consume()
    {

        EventController.InvokeEvent(Consts.Events.events.fillFlashLight);
        Destroy(this.gameObject);
        
    }



}
