using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tod;

public class Booster : ConsumableObject
{

    public override void Consume()
    {
        EventController.InvokeEvent(Consts.Events.events.upgradeWeapon);
        gameObject.GetComponent<UnityPoolObject>().Push();
    }


}
