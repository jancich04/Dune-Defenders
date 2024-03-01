using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;

public class Menu : MonoBehaviourPunCallbacks
{
    [Header("LoadingText")]
    public Text loadingText;
    public int loadingTextState;
    public bool loadingFlick;

    [Header("MenuStates")]
    public int menuState;

    public GameObject loadingCanvas;
    public GameObject mainCanvas;
    public GameObject createCanvas;
    public GameObject lobbyCanvas;
    public GameObject optionsCanvas;
    public GameObject playerCanvas;
    public GameObject StatsUI;

    [Header("CreateRoom")]
    public InputField createRoomIF;
    public int GameMode;
    public Image DMButton;
    public Image TDMButton;
    public int time;
    public int timeSlected;
    public Button min5;
    public Button min10;
    public Button min20;
    public Button min25;
    public byte maxPlayers;
    public int MAXPlayers;
    public Image max2;
    public Image max6;
    public Image max10;
    public Image max16;

    [Header("Lobby")]
    public Transform roomListHolder;
    public GameObject roomListItemPrtefab;

    [Header("Options")]
    public Text quality;

    [Header("PlayerCanvas")]
    public TMP_InputField IDField;
    public Image playerButton;
    public Image playButton;
    public Image ladoutButton;
    public Image skinButton;
    public TMP_Text nameText;
    public TMP_Text Experience;
    public TMP_Text Level;
    public TMP_Text LevelMainRoom;
    public TMP_Text Kills;
    public TMP_Text Deaths;
    public TMP_Text Points;
    public TMP_Text KDR;
    public TMP_Text playerID;
    public TMP_Text playerIDMainRoom;
    public float kdr;
    

    [Header("GameOverUI")]
    public GameObject GOCanvas;
    public TMP_Text GOKills;
    public TMP_Text GODeaths;
    public TMP_Text GOScore;
    public TMP_Text GOKDR;
    public float GOKDRINT;


    // Start is called before the first frame update
    void Start()
    {
        loadingFlick = true;
        PhotonNetwork.ConnectUsingSettings();
        timeSlected = 1;
        MAXPlayers = 3;
    }

    public override void OnConnectedToMaster()
    {
        if(PlayerPrefs.GetString("Username") == "")
        {
            PhotonNetwork.NickName = "Soldier " + Random.Range(0, 9999);
        } else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("Username");
        }
        
