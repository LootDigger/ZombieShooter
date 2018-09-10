using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Zombie : MonoBehaviour {

    #region Protected fields

    protected NavMeshAgent thisAgent;
    protected GameObject Player;
    protected bool isAlive;

    #endregion


    #region Serializable fields

    [SerializeField]
    protected float speed;

    [SerializeField]
    protected float HP;

    [SerializeField]
    protected float damagePerHit;

    #endregion


    #region Unity lifeCycle

    void Start()
    {
        isAlive = true;
             
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            HP -= 10f;
            Destroy(other.gameObject);
        }

    }

    #endregion


    #region private Methods


    #endregion

}
