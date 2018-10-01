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

    [SerializeField]
    private GameObject blood;

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
            UnityPoolManager.Instance.Push(other.gameObject.GetComponent<UnityPoolObject>());
            thisAnimator.SetTrigger("isShoted");
        }

        if (other.GetComponent<SniperBullet>())
        {
            HP -= 50f;
            UnityPoolManager.Instance.Push(other.gameObject.GetComponent<UnityPoolObject>());
            thisAnimator.SetTrigger("isShoted");
        }

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
            GameConditionsManager.countOfKilledZombiesInCurrentWave++;
            Destroy(Instantiate(blood, transform.position, Quaternion.identity), 1f);

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
        Debug.Log("++tanks");
        Debug.Log("Spawn something");

     

        if (GameConditionsManager.currentWave == 5 && GameConditionsManager.countOfKilledZombiesInCurrentWave == 1)
        {
                    
            Debug.Log("Spawn m14");
            GameObject go = UnityPoolManager.Instance.Pop<UnityPoolObject>(9, true).gameObject;
            go.transform.SetPositionAndRotation(new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.Euler(0, 0, 90));
            GameConditionsManager.loot.Add(go.GetComponent<UnityPoolObject>());

        }


        if (GameConditionsManager.currentWave == 10 && GameConditionsManager.countOfKilledZombiesInCurrentWave == 1)
        {
            // GameConditionsManager.countOfKilledTanks = 0;
            Debug.Log("Spawn m16");
            GameObject go = UnityPoolManager.Instance.Pop<UnityPoolObject>(7, true).gameObject;
            go.transform.SetPositionAndRotation(new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.Euler(0,0,90));
            GameConditionsManager.loot.Add(go.GetComponent<UnityPoolObject>());
        }


        if (GameConditionsManager.currentWave >= 2)
        {




            if (gameObject.GetComponent<SlowZombie>())
            {
                Debug.Log("++tanks");
                GameConditionsManager.numberOfDeadZombies++;
                GameConditionsManager.countOfKilledTanks++;
            }
            Debug.Log("Количество убитых танков == " + GameConditionsManager.countOfKilledTanks);

            //if (GameConditionsManager.countOfKilledTanks == 5 && gameObject.GetComponent<SlowZombie>())
            //{
            //    GameConditionsManager.countOfKilledTanks = 0;
            //    Debug.Log("Spawn m16");
            //    GameObject go = UnityPoolManager.Instance.Pop<UnityPoolObject>(7, true).gameObject;
            //    go.transform.SetPositionAndRotation(new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);
            //    GameConditionsManager.loot.Add(go.GetComponent<UnityPoolObject>());
            //}


           

            if (GameConditionsManager.numberOfDeadZombies == 4  && gameObject.GetComponent<SlowZombie>())
         {
                    Debug.Log("Spawn booster");

                    GameConditionsManager.numberOfDeadZombies = 0;
                    GameObject go = UnityPoolManager.Instance.Pop<UnityPoolObject>(1, true).gameObject;
                    go.transform.SetPositionAndRotation(new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);
                    GameConditionsManager.loot.Add(go.GetComponent<UnityPoolObject>());
         }
         
           
            else
            {


                if (Random.Range(1, Consts.Values.Meds.medKitDropChance + 1) == 1)
                {
                    Debug.Log("Spawn Meds");
                    GameObject go = UnityPoolManager.Instance.Pop<UnityPoolObject>(2, true).gameObject;
                    go.transform.SetPositionAndRotation(new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);
                    GameConditionsManager.loot.Add(go.GetComponent<UnityPoolObject>());

                }
                else
                    if (Random.Range(1, Consts.Values.FlashLight.batterySpawnChanse + 1) == 1)
                {
                    Debug.Log("Spawn battery");
                    GameObject go = UnityPoolManager.Instance.Pop<UnityPoolObject>(0, true).gameObject;
                    go.transform.SetPositionAndRotation(new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);
                    GameConditionsManager.loot.Add(go.GetComponent<UnityPoolObject>());

                }
            }
            
        }




    }

    public void Reborn()
    {
        isAlive = true;

        if (gameObject.GetComponent<SlowZombie>())
        {
            HP = Consts.Values.Zombie.slowZombieMaxhealth;

            if (GameConditionsManager.currentWave >= 9)
                HP += GameConditionsManager.currentWave;    




        }
        else
            if (gameObject.GetComponent<FastZombie>())
            HP = Consts.Values.Zombie.fastZombieMaxhealth;


    }

    #endregion
}