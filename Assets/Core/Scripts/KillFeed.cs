using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KillFeed : MonoBehaviour
{
    public Transform killFeedArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void PlayerKilled(string killer, string killed)
    {
        GameObject prefab = PhotonNetwork.Instantiate("KillFeedPrefab", killFeedArea.position, killFeedArea.rotation);
        prefab.transform.SetParent(killFeedArea);
        prefab.transform.SetAsFirstSibling();
        prefab.GetComponent<PhotonView>().RPC("UpdateNames", RpcTarget.All, killer, killed);
    }
}
