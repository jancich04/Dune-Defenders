using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;
using System.Threading;

public class RoomTimer : MonoBehaviour
{
    public Text time;
    public Image timePanel;
    public bool count;
    public int Time;
    private ExitGames.Client.Photon.Hashtable setTime = new ExitGames.Client.Photon.Hashtable();
    public Manager manager;
    public bool flick;

    // Start is called before the first frame update
    void Start()
    {
        count = true;
    }

    // Update is called once per frame
    void Update()
    {
                
        Time = (int)PhotonNetwork.CurrentRoom.CustomProperties["Time"];
        float minutes = Mathf.FloorToInt((int)PhotonNetwork.CurrentRoom.CustomProperties["Time"] /60);
        float seconds = Mathf.FloorToInt((int)PhotonNetwork.CurrentRoom.CustomProperties["Time"] %60);

        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if(PhotonNetwork.IsMasterClient)
        {
            if(count)
            {
                count = false;
                StartCoroutine(timer());
            }
        }

        if(Time <= 0 &&!flick)
        {
            flick = true;
            StartCoroutine(endGame());
        }

        if(flick)
        {
            manager.scoreboardCanvas = true;
            manager.scoreboardUI.SetActive(true);
            time.gameObject.SetActive(false);
            timePanel.gameObject.SetActive(false);
        }
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(1);
        int nextTime = Time -= 1;
        setTime["Time"] = nextTime;
        PhotonNetwork.CurrentRoom.SetCustomProperties(setTime);
        //PhotonNetwork.CurrentRoom.CustomProperties["Timer"] = nextTime;

        count = true;

    }

    IEnumerator endGame()
    {
        yield return new WaitForSeconds(10);
        PlayerPrefs.SetInt("GO", 1);
        PlayerPrefs.SetFloat("Kills", PlayerPrefs.GetFloat("Kills") + PlayerPrefs.GetFloat("GOKills"));
        PlayerPrefs.SetFloat("Deaths", PlayerPrefs.GetFloat("Deaths") + PlayerPrefs.GetFloat("GODeaths"));
        PlayerPrefs.SetFloat("Points", PlayerPrefs.GetFloat("Points") + PlayerPrefs.GetFloat("GOScore"));
        PlayerPrefs.SetFloat("Experience", PlayerPrefs.GetFloat("Experience") + PlayerPrefs.GetFloat("GOScore") / 10);
        PhotonNetwork.LeaveRoom();
        Application.LoadLevel("Menu");
    }
}
