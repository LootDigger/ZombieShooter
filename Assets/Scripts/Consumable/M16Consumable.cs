using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M16Consumable : WeaponConsumable
{

    #region Public methods


    public override void Consume()
    {
        EventController.InvokeEvent(Consts.Events.events.pickUpM16);
        Destroy(this.gameObject);

    }


    #endregion


}
