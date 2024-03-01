using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Health : MonoBehaviour
{
    public Text healthText;
    public Slider healthSlider;
    public float health;
    public float HealSpeed;
    public float maxHealth;
    public bool heal;
    public PhotonView PV;
    public Manager manager;
    public bool dead;
    public bool spawnShield;
    public float spawnShieldTime;
    public float SST;
    public GameObject SSUI;
    public GameObject SSUI2;
    public GameObject damageUI;
    public float UITime;

    // Start is called before the first frame update
    void Start()
    {
       manager = GameObject.FindWithTag("Scripts").GetComponent<Manager>(); 
       spawnShield = true;
       SST = spawnShieldTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey && spawnShield)
        {
            PV.RPC("spawnShieldOff", RpcTarget.All);
        }
        if(spawnShield)
        {
            health = maxHealth;
            SST -= 1 * Time.deltaTime;
            SSUI.SetActive(true);
            SSUI2.SetActive(true);
            SSUI2.GetComponent<Text>().text = SST.ToString("0.0");
        }
        else
        {
            SSUI.SetActive(false);
            SSUI2.SetActive(false);
        }

        if(SST <= 0)
        {
            PV.RPC("spawnShieldOff", RpcTarget.All);
        }

        healthText.text = healthSlider.value.ToString("0");
        healthSlider.value = health;
        healthSlider.maxValue = maxHealth;

        if(health > maxHealth)
        {
            health = maxHealth;
        }

        if(health < maxHealth && health > 0)
        {
            health += HealSpeed * Time.deltaTime;
            heal = true;
        }
    }

    [PunRPC]
    public void Damage (float damage)
    {
        damageUI.SetActive(true);
        StartCoroutine(damageUIDelay());

        health -= damage;
        if(PV.IsMine)
        {
            if(health <= 0)
            {
                Die();
            }
        } 

        if(health <= 0)
            {
                dead = true;
            }
    }

    IEnumerator damageUIDelay()
    {
        yield return new WaitForSeconds(UITime);
        damageUI.SetActive(false);
    }

    [PunRPC]
    public void Die()
    {
        manager.deaths++;
        manager.cooldown();
        manager.Alive = false;
        PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    public void spawnShieldOff()
    {
        spawnShield = false;
    }

}
