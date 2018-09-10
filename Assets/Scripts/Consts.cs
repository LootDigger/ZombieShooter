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
            hitPlayer,
            updateHealth
        };

    }

    public static class Values
    {
        public static float damage = 10f;
        public static float attackDistance = 2f;
        public static float zombieAttackCooldown = 2f;
        public static float fastZombieSpeed = 3.5f;
        public static float slowZombieSpeed = 1f;

    }



}
