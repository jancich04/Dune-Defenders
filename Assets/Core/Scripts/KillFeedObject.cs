using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;


public class KillFeedObject : MonoBehaviour
{
    public Text Killer;
    public Text Killed;
    public int Lifetime;

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<PhotonView>().IsMine)
        {
            StartCoroutine(destroy());
        }
    }

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(Lifetime);
        if(GetComponent<PhotonView>().IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void UpdateNames(string killer, string killed)
    {
        Killer.text = killer;
        Killed.text = killed;
    }
}
