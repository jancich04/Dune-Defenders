using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Collectable : MonoBehaviour
{
    public int bonus;
    public bool collectable;

    // Start is called before the first frame update
    void Start()
    {
        collectable = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,1,0, Space.Self);
    }

    [PunRPC]
    public void delete ()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        
    }
}
