using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Tod;

public class FastZombie : Zombie {

    #region Private fields

    private bool isReadyToAttack;
    private Animator thisAnimator;

    #endregion



    #region Unity lifecycle

    void Start()
    {

        isReadyToAttack = true;
        this.isAlive = true;
        thisAgent = GetComponent<NavMeshAgent>();
        thisAnimator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
        EventController.spawnLoot += SpawnLoot;
    }




    void Update()
    {
        CheckDistance();
        if (this.isAlive)
            thisAgent.SetDestination(Player.transform.position);
    }

    #endregion


    #region private methods

    void SpawnLoot()
    {
       
        
            if (Random.Range(1, Consts.Values.Meds.medKitDropChance + 1) == 1)
            {
            //  Instantiate(medKit, new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);

                GameObject go =  UnityPoolManager.Instance.Pop<UnityPoolObject>(2, false).gameObject;
                go.transform.SetPositionAndRotation(new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);
                go.SetActive(true);
          
        }
            else
            {

            }
            if (Random.Range(1, Consts.Values.FlashLight.batterySpawnChanse + 1) == 1)
            {
                Transform go = UnityPoolManager.Instance.Pop<UnityPoolObject>(0, false).GetComponent<Transform>();
                go.transform.SetPositionAndRotation(new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);
               // go.gameObject.SetActive(true);

            }

            if (GameConditionsManager.currentWave >= 2)
            {
                if (GameConditionsManager.numberOfDeadZombies == 9)
                {

                    GameConditionsManager.numberOfDeadZombies = 0;
                    Transform go = UnityPoolManager.Instance.Pop<UnityPoolObject>(1, false).GetComponent<Transform>();
                    go.transform.SetPositionAndRotation(new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);
                    go.gameObject.SetActive(true);
                }
                else
                {
                    GameConditionsManager.numberOfDeadZombies++;
                    Debug.Log("++");
                }
            }



        
    }


    void CheckDistance()
    {
        if (Vector3.Distance(transform.position, this.Player.transform.position) <= Consts.Values.Zombie.attackDistance && isReadyToAttack)
        {
            isReadyToAttack = false;
            thisAgent.speed = 0f;
            StartCoroutine(CoolDown());

        }
        
        if(Vector3.Distance(transform.position, this.Player.transform.position) > Consts.Values.Zombie.attackDistance)
        {
            thisAgent.speed = Consts.Values.Zombie.fastZombieSpeed;
        }

    }

    void TryToAttack()
    {
        if(Vector3.Distance(transform.position, this.Player.transform.position) <= Consts.Values.Zombie.attackDistance)
        {
            EventController.InvokeEvent(Consts.Events.events.fZhitPlayer);
            thisAnimator.SetBool("isAtack", true);
        }
        else
        {
            thisAnimator.SetBool("isAtack", false);

        }

    }
    

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(Consts.Values.Zombie.fastZombieAttackCooldown);
        TryToAttack();
        isReadyToAttack = true;
    }

    #endregion

}
