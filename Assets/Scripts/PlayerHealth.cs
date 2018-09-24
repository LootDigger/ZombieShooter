using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    #region Private fields

    private bool isAlive;

    #endregion


    #region serialize Fields

    [SerializeField]
    private GameObject blood;

    #endregion

    
    #region Properties

    public float Health { get; set; }

    #endregion

    
    #region Unity LifeCycle

    void Start()
    {

        isAlive = false;
        Health = Consts.Values.Player.playermaxHealth;
        EventController.Subscribe(Consts.Events.events.lose, Lose);
        EventController.Subscribe(Consts.Events.events.fZhitPlayer, fZHitPlayer);
        EventController.Subscribe(Consts.Events.events.sZhitPlayer, sZHitPlayer);
        EventController.Subscribe(Consts.Events.events.startGame, StartGame);
        EventController.Subscribe(Consts.Events.events.replay, Replay);

    }



    void FixedUpdate()
    {

        CheckHealth();

    }

    #endregion



    #region private Methods

    void CheckHealth()
    {
        if (Health <= 0)
        {
            EventController.InvokeEvent(Consts.Events.events.lose);
            isAlive = false;
        }
    }

    #endregion


    #region Event Handlers

    void Replay()
    {
        transform.position = Consts.Values.Player.startPos;
        Health = Consts.Values.Player.playermaxHealth;
        EventController.InvokeEvent(Consts.Events.events.updateHealth);
        isAlive = true;
    }


    void fZHitPlayer()
    {
        Health -= Consts.Values.Zombie.fZDamage;
        EventController.InvokeEvent(Consts.Events.events.updateHealth);
        Destroy(Instantiate(blood, transform.position, Quaternion.identity), 1f);
    }


    void sZHitPlayer()
    {
        Health -= Consts.Values.Zombie.sZDamage;
        EventController.InvokeEvent(Consts.Events.events.updateHealth);
        Destroy(Instantiate(blood, transform.position, Quaternion.identity), 1f);

    }



    void Lose()
    {
        isAlive = false;
    }


    void StartGame()
    {
        isAlive = true;
    }


    #endregion

}
