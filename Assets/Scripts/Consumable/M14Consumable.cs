﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M14Consumable : WeaponConsumable {

    #region Public methods


    public override void Consume()
    {
        EventController.InvokeEvent(Consts.Events.events.pickUpM14);
        Destroy(this.gameObject);

    }


    #endregion

}