using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    public RoomInfo info;
    public Text playerCount;
    public Text roomID;
    public Text gamemode;
    public TMP_Text mapName;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void configure (RoomInfo myRoomInfo)
    {
        info = myRoomInfo;
        roomID.text = info.Name;
        if((int)info.CustomProperties["GT"] == 0)
        {
            gamemode.text = "Free For All";
        } else
        {
            gamemode.text = "Team Deathmatch";
        }

        /*if((int)info.CustomProperties["M"] == 1)
        {
            mapName.text = "Dessert";
        } else if((int)info.CustomProperties["M"] == 2)
        {
            mapName.text = "City";
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        playerCount.text = info.PlayerCount.ToString() + " / " + (string)info.CustomProperties["MP"].ToString();
    }

    public void Join()
    {
        PhotonNetwork.JoinRoom(info.Name);
    }
}
