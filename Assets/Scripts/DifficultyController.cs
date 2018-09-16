using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DifficultyController
{
    public static bool isMaxim;

    public static bool isMinim;


    public static int CalculateDifficulty(int currentWave)
    {

        int tmpCurrentWaveDef = currentWave;
        int counter = 0;

        if (tmpCurrentWaveDef % 3 != 0)
        {
            do
            {
                tmpCurrentWaveDef += 1;
                counter++;

            }
            while (tmpCurrentWaveDef % 3 != 0);




            if (counter == 2)
            {
                int diff = tmpCurrentWaveDef / 3;
                isMaxim = false;
                return diff;
            }
            else if (counter == 1)
            {
                isMaxim = true;
                int diff = tmpCurrentWaveDef / 3;
                diff += 1;
                return diff;
            }
        }
        else
        {
            isMaxim = false;
            int diff = tmpCurrentWaveDef / 3;
            return diff;
        }


        return 0;
    }


	


}
