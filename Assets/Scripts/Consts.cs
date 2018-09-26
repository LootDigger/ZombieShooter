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
            upgradeWeapon,
            updateWaveUI,
            flashLightTurned,
            fillFlashLight,
            spawnLoot
        };

    }

    public static class Values
    {
        public static class Zombie
        {
            public static float fZDamage = 10f;
            public static float sZDamage = 25f;
            public static float attackDistance = 4.5f;
            public static float fastZombieAttackCooldown = 1f;
            public static float slowZombieAttackCooldown = 1f;
            public static float fastZombieSpeed = 4.5f;
            public static float slowZombieSpeed = 2.5f;
            public static float zombieSpawnDistance = 50f;
            public static int slowZombieSpawnRate = 10;
            public static int scoreCountForSlowZombieKill = 100;
            public static int scoreCountForFastZombieKill = 10;
            public static float slowZombieMaxhealth = 100f;
            public static float fastZombieMaxhealth = 30f;

        }

        public static class FlashLight
        {
            public static float flashLightlifeCycle = 15f;
            public static float batteryPower = 15f;
            public static int batterySpawnChanse = 30;


        }

        public static class Lightning
        {

            public static float minLightIntensivity = 2f;
            public static float maxLightIntensivity = 4f;
        }

        public static class Meds
        {

            public static float medKitCureEffect = 20f;
            public static int medKitDropChance = 20;
        }

        

        public static class Balance
        {
            public static double loweringCoef = 2;
            public static double RisingCoef = 1.5;


        }

        public static class Weapons
        {
            public static float PistolShootingSpeed = 120f;
            public static float minimumPistolShootingSpeed = 120f;
        }

        public static class Player
        {
            public static Vector3 startPos = new Vector3(15,1.5f,43f);
            public static float playermaxHealth = 10000f;
            public static float speed = 0.075f;


        }


    }



}
