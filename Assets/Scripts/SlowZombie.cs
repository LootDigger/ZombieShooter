using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowZombie : Zombie {

     #region Unity lifecycle
        
    void Start()
    {
        this.isAlive = true;
        thisAgent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        CheckHealth();
        if (this.isAlive)
            thisAgent.SetDestination(Player.transform.position);
    }

    #endregion


    #region private methods

    void CheckHealth()
    {
        if(HP<=0)
        {
            this.isAlive = false;
            Destroy(this.gameObject);
        }
    }

    #endregion
}
