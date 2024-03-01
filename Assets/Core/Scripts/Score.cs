using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public float score;
    public Text ScoreUI;
    public Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindWithTag("Scripts").GetComponent<Manager>();
        score = manager.score;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreUI.text = score.ToString("0");
    }

    public void AddScore(float scoreBonus)
    {
        score += scoreBonus;
    }
}
