using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleZ : MonoBehaviour
{
    Renderer rend;
    Vector3 lastPos;
    Vector3 velocity;
    Vector3 lastRot;
    Vector3 angularVelocity;
    public float MaxWobble = 0.03f;
    public float wobbleSpeed = 1f;
    public float recovery = 1f;
    float WobbleAmountX;
    float WobbleAmountZ;
    float WobbleAmountToAddX;
    float WobbleAmountToAddZ;
    float pulse;
    float time = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

   private void Update()
    {
        time += Time.deltaTime;
        //decrease wobble over time
        WobbleAmountToAddX = Mathf.Lerp(WobbleAmountToAddX, 0, Time.deltaTime * (recovery));
        WobbleAmountToAddZ = Mathf.Lerp(WobbleAmountToAddZ, 0, Time.deltaTime * (recovery));

        // make asine wave of the decreasing wobble
        pulse = 2 * Mathf.PI * wobbleSpeed;
        WobbleAmountX = WobbleAmountToAddX * Mathf.Sin(pulse * time);
        WobbleAmountZ = WobbleAmountToAddZ * Mathf.Sin(pulse * time);

        //send it to the shader
        rend.material.SetFloat("_WobbleX", WobbleAmountX);
        rend.material.SetFloat("_WobbleZ", WobbleAmountZ);

        //Velocity
        velocity = (lastPos - transform.position) / Time.deltaTime;
        angularVelocity = transform.rotation.eulerAngles - lastRot;

        //Add clamped velocity to wobble
        WobbleAmountToAddX += Mathf.Clamp((velocity.x + (angularVelocity.z * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);
        WobbleAmountToAddZ += Mathf.Clamp((velocity.z = (angularVelocity.x * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);

        //Keep last position 
        lastPos = transform.position;
        lastRot = transform.rotation.eulerAngles;

    }
}
