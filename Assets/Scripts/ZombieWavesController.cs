using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWavesController : MonoBehaviour
{

    #region Private fields

    private double spawnCount;
    private int zombiesAliveCount;
    private int currentWaveNumber;
    private double difficulty;
    private bool isGameStarted;
    private bool isGamePaused;
    private GameObject[] zombieArray;
    private GameObject[] meds;
    private GameObject[] boosts;
    #endregion


    #region Serializable fields

    [SerializeField]
    private GameObject fastZombie;

    [SerializeField]
    private GameObject slowZombie;

    [SerializeField]
    private GameObject Player;

    #endregion
    


    #region Unity lifecycle

    void Start()
    {
        isGameStarted = false;
        difficulty = 0;
        currentWaveNumber = 0;
        EventController.Subscribe(Consts.Events.events.spawnWave, SpawnWave);
        EventController.Subscribe(Consts.Events.events.pause, PauseGame);
        EventController.Subscribe(Consts.Events.events.reduceZombie, ReduceCount);
        EventController.Subscribe(Consts.Events.events.replay, Replay);
    }


    void Update()
    {

        if ( isGameStarted && zombiesAliveCount == 0)
        {
            EventController.InvokeEvent(Consts.Events.events.spawnWave);
        }
    }


    #endregion

    

    #region public methods




    public void SpawnWave()
    {

        GameConditionsManager.currentWave++;
        EventController.InvokeEvent(Consts.Events.events.updateWaveUI);
        if (!isGameStarted)
        {
            EventController.InvokeEvent(Consts.Events.events.startGame);
            isGameStarted = true;
        }

        

        difficulty = DifficultyController.CalculateDifficulty(GameConditionsManager.currentWave);



        if (DifficultyController.isMaxim)
        {
            spawnCount =  GameConditionsManager.currentWave * difficulty * Consts.Values.Balance.RisingCoef;
        }

        else if (DifficultyController.isMinim)
        {
            spawnCount = GameConditionsManager.currentWave * difficulty;
            spawnCount /= Consts.Values.Balance.loweringCoef;
        }
        else
        {
            spawnCount =  GameConditionsManager.currentWave * difficulty * 2 ;
            Debug.Log("Calculate!");     
        }

       

        int tmpCount = (int)(spawnCount / 4);

        if (tmpCount < 1)
        {
            tmpCount = 1;
        }

        zombiesAliveCount = tmpCount * 4;
        Debug.Log("tmp count " + tmpCount);

        Debug.Log("zombie count " + zombiesAliveCount);

      


        for (int i = 0; i < tmpCount; i++)
        {
            Vector3 spawnPos = new Vector3(-tmpCount + i * 2, 1f, Player.transform.position.z + Consts.Values.Zombie.zombieSpawnDistance);
            if (i % 2 == 0)
            {
                spawnPos.z *= 2f;
            }
            int tmp = Random.Range(1, Consts.Values.Zombie.slowZombieSpawnRate + 1);


            if (tmp == Consts.Values.Zombie.slowZombieSpawnRate && DifficultyController.isMaxim)
            {
                Instantiate(slowZombie, spawnPos, Quaternion.identity);
            }
            else
            {
                Instantiate(fastZombie, spawnPos, Quaternion.identity);

            }
        }


        for (int i = 0; i < tmpCount; i++)
        {
            Vector3 spawnPos = new Vector3(-tmpCount + i * 2, 1f, Player.transform.position.z - Consts.Values.Zombie.zombieSpawnDistance);
            if (i % 2 == 0)
            {
                spawnPos.z *= 2f;
            }

            int tmp = Random.Range(1, Consts.Values.Zombie.slowZombieSpawnRate + 1);


            if (tmp == Consts.Values.Zombie.slowZombieSpawnRate)
            {
                Instantiate(slowZombie, spawnPos, Quaternion.identity);
            }
            else
            {
                Instantiate(fastZombie, spawnPos, Quaternion.identity);

            }
        }


        for (int i = 0; i < tmpCount; i++)
        {
            Vector3 spawnPos = new Vector3(Player.transform.position.x - Consts.Values.Zombie.zombieSpawnDistance, 1f, Player.transform.position.z - tmpCount + i * 2);
            if (i % 2 == 0)
            {
                spawnPos.x *= 2f;
            }

            int tmp = Random.Range(1, Consts.Values.Zombie.slowZombieSpawnRate + 1);
            if (tmp == Consts.Values.Zombie.slowZombieSpawnRate)
            {
                Instantiate(slowZombie, spawnPos, Quaternion.identity);
            }
            else
            {
                Instantiate(fastZombie, spawnPos, Quaternion.identity);
            }
        }


        for (int i = 0; i < tmpCount; i++)
        {
            Vector3 spawnPos = new Vector3(Player.transform.position.x + Consts.Values.Zombie.zombieSpawnDistance, 1f, Player.transform.position.z - tmpCount + i * 2);
            if (i % 2 == 0)
            {
                spawnPos.x *= 2f;
            }

            int tmp = Random.Range(1, Consts.Values.Zombie.slowZombieSpawnRate + 1);

            if (tmp == Consts.Values.Zombie.slowZombieSpawnRate)
            {
                Instantiate(slowZombie, spawnPos, Quaternion.identity);
            }
            else
            {
                Instantiate(fastZombie, spawnPos, Quaternion.identity);

            }
        }

    }

    #endregion

    #region private methods

    void Replay()
    {
        boosts = GameObject.FindGameObjectsWithTag("Booster");

        foreach(GameObject boosters in boosts)
        {
            Destroy(boosters);
        }

        meds = GameObject.FindGameObjectsWithTag("MedKit");

        foreach (GameObject medKits in meds)
        {
            Destroy(medKits);
        }


        zombieArray =  GameObject.FindGameObjectsWithTag("Zombie");


        foreach(GameObject zmb in zombieArray)
        {
            Destroy(zmb);
        }


    }


    void PauseGame()
    {
        if (!isGamePaused)
        {
            Time.timeScale = 0;
            isGamePaused = true;
        }
        else
        {
            Time.timeScale = 1;
            isGamePaused = false;
        }

    }


    void ReduceCount()
    {
        zombiesAliveCount--;
    }
    #endregion

}
