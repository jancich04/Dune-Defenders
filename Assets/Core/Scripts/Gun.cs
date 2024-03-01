using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Character characterScript;
    private ExitGames.Client.Photon.Hashtable updateScore = new ExitGames.Client.Photon.Hashtable();

    [Header("Damage")]
    public float Damage;
    public  float minDamage;
    public float maxDamage;

    [Header("Ammo")]
    public int Ammo;
    public int AmmoPerClip;
    public int AmmoLeft;
    public Text AmmoText;
    public Text AmmoLeftText;

    [Header("Equiupment")]
    public float fireTime;
    public float reloadTime;
    public float drawTime;

    [Header("Bools")]
    public bool canFire;

    [Header("Animations")]
    public string FireAnimName;
    public string ReloadAnimName;
    public string DrawAnimName;
    public GameObject Flash;
    public GameObject muzzleLight;
    public float effectTime;
    public string AimAnimName;
    public string AimReturnAnimName;
    public GameObject aimObject;
    public bool aiming;
    public GameObject weaponCam;
    public GameObject mainCam;
    public int defaultPOV;
    public int aimPOV;
    public bool canAim;
    public float FOV;
    public int deafaultPOV;


    [Header("Audio")]
    public AudioClip FireSound;
    public GameObject Player;

    [Header("Xp")]
    public GameObject i;
    public GameObject damagePrefab;
    public Transform xpSpawn;
    public Transform xpParent;
    public Score score;
    public KillStreaks ks;
    public Manager manager;

    [Header("KillFeed")]
    public KillFeed killFeed;

    [Header("Recoil")]
    public Recoil recoilScript;

    // Start is called before the first frame update
    void Start()
    {
        muzzleLight.SetActive(false);
        Flash.SetActive(false);
        manager = GameObject.FindWithTag("Scripts").GetComponent<Manager>();
        killFeed = GameObject.FindWithTag("Scripts").GetComponent<KillFeed>();
        Draw();
    }

    public void Draw()
    {
        canFire = false;
        gameObject.GetComponent<Animation>().Play(DrawAnimName);
        StartCoroutine(drawDelay());
    }

    IEnumerator drawDelay()
    {
        yield return new WaitForSeconds(drawTime);
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(aiming && canAim && !characterScript.isSprinting)
        {
            if(weaponCam.GetComponent<Camera>().fieldOfView > aimPOV)
            {
                weaponCam.GetComponent<Camera>().fieldOfView -= aimPOV * Time.deltaTime;
            }

            if(mainCam.GetComponent<Camera>().fieldOfView > aimPOV)
            {
                mainCam.GetComponent<Camera>().fieldOfView -= aimPOV * Time.deltaTime;
            }
            //weaponCam.GetComponent<Camera>().fieldOfView = aimPOV;
            //mainCam.GetComponent<Camera>().fieldOfView = aimPOV;
        }
        else if(canAim && !characterScript.isSprinting)
        {
            if(weaponCam.GetComponent<Camera>().fieldOfView < defaultPOV)
            {
                weaponCam.GetComponent<Camera>().fieldOfView += aimPOV * Time.deltaTime;
            }
            
            if(mainCam.GetComponent<Camera>().fieldOfView < defaultPOV && mainCam.GetComponent<Camera>().fieldOfView < deafaultPOV - 1)
            {
                mainCam.GetComponent<Camera>().fieldOfView += aimPOV * Time.deltaTime;
            }
        }

        if(!aiming && Input.GetMouseButton(1) && canAim && !characterScript.isSprinting)
        {
            aimObject.GetComponent<Animation>().Play(AimAnimName);
            aiming = true;
        }

        if(aiming && Input.GetMouseButtonUp(1) && canAim)
        {
            aimObject.GetComponent<Animation>().Play(AimReturnAnimName);
            aiming = false;
        }

        AmmoText.text = Ammo.ToString();
        AmmoLeftText.text = "/ " + AmmoLeft.ToString();

        if(Input.GetMouseButton(0) && canFire && Ammo > 0 && !characterScript.isSprinting)
        {
            Fire();
        }
        else if (Ammo == 0 && AmmoLeft > 0 && canFire && Input.GetMouseButton(0) && AmmoLeft > 0 && Ammo != AmmoPerClip)
        {
            Reload();
        }

        if(Input.GetKey(KeyCode.R) && canFire && AmmoLeft > 0)
        {
            Reload();
        }
    }

    void Fire()
    {
        recoilScript.recoil();
        Damage = Random.Range(minDamage, maxDamage);
        //GetComponent<AudioSource>().PlayOneShot(FireSound);
        Player.GetComponent<PhotonView>().RPC("M4Fire", RpcTarget.All);
        GetComponent<Animation>().Stop(FireAnimName);
        GetComponent<Animation>().Play(FireAnimName);
        Ammo -= 1;
        canFire = false;
        StartCoroutine(fireDelay());
        Flash.SetActive(true);
        muzzleLight.SetActive(true);
        StartCoroutine(effectDelay());

        Ray ray = Camera.main.ViewportPointToRay( new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject.tag == "MAP")
            {
                GameObject bh = PhotonNetwork.Instantiate("Bullethole", hit.point, Quaternion.identity);
                bh.transform.rotation = Quaternion.LookRotation(hit.normal);
                GameObject s = PhotonNetwork.Instantiate("bulletSparks", hit.point, Quaternion.identity);
                s.transform.rotation = Quaternion.LookRotation(hit.normal);
            }
            if(hit.collider.gameObject.tag == "Player")
            {
                GameObject b = PhotonNetwork.Instantiate("blood", hit.point, Quaternion.identity);
                b.transform.rotation = Quaternion.LookRotation(hit.normal);
                //Team DetahMatch
                if((int)PhotonNetwork.CurrentRoom.CustomProperties["GameMode"] == 1)
                {
                    if(hit.collider.gameObject.GetComponent<Health>().dead == false && hit.collider.gameObject.GetComponent<Health>().spawnShield == false && hit.collider.gameObject.GetComponent<Helper>().Team != (int)PhotonNetwork.LocalPlayer.CustomProperties["TEAM"])
                    {
                        if(hit.collider.gameObject.GetComponent<Health>().health <= Damage)
                        {
                            string otherPlayerNickname;
                            otherPlayerNickname = hit.collider.gameObject.GetComponent<Helper>().NICKNAME;
                            killFeed.gameObject.GetComponent<PhotonView>().RPC("PlayerKilled", RpcTarget.All, PhotonNetwork.NickName, otherPlayerNickname);
                            //killFeed.PlayerKilled(PhotonNetwork.NickName, otherPlayerNickname);
                            Damage = hit.collider.gameObject.GetComponent<Health>().health;
                            hit.collider.GetComponent<PhotonView>().RPC("Damage", RpcTarget.All, Damage);
                            i = Instantiate(damagePrefab, xpSpawn.position, xpSpawn.rotation);
                            i.transform.SetParent(xpParent);
                            i.GetComponent<Text>().text = Damage.ToString("0");
                            //score.AddScore(Damage);
                            //manager.score += (float)Damage;
                            ks.KillStreakAdd();
                            manager.kills++;
                            if((int)PhotonNetwork.CurrentRoom.CustomProperties["GameMode"] == 1)
                                    {
                                        if((int)PhotonNetwork.LocalPlayer.CustomProperties["TEAM"] == 0)
                                        {
                                            updateScore["redscore"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["redscore"] + 1;
                                            PhotonNetwork.CurrentRoom.SetCustomProperties(updateScore);
                                        } 
                                        else
                                        {
                                            updateScore["bluescore"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["bluescore"] + 1;
                                            PhotonNetwork.CurrentRoom.SetCustomProperties(updateScore);
                                        }
                                    }
                                }
                                else
                                {
                                    hit.collider.GetComponent<PhotonView>().RPC("Damage", RpcTarget.All, Damage);
                                    i = Instantiate(damagePrefab, xpSpawn.position, xpSpawn.rotation);
                                    i.transform.SetParent(xpParent);
                                    i.GetComponent<Text>().text = Damage.ToString("0");
                                    //score.AddScore(Damage);
                                    //manager.score += (float)Damage;
                                    
                                }
                    }
                } 
                else if((int)PhotonNetwork.CurrentRoom.CustomProperties["GameMode"] == 0)
                { 
                    //Free-For-All
                    if(hit.collider.gameObject.GetComponent<Health>().dead == false && hit.collider.gameObject.GetComponent<Health>().spawnShield == false)
                    {
                        if(hit.collider.gameObject.GetComponent<Health>().health <= Damage)
                        {
                            string otherPlayerNickname;
                            otherPlayerNickname = hit.collider.gameObject.GetComponent<Helper>().NICKNAME;
                            killFeed.gameObject.GetComponent<PhotonView>().RPC("PlayerKilled", RpcTarget.All, PhotonNetwork.NickName, otherPlayerNickname);
                            //killFeed.PlayerKilled(PhotonNetwork.NickName, otherPlayerNickname);
                            Damage = hit.collider.gameObject.GetComponent<Health>().health;
                            hit.collider.GetComponent<PhotonView>().RPC("Damage", RpcTarget.All, Damage);
                            i = Instantiate(damagePrefab, xpSpawn.position, xpSpawn.rotation);
                            i.transform.SetParent(xpParent);
                            i.GetComponent<Text>().text = Damage.ToString("0");
                            //score.AddScore(Damage);
                            //manager.score += (float)Damage;
                            ks.KillStreakAdd();
                            manager.kills++;
                            if((int)PhotonNetwork.CurrentRoom.CustomProperties["GameMode"] == 1)
                                    {
                                        if((int)PhotonNetwork.LocalPlayer.CustomProperties["TEAM"] == 0)
                                        {
                                            updateScore["redscore"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["redscore"] + 1;
                                            PhotonNetwork.CurrentRoom.SetCustomProperties(updateScore);
                                        } 
                                        else
                                        {
                                            updateScore["bluescore"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["bluescore"] + 1;
                                            PhotonNetwork.CurrentRoom.SetCustomProperties(updateScore);
                                        }
                                    }
                                }
                                else
                                {
                                    hit.collider.GetComponent<PhotonView>().RPC("Damage", RpcTarget.All, Damage);
                                    i = Instantiate(damagePrefab, xpSpawn.position, xpSpawn.rotation);
                                    i.transform.SetParent(xpParent);
                                    i.GetComponent<Text>().text = Damage.ToString("0");
                                    //score.AddScore(Damage);
                                    //manager.score += (float)Damage;
                                    
                                }
                    }
                }
            }
        }
    }

    IEnumerator fireDelay()
    {
        yield return new WaitForSeconds(fireTime);
        canFire = true;
    }
    IEnumerator effectDelay()
    {
        yield return new WaitForSeconds(effectTime);
        muzzleLight.SetActive(false);
        Flash.SetActive(false);
    }

    void Reload()
        {
            canFire = false;
            GetComponent<Animation>().Play(ReloadAnimName);
            if(AmmoLeft == 0 && AmmoLeft >= AmmoPerClip)
            {
                StartCoroutine(reloadDelay1());
                return;
            }
            if(AmmoLeft == 0 && AmmoLeft < AmmoPerClip && AmmoLeft >= 1) 
            {
                StartCoroutine(reloadDelay2());
                return;
            }
            StartCoroutine(reloadDelay3());
        }

        IEnumerator reloadDelay1()
        {
            yield return new WaitForSeconds(reloadTime);
            Ammo = AmmoPerClip;
            AmmoLeft -= AmmoPerClip;
            canFire = true;
        }
        IEnumerator reloadDelay2()
        {
            yield return new WaitForSeconds(reloadTime);
            Ammo = AmmoLeft;
            AmmoLeft = 0;
            canFire = true;
        }
        IEnumerator reloadDelay3()
        {
            yield return new WaitForSeconds(reloadTime);
            int ammoint;
            ammoint = AmmoPerClip - Ammo;
            if(AmmoLeft < AmmoPerClip)
            {
                if(AmmoLeft < AmmoPerClip - Ammo)
                {
                    ammoint = AmmoLeft;
                }
            }
            Ammo += ammoint;
            AmmoLeft -= ammoint;
            canFire = true;
        }
}
