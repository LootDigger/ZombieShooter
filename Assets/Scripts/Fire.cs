using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Fire : MonoBehaviour {


    #region Private fields

    Light light;

    #endregion


    #region Unity Lifecycle

    void Start()
    {
        light = GetComponent<Light>();
        RecursionLight();
    }



    void RecursionLight()
    {
        float def = Random.Range(Consts.Values.minLightIntensivity, Consts.Values.maxLightIntensivity);
        light.DOIntensity(def, 1.5f).OnComplete(RecursionLight);  
            
        if(DifficultyController.isCurrentLevelHavePickDifficulty)
        {
            Consts.Values.minLightIntensivity = 0f;
        }
        else
        {
            Consts.Values.minLightIntensivity = 2f;
        }

    }


   

    #endregion

}
