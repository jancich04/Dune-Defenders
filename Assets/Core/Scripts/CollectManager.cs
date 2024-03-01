using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    public GameObject[] collectable;
    public int collectToSpawn;
    public Transform[]CollectableSpawnpoints;
    public int spawnpoint;
    public bool spawn;
    public int SpawnTime;
    public GameObject a;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawn && PhotonNetwork.IsMasterClient)
        {
            SPAWN();
            spawn = true;
            StartCoroutine(spawndelay());
        }
    }

    IEnumerator spawndelay()
    {
        yield return new WaitForSeconds(SpawnTime);
        spawn = false;
    }

    public void SPAWN()
    {
        if(a != null)
        {
            PhotonNetwork.Destroy(a);
        }
        collectToSpawn = Random.Range(0, collectable.Length);
        spawnpoint = Random.Range(0, CollectableSpawnpoints.Length);
        if(collectToSpawn == 0)
        {
            a = PhotonNetwork.Instantiate("+1000", CollectableSpawnpoints[spawnpoint].position, Quaternion.identity);
        }
        if(collectToSpawn == 1)
        {
            a = PhotonNetwork.Instantiate("+2000", CollectableSpawnpoints[spawnpoint].position, Quaternion.identity);
        }
        if(collectToSpawn == 2)
        {
            a = PhotonNetwork.Instantiate("+3000", CollectableSpawnpoints[spawnpoint].position, Quaternion.identity);
        }
    }
}
