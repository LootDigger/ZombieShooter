using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DifficultyController
{
    public static bool isCurrentLevelHavePickDifficulty;

    public static double CalculateDifficulty(int currentWave)
    {

        double tmpCurrentWaveDef = currentWave;
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
                double diff = tmpCurrentWaveDef / 3;
                isCurrentLevelHavePickDifficulty = false;
                return diff;
            }
            else if (counter == 1)
            {
                isCurrentLevelHavePickDifficulty = true;
                double diff = tmpCurrentWaveDef / 3;
                diff += 0.5;
                return diff;
            }
        }
        else
        {
            isCurrentLevelHavePickDifficulty = false;
            double diff = tmpCurrentWaveDef / 3;
            return diff;
        }


        return 0;
    }


	


}
