using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    public int DecayTime;

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<PhotonView>().IsMine)
        {
            StartCoroutine(deleteDelay());
        }
    }

    IEnumerator deleteDelay()
    {
        yield return new WaitForSeconds(DecayTime);
        PhotonNetwork.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
