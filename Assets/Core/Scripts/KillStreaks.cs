using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillStreaks : MonoBehaviour
{
    public GameObject EnemyKilledUI;
    public GameObject DoubleKillUI;
    public GameObject ThripleKillUI;
    public GameObject QuadKillUI;
    public GameObject PentaKillUI;
    public GameObject HexaKillUI;
    public GameObject KillingSpreeUI;
    public int killStreak;
    public float killStreakTime;
    public bool addPoints;
    public Score score;
    public Manager manager;
    

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindWithTag("Scripts").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        killStreakTime -= 1 * Time.deltaTime; 
        if(killStreakTime < 0)
        {
            killStreakTime = 0;
        }

        if(killStreakTime <= 0)
        {
            killStreak = 0; 
        }

        if(killStreak == 1)
        {
            EnemyKilledUI.SetActive(true);
            DoubleKillUI.SetActive(false);
            ThripleKillUI.SetActive(false);
            QuadKillUI.SetActive(false);
            PentaKillUI.SetActive(false);
            HexaKillUI.SetActive(false);
            KillingSpreeUI.SetActive(false);

            if(addPoints)
            {
                score.AddScore(100);
                manager.score += 100;
            }
            addPoints = false;
        }
        if(killStreak == 2)
        {
            EnemyKilledUI.SetActive(false);
            DoubleKillUI.SetActive(true);
            ThripleKillUI.SetActive(false);
            QuadKillUI.SetActive(false);
            PentaKillUI.SetActive(false);
            HexaKillUI.SetActive(false);
            KillingSpreeUI.SetActive(false);

            if(addPoints)
            {
                score.AddScore(200);
                manager.score += 200;
            }
            addPoints = false;
        }
        if(killStreak == 3)
        {
            EnemyKilledUI.SetActive(false);
            DoubleKillUI.SetActive(false);
            ThripleKillUI.SetActive(true);
            QuadKillUI.SetActive(false);
            PentaKillUI.SetActive(false);
            HexaKillUI.SetActive(false);
            KillingSpreeUI.SetActive(false);

            if(addPoints)
            {
                score.AddScore(300);
                manager.score += 300;
            }
            addPoints = false;
        }
        if(killStreak == 4)
        {
            EnemyKilledUI.SetActive(false);
            DoubleKillUI.SetActive(false);
            ThripleKillUI.SetActive(false);
            QuadKillUI.SetActive(true);
            PentaKillUI.SetActive(false);
            HexaKillUI.SetActive(false);
            KillingSpreeUI.SetActive(false);

            if(addPoints)
            {
                score.AddScore(400);
                manager.score += 400;
            }
            addPoints = false;
        }
        if(killStreak == 5)
        {
            EnemyKilledUI.SetActive(false);
            DoubleKillUI.SetActive(false);
            ThripleKillUI.SetActive(false);
            QuadKillUI.SetActive(false);
            PentaKillUI.SetActive(true);
            HexaKillUI.SetActive(false);
            KillingSpreeUI.SetActive(false);

            if(addPoints)
            {
                score.AddScore(500);
                manager.score += 500;
            }
            addPoints = false;
        }
        if(killStreak == 6)
        {
            EnemyKilledUI.SetActive(false);
            DoubleKillUI.SetActive(false);
            ThripleKillUI.SetActive(false);
            QuadKillUI.SetActive(false);
            PentaKillUI.SetActive(false);
            HexaKillUI.SetActive(true);
            KillingSpreeUI.SetActive(false);

            if(addPoints)
            {
                score.AddScore(600);
                manager.score += 600;
            }
            addPoints = false;
        }
        if(killStreak > 6)
        {
            EnemyKilledUI.SetActive(false);
            DoubleKillUI.SetActive(false);
            ThripleKillUI.SetActive(false);
            QuadKillUI.SetActive(false);
            PentaKillUI.SetActive(false);
            HexaKillUI.SetActive(false);
            KillingSpreeUI.SetActive(true);

            if(addPoints)
            {
                score.AddScore(750);
                manager.score += 750;
            }
            addPoints = false;
        }
        
        if(killStreak == 0)
        {
            EnemyKilledUI.SetActive(false);
            DoubleKillUI.SetActive(false);
            ThripleKillUI.SetActive(false);
            QuadKillUI.SetActive(false);
            PentaKillUI.SetActive(false);
            HexaKillUI.SetActive(false);
            KillingSpreeUI.SetActive(false);

            addPoints = false;
        }
    }

    public void KillStreakAdd()
    {
        killStreak ++;  
        killStreakTime = 5.0f; 
        addPoints = true;
    }

    
}
