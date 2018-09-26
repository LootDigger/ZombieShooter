using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tod;
public class Battery : ConsumableObject
{

    #region Serializable fields

    [SerializeField]
    private FlashLight flashLight;

    #endregion

    public override void Consume()
    {
        if (flashLight.lightPower == Consts.Values.FlashLight.flashLightlifeCycle)
            return;
        else
            if (flashLight.lightPower < Consts.Values.FlashLight.flashLightlifeCycle)
        {
            EventController.InvokeEvent(Consts.Events.events.fillFlashLight);
            gameObject.GetComponent<UnityPoolObject>().Push();
                       
        }
    }



}
