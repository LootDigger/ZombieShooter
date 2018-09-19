using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Fire : MonoBehaviour {


    #region Private fields

    new  Light  light;
    private float counter;
    private float fireDarknessDelay;

    #endregion


    #region Unity Lifecycle

    void Start()
    {
        EventController.Subscribe(Consts.Events.events.spawnWave, DecreaseRange);
        EventController.Subscribe(Consts.Events.events.replay, Restart);
        fireDarknessDelay = 1.5f;
        light = GetComponent<Light>();
        RecursionLight();
        counter = 0;
    }



    void RecursionLight()
    {
        float def = Random.Range(Consts.Values.Lightning.minLightIntensivity, Consts.Values.Lightning.maxLightIntensivity);
        light.DOIntensity(def, fireDarknessDelay).OnComplete(RecursionLight);  
            
        if(DifficultyController.isMaxim)
        {
            counter++;
            fireDarknessDelay = counter;
            Consts.Values.Lightning.minLightIntensivity = 0;
            Consts.Values.Lightning.maxLightIntensivity = 2f;

        }
        else
        {
            fireDarknessDelay = 1.5f;

            Consts.Values.Lightning.minLightIntensivity = 2f;
            Consts.Values.Lightning.maxLightIntensivity = 4f;

        }



    }

    void Restart()
    {
        Debug.Log("Restart");
        light.range = 100f;        
    }

    void DecreaseRange()
    {
        light.range -= 5f;
    }

   

    #endregion

}
