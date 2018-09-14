using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWavesController : MonoBehaviour
{

    #region Private fields

    private int spawnCount;
    private int zombiesAliveCount;
    private int currentWaveNumber;
    private double difficulty;
    private bool isGameStarted;
    private bool isGamePaused;
   
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
        EventController.Subscribe(Consts.Events.events.pause, PauseGame);
        EventController.Subscribe(Consts.Events.events.spawnWave, SpawnWave);
        EventController.Subscribe(Consts.Events.events.reduceZombie, ReduceCount);
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
        if (!isGameStarted)
        {
            EventController.InvokeEvent(Consts.Events.events.startGame);
            isGameStarted = true;
        }

        currentWaveNumber += 1;
        difficulty = DifficultyController.CalculateDifficulty(currentWaveNumber);
        spawnCount = (int)(difficulty * currentWaveNumber * 2);
        zombiesAliveCount = spawnCount;
        int tmpCount = spawnCount;
        if (tmpCount < 1)
        {
            tmpCount = 1;
        }
        zombiesAliveCount = tmpCount * 4;

        Debug.ClearDeveloperConsole();
        Debug.Log("текущая волна = " + currentWaveNumber);
        Debug.Log("Сложность = " + difficulty);
        Debug.Log("количество заспавненых зомби = " + zombiesAliveCount);
        Debug.Log("spawnCount = " + spawnCount);
        Debug.Log("tmpCount = " + tmpCount);



        for (int i = 0; i < tmpCount; i++)
        {
            Vector3 spawnPos = new Vector3(-tmpCount + i * 2, 1f, Player.transform.position.z + Consts.Values.zombieSpawnDistance);
            if (i % 2 == 0)
            {
                spawnPos.z *= 2f;
            }
            int tmp = Random.Range(1, Consts.Values.slowZombieSpawnRate + 1);


            if (tmp == Consts.Values.slowZombieSpawnRate)
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
            Vector3 spawnPos = new Vector3(-tmpCount + i * 2, 1f, Player.transform.position.z - Consts.Values.zombieSpawnDistance);
            if (i % 2 == 0)
            {
                spawnPos.z *= 2f;
            }

            int tmp = Random.Range(1, Consts.Values.slowZombieSpawnRate + 1);


            if (tmp == Consts.Values.slowZombieSpawnRate)
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
            Vector3 spawnPos = new Vector3(Player.transform.position.x - Consts.Values.zombieSpawnDistance, 1f, Player.transform.position.z - tmpCount + i * 2);
            if (i % 2 == 0)
            {
                spawnPos.x *= 2f;
            }

            int tmp = Random.Range(1, Consts.Values.slowZombieSpawnRate + 1);
            if (tmp == Consts.Values.slowZombieSpawnRate)
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
            Vector3 spawnPos = new Vector3(Player.transform.position.x + Consts.Values.zombieSpawnDistance, 1f, Player.transform.position.z - tmpCount + i * 2);
            if (i % 2 == 0)
            {
                spawnPos.x *= 2f;
            }

            int tmp = Random.Range(1, Consts.Values.slowZombieSpawnRate + 1);

            if (tmp == Consts.Values.slowZombieSpawnRate)
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
