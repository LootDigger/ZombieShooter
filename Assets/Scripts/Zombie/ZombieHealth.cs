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

            SpawnLoot();
            Debug.Log("HP is less than zero");
        }
    }




    #endregion



    #region public methods


    void SpawnLoot()
    {
        Debug.Log("Spawn something");

        if (Random.Range(1, Consts.Values.Meds.medKitDropChance + 1) == 1)
        {
            Debug.Log("Spawn Meds");
            GameObject go = UnityPoolManager.Instance.Pop<UnityPoolObject>(2, true).gameObject;
            go.transform.SetPositionAndRotation(new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);

        }
        else
        {

        }
        if (Random.Range(1, Consts.Values.FlashLight.batterySpawnChanse + 1) == 1)
        {
            Debug.Log("Spawn battery");
            GameObject go = UnityPoolManager.Instance.Pop<UnityPoolObject>(0, true).gameObject;
            go.transform.SetPositionAndRotation(new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);

        }

        if (GameConditionsManager.currentWave >= 2)
        {
            if (GameConditionsManager.numberOfDeadZombies == 9)
            {
                Debug.Log("Spawn booster");

                GameConditionsManager.numberOfDeadZombies = 0;
                GameObject go = UnityPoolManager.Instance.Pop<UnityPoolObject>(1, true).gameObject;
                go.transform.SetPositionAndRotation(new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);
            }
            else
            {
                GameConditionsManager.numberOfDeadZombies++;
                Debug.Log("++");
            }
        }




    }

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