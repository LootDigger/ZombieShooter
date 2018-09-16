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

    #region Serializable fields

    [SerializeField]
    private GameObject medKit;


    [SerializeField]
    private GameObject boost;

    #endregion


    #region Unity lifecycle

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            HP -= 10f;
            Destroy(other.gameObject);
        }
        thisAnimator.SetTrigger("isShoted");
        thisAnimator.SetTrigger("exitShotAnim");

    }


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

            if(Random.Range(1f,Consts.Values.Meds.medKitDropChance) == 1f)
            {
                Instantiate(medKit, new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);
            }

            EventController.InvokeEvent(Consts.Events.events.reduceZombie);
            EventController.InvokeEvent(Consts.Events.events.addScoreForTheSZ);

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
