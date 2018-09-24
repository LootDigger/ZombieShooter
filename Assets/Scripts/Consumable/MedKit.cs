using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tod;

public class MedKit : ConsumableObject
{


    public override void Consume()
    {
        PlayerHealth plh = GameObject.Find("Player").GetComponent<PlayerHealth>();

        if (plh.Health >= Consts.Values.Player.playermaxHealth)
            return;
        else
            gameObject.GetComponent<UnityPoolObject>().Push();  //need to setActive(false) in future

        if ((plh.Health + Consts.Values.Meds.medKitCureEffect) >= Consts.Values.Player.playermaxHealth)
        {

            plh.Health = Consts.Values.Player.playermaxHealth;
        }
        else
            plh.Health += Consts.Values.Meds.medKitCureEffect;

        EventController.InvokeEvent(Consts.Events.events.updateHealth);

      
    }

}
