using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Manager : MonoBehaviourPunCallbacks
{
    private ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    private ExitGames.Client.Photon.Hashtable setTime = new ExitGames.Client.Photon.Hashtable();
    private ExitGames.Client.Photon.Hashtable setTeam = new ExitGames.Client.Photon.Hashtable();
    public int UIState;
    
    public bool Alive;

    [Header("Spawn")]
    public GameObject SpawnUI;
    public Transform[] SpawnPoints;
    public Transform[] REDSpawnPoints;
    public Transform[] BlUESpawnPoints;
    public float VariableCoolDownTime;
    public float coolDownTime;
    public bool Cooldown;
    public Slider cooldownSlider;
    public GameObject cooldownUI;
    public Text respawnText;
    public string respawnTextMessage;
    public bool sucideFlick;

    [Header("Scoreboard")]
    public bool scoreboardCanvas;
    public GameObject scoreboardUI;
    public float score;
    public float kills;
    public float deaths;
    public float kdr;
    public Text tdmRoomName;


    [Header("Escape")]
    public GameObject escapeUI;
    public GameObject Player;
    public GameObject optionsUI;
    public bool options;
    public Text quality;

    [Header("Timer")]
    public bool endgameflick;

    [Header("Free-For-All")]
    public TMP_Text highestKillsUI;
    public TMP_Text myKillsUI;
    public int highestKills;
    public int KILLS;
    public GameObject[] DMUI;

    [Header("TDM")]
    public GameObject TDMScoreboardCanvas;
    public GameObject[] TDMUI;
    public int Team;
    public TMP_Text redscore;
    public TMP_Text bluescore;
    


    // Start is called before the first frame update
    void Start()
    {
        Team = Random.Range(0,100);
        UIState = 1;
        print(PhotonNetwork.CurrentRoom.CustomProperties["GameMode"]);
        if((int)PhotonNetwork.CurrentRoom.CustomProperties["GameMode"] == 1)
        {
            scoreboardUI = TDMScoreboardCanvas;
        }
    }

    public void cooldown()
    {
        Cooldown = true;
        coolDownTime = VariableCoolDownTime;
    }

    [PunRPC]
    public void UpdateHighestKills(int KILLS2)
    {
        highestKills = KILLS2;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        highestKills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        tdmRoomName.text = PhotonNetwork.CurrentRoom.Name;

        redscore.text = PhotonNetwork.CurrentRoom.CustomProperties["redscore"].ToString();
        bluescore.text = PhotonNetwork.CurrentRoom.CustomProperties["bluescore"].ToString();

        if((int)PhotonNetwork.CurrentRoom.CustomProperties["GameMode"] == 0)
        {
            foreach(GameObject dm in DMUI)
            {
                dm.SetActive(true); 
            }
            foreach(GameObject tdm in TDMUI)
            {
                tdm.SetActive(false); 
            }
        }
        if((int)PhotonNetwork.CurrentRoom.CustomProperties["GameMode"] == 1)
        {
            foreach(GameObject dm in DMUI)
            {
                dm.SetActive(false); 
            }
            foreach(GameObject tdm in TDMUI)
            {
                tdm.SetActive(true); 
            }
        }

        if(highestKills == 20)
        {
           setTime["Time"] = 0;
           PhotonNetwork.CurrentRoom.SetCustomProperties(setTime); 
        }

        KILLS = (int)kills;
        myKillsUI.text = kills.ToString();
        highestKillsUI.text = highestKills.ToString();

        if(kills > highestKills)
        {
            GetComponent<PhotonView>().RPC("UpdateHighestKills", RpcTarget.AllBuffered, KILLS);
        }
        
        int qualityLevel = QualitySettings.GetQualityLevel();

        if(qualityLevel == 0)
        {
            quality.text = "Very Low";
        }
        if(qualityLevel == 1)
        {
            quality.text = "Low";
        }
        if(qualityLevel == 2)
        {
            quality.text = "Medium";
        }
        if(qualityLevel == 3)
        {
            quality.text = "High";
        }
        if(qualityLevel == 4)
        {
            quality.text = "Very High";
        }
        if(qualityLevel == 5)
        {
            quality.text = "Ultra";
        }

        if(Input.GetKey(KeyCode.Tab))
        {
            scoreboardCanvas = true;
        }
        else
        {
            scoreboardCanvas = false;
        }

        if(scoreboardCanvas && !endgameflick)
        {
            scoreboardUI.SetActive(true);
        }
        else if(!endgameflick)
        {
            scoreboardUI.SetActive(false);
        }

        respawnText.text = respawnTextMessage + coolDownTime.ToString("0.0");

        cooldownSlider.maxValue = VariableCoolDownTime;
        cooldownSlider.value = coolDownTime;

        if(Cooldown)
        {
            coolDownTime -= 1 * Time.deltaTime;
            UIState = 2;
        }

        if(coolDownTime <= 0)
        {
            Cooldown = false;
        }

        if(!Alive && Cooldown == false && !options)
        {
            UIState = 1;
        }

        if(UIState == 0)
        {
            SpawnUI.SetActive(false);
            cooldownUI.SetActive(false);
            escapeUI.SetActive(false);
            optionsUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        if(UIState == 1)
        {
            SpawnUI.SetActive(true);
            cooldownUI.SetActive(false);
            escapeUI.SetActive(false);
            optionsUI.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }

        if(UIState == 2)
        {
            cooldownUI.SetActive(true);
            SpawnUI.SetActive(false);
            escapeUI.SetActive(false);
            optionsUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        if(UIState == 3)
        {
            SpawnUI.SetActive(false);
            cooldownUI.SetActive(false);
            escapeUI.SetActive(true);
            optionsUI.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
        if(UIState == 4)
        {
            SpawnUI.SetActive(false);
            cooldownUI.SetActive(false);
            escapeUI.SetActive(false);
            optionsUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        if(PhotonNetwork.InRoom)
        {
            playerProperties["score"] = score;
            playerProperties["kills"] = kills;
            playerProperties["deaths"] = deaths;
            playerProperties["kdr"] = kdr;
            PlayerPrefs.SetFloat("GOKills", kills);
            PlayerPrefs.SetFloat("GODeaths", deaths);
            PlayerPrefs.SetFloat("GOScore", score);
            
            
            if(Team < 50)
            {
                playerProperties["TEAM"] = 0;
            } else
            {
                playerProperties["TEAM"] = 1;
            }
            

            PhotonNetwork.LocalPlayer.CustomProperties = playerProperties;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }

        if(Alive && Input.GetKeyDown(KeyCode.Escape) && UIState != 3)
        {
            UIState = 3;
            return;
        }

        if(Alive && Input.GetKeyDown(KeyCode.Escape) && UIState == 3)
        {
            UIState = 0;
            return;
        }

        if(coolDownTime <= 0 && !Alive && sucideFlick)
        {
            UIState = 1;
            sucideFlick = false;
        }
    }

    public void Spawn()
    {
        if((int)PhotonNetwork.CurrentRoom.CustomProperties["GameMode"] == 0)
        {
            int spawn = Random.Range(0, SpawnPoints.Length);
            Player = PhotonNetwork.Instantiate("Player", SpawnPoints[spawn].position, Quaternion.identity);
        }
        else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["GameMode"] == 1)
        {
            if((int)PhotonNetwork.LocalPlayer.CustomProperties["TEAM"] == 0)
            {
                int redspawn = Random.Range(0, REDSpawnPoints.Length);
                Player = PhotonNetwork.Instantiate("REDPlayer", REDSpawnPoints[redspawn].position, Quaternion.identity);
            } 
            if((int)PhotonNetwork.LocalPlayer.CustomProperties["TEAM"] == 1)
            {
                int bluespawn = Random.Range(0, BlUESpawnPoints.Length);
                Player = PhotonNetwork.Instantiate("BLUEPlayer", BlUESpawnPoints[bluespawn].position, Quaternion.identity);
            } 
        }
        UIState = 0;
        Alive = true;
    }

    public void LeaveRoom()
    {
        PlayerPrefs.SetInt("GO", 1);
        PlayerPrefs.SetFloat("Kills", PlayerPrefs.GetFloat("Kills") + PlayerPrefs.GetFloat("GOKills"));
        PlayerPrefs.SetFloat("Deaths", PlayerPrefs.GetFloat("Deaths") + PlayerPrefs.GetFloat("GODeaths"));
        PlayerPrefs.SetFloat("Points", PlayerPrefs.GetFloat("Points") + PlayerPrefs.GetFloat("GOScore"));
        PlayerPrefs.SetFloat("Experience", PlayerPrefs.GetFloat("Experience") + PlayerPrefs.GetFloat("GOScore") / 10);
        PhotonNetwork.LeaveRoom();
        Application.LoadLevel("Menu");
    }

    public void Suicide ()
    {
        PhotonNetwork.Destroy(Player);
        UIState = 2;
        Cooldown = true;
        coolDownTime = VariableCoolDownTime;
        Alive = false;
        sucideFlick = true;
    }

    public void Resume()
    {
        UIState = 0;
    }

    public void Options()
    {
        options = true;
        UIState = 4;
    }

    public void Back()
    {
        if(Alive)
        {
            UIState = 3;
        }
        else
        {
            UIState = 1;
        }
    }

    public void IncreaseGraphics ()
    {
        QualitySettings.IncreaseLevel(true);
    }
    public void DecreaseGraphics ()
    {
        QualitySettings.DecreaseLevel(true);
    }
}
