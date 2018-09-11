using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWavesController : MonoBehaviour
{

    #region Private fields

    private int spawnCount;
   
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
        EventController.Subscribe(Consts.Events.events.spawnWave, SpawnWave);
        spawnCount = 4;
        SpawnWave();
<<<<<<< HEAD
<<<<<<< HEAD
       // Debug.Log("Start");
<<<<<<< HEAD
       
=======
        Debug.Log("Start");
>>>>>>> parent of 953b823... Delete Logs and remove light component from bullet
=======
        Debug.Log("Start");
>>>>>>> parent of 953b823... Delete Logs and remove light component from bullet
=======
>>>>>>> parent of 7aba834... Add zombie pack
    }

    #endregion





    #region private methods

    void SpawnWave()
    {

        int tmpCount = spawnCount / 4;
             
        for(int i=0; i< tmpCount; i++)
        {
            Instantiate(fastZombie, new Vector3(-tmpCount + i * 2, 1f, Player.transform.position.z + Consts.Values.zombieSpawnDistance), Quaternion.identity);           
        }


        for (int i = 0; i < tmpCount; i++)
        {
            Instantiate(fastZombie, new Vector3(-tmpCount + i * 2, 1f, Player.transform.position.z -Consts.Values.zombieSpawnDistance), Quaternion.identity);
        }


        for (int i = 0; i < tmpCount; i++)
        {
            Instantiate(fastZombie, new Vector3(Player.transform.position.x - Consts.Values.zombieSpawnDistance,1f, Player.transform.position.z - tmpCount + i * 2), Quaternion.identity);
        }


        for (int i = 0; i < tmpCount; i++)
        {
            Instantiate(fastZombie, new Vector3(Player.transform.position.x + Consts.Values.zombieSpawnDistance, 1f, Player.transform.position.z - tmpCount + i * 2), Quaternion.identity);
        }

    }
    
    #endregion

}
