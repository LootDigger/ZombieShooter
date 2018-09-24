using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region private fields

    private Rigidbody rb;
    private bool isAlive;

    #endregion
    

    #region serialize Fields

    [SerializeField]
    private FlashLight flashLight;
          
    #endregion
    

    #region Unity lifeCycle
    
    void OnTriggerEnter(Collider other)
    {

        if(other.GetComponent<ConsumableObject>())
        {
            other.GetComponent<ConsumableObject>().Consume();
        }
        
    }


    void Start()
    {        
        StartCoroutine(Shooting());
        EventController.fillFlashlight += FillFlashLight;
    }

    #endregion


    #region private methods
    
    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(0.2f);
        
        StartCoroutine(Shooting());

    }

    void FillFlashLight()
    {
        flashLight.lightPower = Consts.Values.FlashLight.batteryPower;
    }
   

    #endregion

}

