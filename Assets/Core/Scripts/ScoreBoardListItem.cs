using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class ScoreBoardListItem : MonoBehaviour
{
    public Text ID;
    public Text score;
    public Text kills;
    public Text deaths;
    public Text kdr;

    public Player playa;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       float scoreREF = (float)playa.CustomProperties["score"];
       score.text = Mathf.FloorToInt(scoreREF).ToString();
       float killsREF = (float)playa.CustomProperties["kills"];
       kills.text = killsREF.ToString();
       float deathsREF = (float)playa.CustomProperties["deaths"];
       deaths.text = deathsREF.ToString();
       float kdrREF = (float)playa.CustomProperties["kdr"];
       kdr.text = kdrREF.ToString("0.0");

       if(playa.IsLocal)
       {
            ID.color = Color.yellow;
            score.color = Color.yellow;
            kills.color = Color.yellow;
            deaths.color = Color.yellow;
       }
    }

    public void Connect(Player player)
    {
        ID.text = player.NickName;
        playa = player;
    }
}