        Debug.Log("Connected to master server");
        PhotonNetwork.JoinLobby(); 
    }

    public void SetID()
    {
        if(IDField.text != "")
        {
            PlayerPrefs.SetString("Username", IDField.text);
            PhotonNetwork.NickName = IDField.text;
        }
    }

    public void PlayerSettings()
    {
        menuState = 5;
    }

    public void Continue ()
    {
        PlayerPrefs.SetInt("GO", 0);
        menuState = 1;
    }

    public override void OnJoinedLobby()
    {
        if(PlayerPrefs.GetInt("GO") == 0)
        {
            menuState = 1;
        } else
        {
            menuState = 6;
        }
        
    }

    public void DM()
    {
        GameMode = 0;
    }

    public void TDM()
    {
        GameMode = 1;
    }

    public void mins5()
    {
        timeSlected = 0;
    }
    public void mins10()
    {
        timeSlected = 1;
    }
    public void mins20()
    {
        timeSlected = 2;
    }
    public void mins25()
    {
        timeSlected = 3;
    }

    public void MAX2()
    {
        MAXPlayers = 0;
    }
    public void MAX6()
    {
        MAXPlayers = 1;
    }
    public void MAX10()
    {
        MAXPlayers = 2;
    }
    public void MAX16()
    {
        MAXPlayers = 3;
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(MAXPlayers == 0)
        {
            max2.color = new Color(0.25f, 0.61f, 0.80f);
            max6.color = new Color(0.376f, 0.376f, 0.376f);
            max10.color = new Color(0.376f, 0.376f, 0.376f);
            max16.color = new Color(0.376f, 0.376f, 0.376f);
            maxPlayers = 2;
        }
        if(MAXPlayers == 1)
        {
            max2.color = new Color(0.376f, 0.376f, 0.376f);
            max6.color = new Color(0.25f, 0.61f, 0.80f);
            max10.color = new Color(0.376f, 0.376f, 0.376f);
            max16.color = new Color(0.376f, 0.376f, 0.376f);
            maxPlayers = 6;
        }
        if(MAXPlayers == 2)
        {
            max2.color =new Color(0.376f, 0.376f, 0.376f);
            max6.color = new Color(0.376f, 0.376f, 0.376f);
            max10.color = new Color(0.25f, 0.61f, 0.80f);
            max16.color = new Color(0.376f, 0.376f, 0.376f);
            maxPlayers = 10;
        }
        if(MAXPlayers == 3)
        {
            max2.color = new Color(0.376f, 0.376f, 0.376f);
            max6.color = new Color(0.376f, 0.376f, 0.376f);
            max10.color = new Color(0.376f, 0.376f, 0.376f);
            max16.color = new Color(0.25f, 0.61f, 0.80f);
            maxPlayers = 16;
        }

        Experience.text = "Xp : " + PlayerPrefs.GetFloat("Experience").ToString();
        //playerID.text = PhotonNetwork.NickName;
        playerIDMainRoom.text = PhotonNetwork.NickName;
        Level.text = "Level " + PlayerPrefs.GetFloat("Level").ToString();
        LevelMainRoom.text = PlayerPrefs.GetFloat("Level").ToString();
        Kills.text = "Kills : " + PlayerPrefs.GetFloat("Kills").ToString();
        Deaths.text = "Deaths : " + PlayerPrefs.GetFloat("Deaths").ToString();
        Points.text = "Points : " + PlayerPrefs.GetFloat("Points").ToString();
        KDR.text = "KDR : " + kdr.ToString("0.0");
        kdr = PlayerPrefs.GetFloat("Kills") / PlayerPrefs.GetFloat("Deaths");

        if(timeSlected == 0)
        {
            min5.image.color = new Color(0.25f, 0.61f, 0.80f);
            min10.image.color = new Color(0.376f, 0.376f, 0.376f);
            min20.image.color = new Color(0.376f, 0.376f, 0.376f);
            min25.image.color = new Color(0.376f, 0.376f, 0.376f);
            time = 300;
        }
        if(timeSlected == 1)
        {
            min5.image.color = new Color(0.376f, 0.376f, 0.376f);
            min10.image.color = new Color(0.25f, 0.61f, 0.80f);
            min20.image.color = new Color(0.376f, 0.376f, 0.376f);
            min25.image.color = new Color(0.376f, 0.376f, 0.376f);
            time = 600;
        }
        if(timeSlected == 2)
        {
            min5.image.color = new Color(0.376f, 0.376f, 0.376f);
            min10.image.color = new Color(0.376f, 0.376f, 0.376f);
            min20.image.color = new Color(0.25f, 0.61f, 0.80f);
            min25.image.color = new Color(0.376f, 0.376f, 0.376f);
            time = 1200;
        }
        if(timeSlected == 3)
        {
            min5.image.color = new Color(0.376f, 0.376f, 0.376f);
            min10.image.color = new Color(0.376f, 0.376f, 0.376f);
            min20.image.color = new Color(0.376f, 0.376f, 0.376f);
            min25.image.color = new Color(0.25f, 0.61f, 0.80f);
            time = 1500;
        }

        GOKills.text = "Kills: " + PlayerPrefs.GetFloat("GOKills").ToString();
        GODeaths.text = "Deaths: " + PlayerPrefs.GetFloat("GODeaths").ToString();
        GOScore.text = "Score: " + PlayerPrefs.GetFloat("GOScore").ToString();

        float adjustedDeaths = Mathf.Max(PlayerPrefs.GetFloat("GODeaths"), 1);
        float goKDR = (float)PlayerPrefs.GetFloat("GOKills") / adjustedDeaths;
        GOKDR.text = "KDR: " + goKDR.ToString("0.0");

        //GOKDRINT = PlayerPrefs.GetInt("GOKills") / PlayerPrefs.GetInt("GODeaths");
        //GOKDR.text = "KDR: " + GOKDRINT.ToString("0.0");

        nameText.text = PhotonNetwork.NickName;

        if(GameMode == 0)
        {
            DMButton.color = new Color(0.25f, 0.61f, 0.80f);
            TDMButton.color = new Color(0.376f, 0.376f, 0.376f);
        }
        if(GameMode == 1)
        {
            DMButton.color = new Color(0.376f, 0.376f, 0.376f);
            TDMButton.color = new Color(0.25f, 0.61f, 0.80f);
        }

        Cursor.lockState = CursorLockMode.None;

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

        if(loadingFlick)
        {
            loadingFlick = false;
            StartCoroutine(loadstateChange());
        }

        if(loadingTextState == 0)
        {
            loadingText.text = "loading";
        }
        if(loadingTextState == 1)
        {
            loadingText.text = "loading.";
        }
        if(loadingTextState == 2)
        {
            loadingText.text = "loading..";
        }
        if(loadingTextState == 3)
        {
            loadingText.text = "loading...";
        }

        if(menuState == 0)
        {
            loadingCanvas.SetActive(true);
            mainCanvas.SetActive(false);
            createCanvas.SetActive(false);
            lobbyCanvas.SetActive(false);
            optionsCanvas.SetActive(false);
            playerCanvas.SetActive(false);
            GOCanvas.SetActive(false);
            StatsUI.SetActive(false);
            playerButton.color = new Color(0.376f, 0.376f, 0.376f);
            playButton.color = new Color(0.376f, 0.376f, 0.376f);
            skinButton.color = new Color(0.376f, 0.376f, 0.376f);
            ladoutButton.color = new Color(0.376f, 0.376f, 0.376f);
        }
        if(menuState == 1)
        {
            loadingCanvas.SetActive(false);
            mainCanvas.SetActive(true);
            createCanvas.SetActive(false);
            lobbyCanvas.SetActive(false);
            optionsCanvas.SetActive(false);
            playerCanvas.SetActive(false);
            GOCanvas.SetActive(false);
            StatsUI.SetActive(true);
            playerButton.color = new Color(0.376f, 0.376f, 0.376f);
            playButton.color = new Color(0.25f, 0.61f, 0.80f);
            skinButton.color = new Color(0.376f, 0.376f, 0.376f);
            ladoutButton.color = new Color(0.376f, 0.376f, 0.376f);
        }
        if(menuState == 2)
        {
            loadingCanvas.SetActive(false);
            mainCanvas.SetActive(false);
            createCanvas.SetActive(true);
            lobbyCanvas.SetActive(false);
            optionsCanvas.SetActive(false);
            GOCanvas.SetActive(false);
            StatsUI.SetActive(true);
            playerButton.color = new Color(0.376f, 0.376f, 0.376f);
            playButton.color = new Color(0.25f, 0.61f, 0.80f);
            skinButton.color = new Color(0.376f, 0.376f, 0.376f);
            ladoutButton.color = new Color(0.376f, 0.376f, 0.376f);
        }
        if(menuState == 3)
        {
            loadingCanvas.SetActive(false);
            mainCanvas.SetActive(false);
            createCanvas.SetActive(false);
            lobbyCanvas.SetActive(true);
            optionsCanvas.SetActive(false);
            playerCanvas.SetActive(false);
            GOCanvas.SetActive(false);
            StatsUI.SetActive(false);
            playerButton.color = new Color(0.376f, 0.376f, 0.376f);
            playButton.color = new Color(0.25f, 0.61f, 0.80f);
            skinButton.color = new Color(0.376f, 0.376f, 0.376f);
            ladoutButton.color = new Color(0.376f, 0.376f, 0.376f);
        }
        if(menuState == 4)
        {
            loadingCanvas.SetActive(false);
            mainCanvas.SetActive(false);
            createCanvas.SetActive(false);
            lobbyCanvas.SetActive(false);
            optionsCanvas.SetActive(true);
            playerCanvas.SetActive(false);
            GOCanvas.SetActive(false);
            StatsUI.SetActive(true);
            playerButton.color = new Color(0.376f, 0.376f, 0.376f);
            playButton.color = new Color(0.25f, 0.61f, 0.80f);
            skinButton.color = new Color(0.376f, 0.376f, 0.376f);
            ladoutButton.color = new Color(0.376f, 0.376f, 0.376f);
        }
        if(menuState == 5)
        {
            loadingCanvas.SetActive(false);
            mainCanvas.SetActive(false);
            createCanvas.SetActive(false);
            lobbyCanvas.SetActive(false);
            optionsCanvas.SetActive(false);
            playerCanvas.SetActive(true);
            GOCanvas.SetActive(false);
            StatsUI.SetActive(false);
            playerButton.color = new Color(0.25f, 0.61f, 0.80f);
            playButton.color = new Color(0.376f, 0.376f, 0.376f);
            skinButton.color = new Color(0.376f, 0.376f, 0.376f);
            ladoutButton.color = new Color(0.376f, 0.376f, 0.376f);
        }
        if(menuState == 6)
        {
            loadingCanvas.SetActive(false);
            mainCanvas.SetActive(false);
            createCanvas.SetActive(false);
            lobbyCanvas.SetActive(false);
            optionsCanvas.SetActive(false);
            playerCanvas.SetActive(false);
            GOCanvas.SetActive(true);
            StatsUI.SetActive(false);
            playerButton.color = new Color(0.25f, 0.61f, 0.80f);
            playButton.color = new Color(0.376f, 0.376f, 0.376f);
            skinButton.color = new Color(0.376f, 0.376f, 0.376f);
            ladoutButton.color = new Color(0.376f, 0.376f, 0.376f);
        }
    }

    IEnumerator loadstateChange()
    {
        yield return new WaitForSeconds( 0.25f);
        loadingFlick = true;
        if(loadingTextState < 3)
        {
            loadingTextState++;
        } else
        {
            loadingTextState = 0;
        }
    }

    public void createroom()
    {
        menuState = 2;
    }

    public void lobby()
    {
        menuState = 3;
    }

    public void mainRoom()
    {
        menuState = 1;
    }

    public void options()
    {
        menuState = 4;
    }

    public void Back()
    {
        menuState = 1;
    }

    public void QuickStart()
    {
        PhotonNetwork.JoinRandomRoom();
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();
        Hashtable options = new Hashtable();
        string[] lobbyProps = {"GT", "MP"};
        roomOptions.CustomRoomPropertiesForLobby = lobbyProps;
        options.Add("MP", 16);
        options.Add("GT", 0);
        options.Add("Time", 600);
        options.Add("redscore", 0);
        options.Add("bluescore", 0);
        options.Add("GameMode", 0);
        roomOptions.CustomRoomProperties = options;
        roomOptions.MaxPlayers = 16;
        PhotonNetwork.CreateRoom("Bagdad " + Random.Range(0, 9999), roomOptions);
    }

    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(createRoomIF.text))
        {
            return;
        }
        RoomOptions roomOptions = new RoomOptions();
        Hashtable options = new Hashtable();
        string[] lobbyProps = {"GT", "MP"};
        roomOptions.CustomRoomPropertiesForLobby = lobbyProps;
        options.Add("MP", maxPlayers);
        options.Add("GT", GameMode);
        options.Add("Time", time);
        options.Add("redscore", 0);
        options.Add("bluescore", 0);
        options.Add("GameMode", GameMode);
        roomOptions.CustomRoomProperties = options;
        roomOptions.MaxPlayers = maxPlayers;

        PhotonNetwork.CreateRoom(createRoomIF.text, roomOptions);
        menuState = 0;
        loadingTextState = 0;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Development"); 
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListHolder)
        {
            Destroy(trans.gameObject);
        }

        for (int i =0; i < roomList.Count; i++)
        {
            if(roomList[i].RemovedFromList)
            {
                continue;
            }
            Instantiate(roomListItemPrtefab, roomListHolder).GetComponent<RoomListItem>().configure(roomList[i]);
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
