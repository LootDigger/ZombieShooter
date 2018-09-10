using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZombie : Zombie {


    #region Unity lifecycle

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
            Debug.Log("Shot him!");

    }


    #endregion


}
