using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConditionsManager:MonoBehaviour
{



    #region public fields

    public static int mainScore;
    public static int currentWave = 0;
    public static int numberOfDeadZombies = 0;
    public static int countOfKilledZombies = 0;

    #endregion




    #region Unity LifeCycle

    #endregion

    #region public Methods

    public void Replay()
    {
        mainScore = 0;
        currentWave = 0 ;
        EventController.InvokeEvent(Consts.Events.events.replay);
        EventController.InvokeEvent(Consts.Events.events.spawnWave);
        countOfKilledZombies = 0;

    }



    #endregion


}
