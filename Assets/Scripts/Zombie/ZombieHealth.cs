using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tod;

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

    void OnDisable()
    {
        Reborn();
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
            UnityPoolManager.Instance.Push(this.gameObject.GetComponent<UnityPoolObject>());
            EventController.InvokeEvent(Consts.Events.events.reduceZombie);
            EventController.InvokeEvent(Consts.Events.events.addScoreForTheFZ);

            EventController.InvokeEvent(Consts.Events.events.spawnLoot);
            Debug.Log("HP is less than zero");
        }
    }




    #endregion



    #region public methods

    public void Reborn()
    {
        isAlive = true;

        if(gameObject.GetComponent<SlowZombie>())
          HP = Consts.Values.Zombie.slowZombieMaxhealth;
        else
            if(gameObject.GetComponent<FastZombie>())
               HP = Consts.Values.Zombie.fastZombieMaxhealth;


    }

    #endregion
}