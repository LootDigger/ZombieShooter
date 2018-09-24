using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour {

    #region Protected fields

    
    protected bool isAlive;

    #endregion

    #region Private fields

    private Animator thisAnimator;

    #endregion

    #region Serializable fields

    [SerializeField]
    protected float HP;

    #endregion


    #region Unity lifeCycle

    void Start()
    {
        isAlive = true;
        thisAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>())
        {
            HP -= 10f;
            Destroy(other.gameObject);
           
        }
        thisAnimator.SetTrigger("isShoted");
        thisAnimator.SetTrigger("exitShotAnim");

    }

    void Update ()
    {
        CheckHealth();

    }

    #endregion



    #region private methods


    void CheckHealth()
    {
        if (HP <= 0)
        {
            GameConditionsManager.countOfKilledZombies++;
            this.isAlive = false;
            gameObject.SetActive(false);
            EventController.InvokeEvent(Consts.Events.events.reduceZombie);
            EventController.InvokeEvent(Consts.Events.events.addScoreForTheFZ);

            EventController.InvokeEvent(Consts.Events.events.spawnLoot);
            Debug.Log("HP is less than zero");
        }
    }


    #endregion
}
