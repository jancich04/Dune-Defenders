using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public CharacterController controller;
    public WeaponManager weaponManager;

    [Header("Movement")]
    public float speed;
    public float sprintSpeed;
    public float aimSpeed;
    public float gravity;
    public float jumpHeigth;
    public Vector3 velocity;
    public Transform groundChecker;
    public float groundedDistance;
    public LayerMask groundMask;
    public bool isGrounded;
    public bool isSprinting;
    public bool isWalking;
    public bool LandFlick;


    [Header("Stamina")]
    public float Stamina;
    public float StaminaDecay;
    public float StaminaHeal;
    public float maxStamina;
    public bool emptyStamina;

    [Header("SprintUI")]
    public Slider sprintSlider;
    public Camera mainCam;
    public int normalPOV;
    public int sprintPOV;
    public float povmultiplier;

    [Header("Animations")]
    public string walkAnim;
    public string sprintAnim;
    public string sprintReturnAnim;
    public string sprintAnim2;
    public string jumpAnim;
    public string landAnim;
    public string shakeAnim;
    public GameObject SprintAnimObject;
    public GameObject SprintAnim2Object;
    public GameObject JumpAnimObject;
    public GameObject WalkAnimObject;
    public GameObject LandAnimObject;
    public GameObject CameraShakeAnimObject;
    public bool aimed;
    public GameObject crosshair;
    

    [Header("Audio")]
    public GameObject sprintSoundObj;

    [Header("Photon")]
    public PhotonView PV;

    [Header("Collect")]
    public Helper helperScript;
    public Score score;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnControllerCollider(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Collectable" && hit.collider.gameObject.GetComponent<Collectable>().collectable)
        {
            helperScript .manager.score += hit.gameObject.GetComponent<Collectable>().bonus;
            score.AddScore(hit.gameObject.GetComponent<Collectable>().bonus);
            hit.gameObject.GetComponent<PhotonView>().RPC("delete", RpcTarget.All);
            hit.collider.gameObject.GetComponent<Collectable>().collectable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine)
        {
            

            if(isSprinting && mainCam.fieldOfView < sprintPOV)
            {
                mainCam.fieldOfView += povmultiplier * Time.deltaTime;
            }
            if(!isSprinting && mainCam.fieldOfView > normalPOV)
            {
                mainCam.fieldOfView -= povmultiplier * Time.deltaTime;
            }

            if(weaponManager.currentweapon == 1)
            {
                if(weaponManager.Primary.GetComponent<Gun>().aiming)
                {
                    aimed = true;
                }
                else
                {
                    aimed = false;
                }
            }

            if(weaponManager.currentweapon == 2)
            {
                if(weaponManager.Secondary.GetComponent<Gun>().aiming)
                {
                    aimed = true;
                }
                else
                {
                    aimed = false;
                }
            }

            if(aimed)
            {
                crosshair.SetActive(false);
            } else
            {
                crosshair.SetActive(true);
            }

            if(isSprinting)
            {
                CameraShakeAnimObject.GetComponent<Animation>().Play(shakeAnim);
            }

            if(Stamina < maxStamina)
            {
                sprintSlider.gameObject.SetActive(true);
            }
            else
            {
                sprintSlider.gameObject.SetActive(false);
            }

            //Audio
            if(isSprinting)
            {
                sprintSoundObj.SetActive(true);
            } else
            {
                sprintSoundObj.SetActive(false);
            }

            //Animations
            if(!isGrounded)
            {
                LandFlick = true;
            }

            if(LandFlick && isGrounded)
            {
                LandAnimObject.GetComponent<Animation>().Play(landAnim);
                LandFlick = false;
            }

            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                SprintAnimObject.GetComponent<Animation>().Play(sprintAnim);
            }

            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                SprintAnimObject.GetComponent<Animation>().Play(sprintReturnAnim);
            }

            if(isWalking && isGrounded && !aimed)
            {
                WalkAnimObject.GetComponent<Animation>().Play(walkAnim);
            }

            if(isSprinting)
            {
                SprintAnim2Object.GetComponent<Animation>().Play(sprintAnim2);
            }
            

            //sprinting
            sprintSlider.value = Stamina;
            sprintSlider.maxValue = maxStamina;

            if(Stamina > 10)
            {
                emptyStamina = false;
            }

            if(Stamina <= 0)
            {
                emptyStamina = true;
            }

            if(emptyStamina)
            {
                isSprinting = false;
            }

            if(Input.GetKey(KeyCode.LeftShift) && !emptyStamina)
            {
                isSprinting = true;
            } else
            {
                isSprinting = false;
            }

            if(isSprinting && Stamina > 0)
            {
                Stamina -= StaminaDecay * Time.deltaTime;
            }
            else
            {
                if(Stamina < maxStamina)
                {
                    Stamina += StaminaHeal * Time.deltaTime;
                }
            }

            //moving
            if(Input.GetKey(KeyCode.W) && !isSprinting || Input.GetKey(KeyCode.A) && !isSprinting || Input.GetKey(KeyCode.D) && !isSprinting || Input.GetKey(KeyCode.A) && !isSprinting)
            {
                isWalking = true;
            } else
            {
                isWalking = false;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            if(!isSprinting)
            {
                if(!aimed)
                {
                    controller.Move(move * speed * Time.deltaTime);
                }
                else
                {
                    controller.Move(move * aimSpeed * Time.deltaTime);
                }
            }
            else 
            {
            controller.Move(move * sprintSpeed * Time.deltaTime); 
            }

            
            velocity.y -= gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            isGrounded = Physics.CheckSphere(groundChecker.position, groundedDistance, groundMask);

            if(isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            } 

            if(Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeigth * 2f * gravity);
        JumpAnimObject.GetComponent<Animation>().Play(jumpAnim);
    }
}
