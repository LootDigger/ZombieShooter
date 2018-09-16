using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventController
{


    static event Action fZHitPlayer;
    static event Action sZHitPlayer;
    static event Action startGame;
    static event Action updateHealthBar;
    static event Action spawnWave;
    static event Action lose;
    static event Action replay;
    static event Action spawnNewWave;
    static event Action reduceZombie;
    static event Action addScoreForTheFZ;
    static event Action addScoreForTheSZ;
    static event Action pause;
    static event Action upgradeWeapon;


    public static void Subscribe(Consts.Events.events gameEvent, Action method)
    {

        if (gameEvent == Consts.Events.events.fZhitPlayer)
        {
            fZHitPlayer += method;
        }


        if (gameEvent == Consts.Events.events.upgradeWeapon)
        {
            upgradeWeapon += method;
        }

        if (gameEvent == Consts.Events.events.pause)
        {
            pause += method;
        }


        if (gameEvent == Consts.Events.events.sZhitPlayer)
        {
            sZHitPlayer += method;
        }


        if (gameEvent == Consts.Events.events.addScoreForTheFZ)
        {
            addScoreForTheFZ += method;
        }


        if (gameEvent == Consts.Events.events.addScoreForTheSZ)
        {
            addScoreForTheSZ += method;
        }

        
        if (gameEvent == Consts.Events.events.reduceZombie)
        {
            reduceZombie += method;
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

  
    public static void InvokeEvent(Consts.Events.events eventParametr)
    {
        if (eventParametr == Consts.Events.events.fZhitPlayer)
        {
            fZHitPlayer();
        }


        if (eventParametr == Consts.Events.events.upgradeWeapon)
        {
            upgradeWeapon();
        }


        if (eventParametr == Consts.Events.events.pause)
        {
            pause();
        }


        if (eventParametr == Consts.Events.events.sZhitPlayer)
        {
            sZHitPlayer();
        }

        if (eventParametr == Consts.Events.events.addScoreForTheFZ)
        {
            addScoreForTheFZ();
        }


        if (eventParametr == Consts.Events.events.addScoreForTheSZ)
        {
            addScoreForTheSZ();
        }

        if (eventParametr == Consts.Events.events.reduceZombie)
        {
            reduceZombie();
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
