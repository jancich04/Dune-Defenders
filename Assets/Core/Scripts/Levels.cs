using UnityEngine;

public class Levels : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float experience = PlayerPrefs.GetFloat("Experience");
        int level = 1;

        // Define the experience thresholds for each level
        int[] experienceThresholds = {
                0,      
                // Level 2 starts at 80 experience
                80,     
                // Level 3 starts at 200 experience
                200,    
                // Level 4 starts at 340 experience
                340,    
                // Level 5 starts at 500 experience
                500,    
                // Level 6 starts at 700 experience
                700,    
                // Level 7 starts at 950 experience
                950,    
                // Level 8 starts at 1250 experience
                1250,   
                // Level 9 starts at 1600 experience
                1600,   
                // Level 10 starts at 2000 experience
                2000,
                // Level 11 starts at 3000 experience
                3000,
                // Level 12 starts at 3600 experience
                3600,
                // Level 13 starts at 4300 experience
                4300,
                // Level 14 starts at 5100 experience
                5100,
                // Level 15 starts at 6000 experience
                6000,
                // Level 16 starts at 7000 experience
                7000,
                // Level 17 starts at 8100 experience
                8100,
                // Level 18 starts at 9300 experience
                9300,
                // Level 19 starts at 10600 experience
                10600,
                // Level 20 starts at 12000 experience
                12000,
                // Level 21 starts at 13500 experience
                13500,
                // Level 22 starts at 15100 experience
                15100,
                // Level 23 starts at 16800 experience
                16800,
                // Level 24 starts at 18600 experience
                18600,
                // Level 25 starts at 20500 experience
                20500,
                // Level 26 starts at 22500 experience
                22500,
                // Level 27 starts at 24600 experience
                24600,
                // Level 28 starts at 26800 experience
                26800,
                // Level 29 starts at 29100 experience
                29100,
                // Level 30 starts at 31500 experience
                31500,
                // Level 31 starts at 34000 experience
                34000,
                // Level 32 starts at 36600 experience
                36600,
                // Level 33 starts at 39300 experience
                39300,
                // Level 34 starts at 42100 experience
                42100,
                // Level 35 starts at 45000 experience
                45000,
                // Level 36 starts at 48000 experience
                48000,
                // Level 37 starts at 51100 experience
                51100,
                // Level 38 starts at 54300 experience
                54300,
                // Level 39 starts at 57600 experience
                57600,
                // Level 40 starts at 61000 experience
                61000,
                // Level 41 starts at 64500 experience
                64500,
                // Level 42 starts at 68100 experience
                68100,
                // Level 43 starts at 71800 experience
                71800,
                // Level 44 starts at 75600 experience
                75600,
                // Level 45 starts at 79500 experience
                79500,
                // Level 46 starts at 83500 experience
                83500,
                // Level 47 starts at 87600 experience
                87600,
                // Level 48 starts at 91800 experience
                91800,
                // Level 49 starts at 96100 experience
                96100,
                // Level 50 starts at 100500 experience
                100500,
                // Level 51 starts at 105000 experience
                105000,
                // Level 52 starts at 109600 experience
                109600,
                // Level 53 starts at 114300 experience
                114300,
                // Level 54 starts at 119100 experience
                119100,
                // Level 55 starts at 124000 experience
                124000,
                // Level 56 starts at 129000 experience
                129000,
                // Level 57 starts at 134100 experience
                134100,
                // Level 58 starts at 139300 experience
                139300,
                // Level 59 starts at 144600 experience
                144600,
                // Level 60 starts at 150000 experience
                150000,
                // Level 61 starts at 155500 experience
                155500,
                // Level 62 starts at 161100 experience
                161100,
                // Level 63 starts at 166800 experience
                166800,
                // Level 64 starts at 172600 experience
                172600,
                // Level 65 starts at 178500 experience
                178500,
                // Level 66 starts at 184500 experience
                184500,
                // Level 67 starts at 190600 experience
                190600,
                // Level 68 starts at 196800 experience
                196800,
                // Level 69 starts at 203100 experience
                203100,
                // Level 70 starts at 209500 experience
                209500,
                // Level 71 starts at 216000 experience
                216000,
                // Level 72 starts at 222600 experience
                222600,
                // Level 73 starts at 229300 experience
                229300,
                // Level 74 starts at 236100 experience
                236100,
                // Level 75 starts at 243000 experience
                243000,
                // Level 76 starts at
                294600,
                // Level 77 starts at 302400 experience
                302400,
                // Level 78 starts at 310300 experience
                310300,
                // Level 79 starts at 318300 experience
                318300,
                // Level 80 starts at 326400 experience
                326400,
                // Level 81 starts at 334600 experience
                334600,
                // Level 82 starts at 342900 experience
                342900,
                // Level 83 starts at 351300 experience
                351300,
                // Level 84 starts at 359800 experience
                359800,
                // Level 85 starts at 368400 experience
                368400,
                // Level 86 starts at 377100 experience
                377100,
                // Level 87 starts at 385900 experience
                385900,
                // Level 88 starts at 394800 experience
                394800,
                // Level 89 starts at 403800 experience
                403800,
                // Level 90 starts at 412900 experience
                412900,
                // Level 91 starts at 422100 experience
                422100,
                // Level 92 starts at 431400 experience
                431400,
                // Level 93 starts at 440800 experience
                440800,
                // Level 94 starts at 450300 experience
                450300,
                // Level 95 starts at 459900 experience
                459900,
                // Level 96 starts at 469600 experience
                469600,
                // Level 97 starts at 479400 experience
                479400,
                // Level 98 starts at 489300 experience
                489300,
                // Level 99 starts at 500000 experience 
                500000


        };

        // Find the level based on the current experience
        for (int i = 1; i <= 10; i++)
        {
            if (experience < experienceThresholds[i])
            {
                level = i;
                break;
            }
        }

        // Set the player's level
        PlayerPrefs.SetFloat("Level", level);

        
    }
}
