using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Sparks : MonoBehaviour
{
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<PhotonView>().IsMine)
        {
            StartCoroutine(deletDelay());
        }
    }

    IEnumerator deletDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        PhotonNetwork.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
