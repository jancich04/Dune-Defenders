using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float smoothness;
    public float sway;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       float mouseX = Input.GetAxisRaw("Mouse X") * sway; 
       float mouseY = Input.GetAxisRaw("Mouse Y") * sway; 

       Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
       Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

       Quaternion targetRotation = rotationX * rotationY;

       transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoothness * Time.deltaTime);
    }
}
