using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : ConsumableObject
{

    public override void Consume()
    {
        EventController.InvokeEvent(Consts.Events.events.upgradeWeapon);
        Destroy(this.gameObject);
    }


}
