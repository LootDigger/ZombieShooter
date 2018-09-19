using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FastZombie : Zombie {

    #region Private fields

    private bool isReadyToAttack;
    private Animator thisAnimator;

    #endregion

    #region SerializableFields

    [SerializeField]
    private GameObject medKit;

    [SerializeField]
    private GameObject battery;

    [SerializeField]
    private GameObject boost;

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
            GameConditionsManager.countOfKilledZombies++;
            this.isAlive = false;
            Destroy(this.gameObject);
            EventController.InvokeEvent(Consts.Events.events.reduceZombie);
            EventController.InvokeEvent(Consts.Events.events.addScoreForTheFZ);

            if (Random.Range(1, Consts.Values.Meds.medKitDropChance + 1) == 1)
            {
                Instantiate(medKit, new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);
                Debug.Log("if");
            }
            else
            {
    
            }
            if (Random.Range(1, Consts.Values.FlashLight.batterySpawnChanse + 1) == 1)
            {
                Debug.Log("else");
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
