using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Zombie : MonoBehaviour {

    #region Serializable fields
    [SerializeField]
    protected float speed;

    [SerializeField]
    protected float maxHP;

    [SerializeField]
    protected float damagePerHit;

    #endregion

}
