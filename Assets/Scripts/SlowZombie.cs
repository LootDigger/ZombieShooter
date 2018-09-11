﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowZombie : Zombie
{
    #region Private fields

    private bool isReadyToAttack;

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
        if(HP<=0)
        {
            this.isAlive = false;
            Destroy(this.gameObject);
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

        if (Vector3.Distance(transform.position, this.Player.transform.position) > Consts.Values.attackDistance)
        {
            thisAgent.speed = Consts.Values.slowZombieSpeed;
        }

    }



    void TryToAttack()
    {
        if (Vector3.Distance(transform.position, this.Player.transform.position) <= Consts.Values.attackDistance)
        {
            EventController.InvokeEvent(Consts.Events.events.hitPlayer);
        }

    }


    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(Consts.Values.zombieAttackCooldown);
        TryToAttack();
        isReadyToAttack = true;
    }


    #endregion
}
