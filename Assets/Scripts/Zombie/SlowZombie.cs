using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowZombie : Zombie
{
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
        Debug.Log("spawnLoot");
        if (Random.Range(1, Consts.Values.Meds.medKitDropChance + 1) == 1)
        {
            //Instantiate(medKit, new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);
           
        }
        else
        {

        }
        if (Random.Range(1, Consts.Values.FlashLight.batterySpawnChanse + 1) == 1)
        {
            Instantiate(battery, new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);

        }

        if (GameConditionsManager.currentWave >= 2)
        {
            if (GameConditionsManager.numberOfDeadZombies == 9)
            {

                GameConditionsManager.numberOfDeadZombies = 0;
                Instantiate(boost, new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);
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

        if (Vector3.Distance(transform.position, this.Player.transform.position) > Consts.Values.Zombie.attackDistance)
        {
            thisAgent.speed = Consts.Values.Zombie.slowZombieSpeed;
        }

    }

       
    void TryToAttack()
    {
        if (Vector3.Distance(transform.position, this.Player.transform.position) <= Consts.Values.Zombie.attackDistance)
        {
            EventController.InvokeEvent(Consts.Events.events.sZhitPlayer);
            thisAnimator.SetBool("isAtack", true);

        }
        else
        {
            thisAnimator.SetBool("isAtack", false);

        }

    }


    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(Consts.Values.Zombie.slowZombieAttackCooldown);
        TryToAttack();
        isReadyToAttack = true;
    }


    #endregion
}
