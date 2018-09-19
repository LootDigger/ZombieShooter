using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour {


    #region private fields

    private Light light;
    private bool isTurnedOn;

    #endregion

    #region Properties

    public float lightPower;


    #endregion

    #region Unity Lifecycle

    void Start ()
    {

        EventController.Subscribe(Consts.Events.events.flashLightTurned, TurnOn);
        EventController.Subscribe(Consts.Events.events.replay, Restart);

        lightPower = Consts.Values.FlashLight.flashLightlifeCycle;
        isTurnedOn = false;
        light = GetComponent<Light>();

    }
	

	void Update ()
    {
        if(isTurnedOn && lightPower >= 0)
        {
            lightPower -= Time.fixedDeltaTime;
            Debug.Log(lightPower);
            light.intensity = lightPower * 6.666666f;
        }

	}
    
    #endregion



    #region Private methods

    void TurnOn()
    {
        isTurnedOn = !isTurnedOn;
        light.enabled = isTurnedOn;
    }


    void Restart()
    {
        if (isTurnedOn)
            isTurnedOn = false;
        light.enabled = isTurnedOn;
        lightPower = Consts.Values.FlashLight.flashLightlifeCycle;
    }


    #endregion
}
