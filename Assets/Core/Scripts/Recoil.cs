using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public Vector3 currentRotation;
    public Vector3 targetRotation;

    public float RecoilX;
    public float RecoilY;
    public float RecoilZ;
    public float ScopeRecoilX;
    public float ScopeRecoilY;
    public float ScopeRecoilZ;

    public float EffectStrength;
    public float ReturnSpeed;
    public bool Aiming;
    public Character character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Aiming = character.aimed;
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, ReturnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, EffectStrength * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void recoil()
    {
        if(!Aiming)
        {
            targetRotation += new Vector3(RecoilX, Random.Range(-RecoilY, RecoilY), Random.Range(-RecoilZ, RecoilZ));
        } else
        {
            targetRotation += new Vector3(ScopeRecoilX, Random.Range(-ScopeRecoilY, ScopeRecoilY), Random.Range(-ScopeRecoilZ, ScopeRecoilZ));
        }
    }
}
