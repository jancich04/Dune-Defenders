using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Helper helper;
    public int Team;
    public GameObject friendlyArrow;
    public PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Team = helper.Team;

        if(PV.IsMine)
        {
            friendlyArrow.SetActive(false);
        }

        if((int)PhotonNetwork.CurrentRoom.CustomProperties["GameMode"] == 1)
        {
            if(Team == (int)PhotonNetwork.LocalPlayer.CustomProperties["TEAM"])
            {
                friendlyArrow.SetActive(true);
            }

        }
        else
        {
            friendlyArrow.SetActive(false);
        }

        if(PV.IsMine)
        {
            friendlyArrow.SetActive(false);
        }
    }
}
