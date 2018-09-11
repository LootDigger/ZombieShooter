using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventController
{


    static event Action hitPlayer;
    static event Action startGame;
    static event Action updateHealthBar;
    static event Action spawnWave;
    static event Action lose;
    static event Action replay;


    public static void Subscribe(Consts.Events.events gameEvent, Action method)
    {

        if (gameEvent == Consts.Events.events.hitPlayer)
        {
            hitPlayer += method;
        }


        if (gameEvent == Consts.Events.events.spawnWave)
        {
            spawnWave += method;
        }



        if (gameEvent == Consts.Events.events.updateHealth)
        {
            updateHealthBar += method;
        }


        if (gameEvent == Consts.Events.events.startGame)
        {
            startGame += method;
        }

               

        if (gameEvent == Consts.Events.events.lose)
        {
            lose += method;
        }
               


        if (gameEvent == Consts.Events.events.replay)
        {
            replay += method;
        }
    }

    public static void Unsubscribe(Consts.Events.events gameEvent, Action method)
    {

        if (gameEvent == Consts.Events.events.updateHealth)
        {
            updateHealthBar -= method;
        }

        if (gameEvent == Consts.Events.events.spawnWave)
        {
            spawnWave -= method;
        }



        if (gameEvent == Consts.Events.events.hitPlayer)
        {
            hitPlayer -= method;
        }


        if (gameEvent == Consts.Events.events.startGame)
        {
            startGame -= method;
        }

        

        

        if (gameEvent == Consts.Events.events.lose)
        {
            lose -= method;
        }


        

        if (gameEvent == Consts.Events.events.replay)
        {
            replay -= method;
        }

    }

    public static void InvokeEvent(Consts.Events.events eventParametr)
    {
        if (eventParametr == Consts.Events.events.hitPlayer)
        {
            hitPlayer();
        }


        if (eventParametr == Consts.Events.events.spawnWave)
        {
            spawnWave();
        }

        if (eventParametr == Consts.Events.events.updateHealth)
        {
            updateHealthBar();
        }


        if (eventParametr == Consts.Events.events.startGame)
        {
            startGame();
        }
                    

        if (eventParametr == Consts.Events.events.lose)
        {
            lose();
        }

        

        if (eventParametr == Consts.Events.events.replay)
        {
            replay();
        }

    }


}
