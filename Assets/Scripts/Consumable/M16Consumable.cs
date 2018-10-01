using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tod;

public class M16Consumable : WeaponConsumable
{

    #region Public methods


    public override void Consume()
    {
        EventController.InvokeEvent(Consts.Events.events.pickUpM16);
        UnityPoolManager.Instance.Push(this.gameObject.GetComponent<UnityPoolObject>());

    }


    #endregion


}
