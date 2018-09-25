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

    }


    void Update()
    {
        CheckDistance();
        if (this.isAlive)
            thisAgent.SetDestination(Player.transform.position);
    }


    #endregion


    #region private methods


   


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
