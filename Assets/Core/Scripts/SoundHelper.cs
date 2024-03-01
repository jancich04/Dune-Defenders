using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SoundHelper : MonoBehaviour
{
    public GameObject firesoundobject;
    public AudioClip FireSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void M4Fire()
    {
        firesoundobject.GetComponent<AudioSource>().PlayOneShot(FireSound);
    }
}
