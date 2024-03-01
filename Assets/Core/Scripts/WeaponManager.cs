using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class WeaponManager : MonoBehaviour
{
    public GameObject Primary;
    public GameObject Secondary;
    public int currentweapon;
    public TPW tpw;

    [Header("Guns")]
    public GameObject pistol1;
    public GameObject M4;

    
    // Start is called before the first frame update
    void Start()
    {
        currentweapon = 1;
        EquipPrimary();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentweapon != 1)
        {
            EquipPrimary();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && currentweapon != 2)
        {
            EquipSecondary();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(currentweapon == 1)
            {
                EquipSecondary();

            } else if (currentweapon == 2)
            {
                EquipPrimary();
            }
        }
            
    }

    public void EquipPrimary()
    {
        if(Secondary.GetComponent<Gun>().aiming)
        {
            Secondary.GetComponent<Gun>().aiming = false;
            Secondary.GetComponent<Gun>().aimObject.GetComponent<Animation>().Play(Secondary.GetComponent<Gun>().AimReturnAnimName);
            Secondary.GetComponent<Gun>().mainCam.GetComponent<Camera>().fieldOfView = Secondary.GetComponent<Gun>().defaultPOV;
            Secondary.GetComponent<Gun>().weaponCam.GetComponent<Camera>().fieldOfView = Secondary.GetComponent<Gun>().defaultPOV;
        }
        tpw.gameObject.GetComponent<PhotonView>().RPC("PrimaryActive", RpcTarget.AllBuffered);
        Secondary.SetActive(false);
        Primary.SetActive(true);
        currentweapon = 1;
        Primary.GetComponent<Gun>().Draw();
    }

    public void EquipSecondary()
    {
        if(Primary.GetComponent<Gun>().aiming)
        {
            Primary.GetComponent<Gun>().aiming = false;
            Primary.GetComponent<Gun>().aimObject.GetComponent<Animation>().Play(Primary.GetComponent<Gun>().AimReturnAnimName);
            Primary.GetComponent<Gun>().mainCam.GetComponent<Camera>().fieldOfView = Primary.GetComponent<Gun>().defaultPOV;
            Primary.GetComponent<Gun>().weaponCam.GetComponent<Camera>().fieldOfView = Primary.GetComponent<Gun>().defaultPOV;
        }
        tpw.gameObject.GetComponent<PhotonView>().RPC("SecondaryActive", RpcTarget.AllBuffered);
        Secondary.SetActive(true);
        Primary.SetActive(false);
        currentweapon = 2;
        Secondary.GetComponent<Gun>().Draw();
    }


}
