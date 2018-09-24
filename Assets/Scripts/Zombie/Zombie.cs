using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Zombie : MonoBehaviour
{

    #region Protected fields

    protected NavMeshAgent thisAgent;
    protected GameObject Player;
    protected bool isAlive;

    #endregion


    #region SerializableFields

   

    [SerializeField]
    protected GameObject medKit;

    [SerializeField]
    protected GameObject battery;

    [SerializeField]
    protected GameObject boost;

    #endregion


}
