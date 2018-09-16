using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Consts
{
    public static class Events
    {

        public enum events
        {          
            startGame,
            lose,
            replay,
            fZhitPlayer,
            sZhitPlayer,
            updateHealth,
            spawnWave,
            reduceZombie,
            addScoreForTheSZ,
            addScoreForTheFZ,
            pause,
            upgradeWeapon
        };

    }

    public static class Values
    {
        public static class Zombie
        {
            public static float fZDamage = 10f;
            public static float sZDamage = 25f;
            public static float attackDistance = 4f;
            public static float fastZombieAttackCooldown = 1f;
            public static float slowZombieAttackCooldown = 2f;
            public static float fastZombieSpeed = 3.5f;
            public static float slowZombieSpeed = 2.5f;
            public static float zombieSpawnDistance = 35f;
            public static int slowZombieSpawnRate = 10;
            public static int scoreCountForSlowZombieKill = 100;
            public static int scoreCountForFastZombieKill = 10;
        }

       public static class Lightning
        {

            public static float minLightIntensivity = 2f;
            public static float maxLightIntensivity = 4f;
        }

        public static class Meds
        {

            public static float medKitCureEffect = 10f;
            public static int medKitDropChance = 10;
        }

        

        public static class Balance
        {
            public static double loweringCoef = 2;
            public static double RisingCoef = 1.5;


        }

        public static class Weapons
        {
            public static float PistolShootingSpeed = 120f;

        }


    }



}
