using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FastZombie : Zombie {

    #region Private fields

    private bool isReadyToAttack;

    #endregion

    #region SerializableFields

    [SerializeField]
    private GameObject medKit;

    #endregion


    #region Unity lifecycle

    void Start()
    {
        isReadyToAttack = true;
        this.isAlive = true;
        thisAgent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");

    }


    void Update()
    {
        CheckDistance();
        CheckHealth();
        if (this.isAlive)
            thisAgent.SetDestination(Player.transform.position);
    }

    #endregion


    #region private methods

    void CheckHealth()
    {
        if (HP <= 0)
        {
            this.isAlive = false;
            Destroy(this.gameObject);
            EventController.InvokeEvent(Consts.Events.events.reduceZombie);
            EventController.InvokeEvent(Consts.Events.events.addScoreForTheFZ);

            if (Random.Range(1, Consts.Values.medKitDropChance + 1) == 1)
            {
                Instantiate(medKit, new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);
            }

        }
    }


    void CheckDistance()
    {
        if (Vector3.Distance(transform.position, this.Player.transform.position) <= Consts.Values.attackDistance && isReadyToAttack)
        {
            isReadyToAttack = false;
            thisAgent.speed = 0f;
            StartCoroutine(CoolDown());

        }
        
        if(Vector3.Distance(transform.position, this.Player.transform.position) > Consts.Values.attackDistance)
        {
            thisAgent.speed = Consts.Values.fastZombieSpeed;
        }

    }

    void TryToAttack()
    {
        if(Vector3.Distance(transform.position, this.Player.transform.position) <= Consts.Values.attackDistance)
        {
            EventController.InvokeEvent(Consts.Events.events.fZhitPlayer);
        }
        
    }
    

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(Consts.Values.fastZombieAttackCooldown);
        TryToAttack();
        isReadyToAttack = true;
    }

    #endregion

}
