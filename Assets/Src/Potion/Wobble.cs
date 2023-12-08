using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    Renderer rend;

    MaterialOverride over;
    Vector3 lastPos;
    Vector3 velocity;
    Vector3 acceleration;
    Vector3 lastRot;
    Vector3 angularVelocity;
    Vector3 angularAcceleration;
    public float MaxWobble = 0.03f;
    public float WobbleSpeed = 1f;
    public float Recovery = 1f;
    float wobbleAmountX;
    float wobbleAmountZ;
    float wobbleAmountToAddX;
    float wobbleAmountToAddZ;
    float pulse;
    float time = 0.5f;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        over = GetComponent<MaterialOverride>();
        velocity = Vector3.zero;
    }
    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        // decrease wobble over time
        wobbleAmountToAddX = Mathf.Lerp(wobbleAmountToAddX, 0, Time.fixedDeltaTime * (Recovery));
        wobbleAmountToAddZ = Mathf.Lerp(wobbleAmountToAddZ, 0, Time.fixedDeltaTime * (Recovery));

        // make a sine wave of the decreasing wobble
        pulse = 2 * Mathf.PI * WobbleSpeed;
        wobbleAmountX = -wobbleAmountToAddX * Mathf.Sin(pulse * time);
        wobbleAmountZ = wobbleAmountToAddZ * Mathf.Sin(pulse * time);

        // send it to the shader
        rend.material.SetFloat("_WobbleX", wobbleAmountX);
        rend.material.SetFloat("_WobbleZ", wobbleAmountZ);

        MaterialPropertyBlock block = new MaterialPropertyBlock();

        rend.GetPropertyBlock(block);

        block.SetFloat("_WobbleX", wobbleAmountX);
        block.SetFloat("_WobbleZ", wobbleAmountZ);
        rend.SetPropertyBlock(block);

        // velocity

        var oldVelocity = velocity;
        velocity = (transform.position - lastPos) / Time.fixedDeltaTime;


        var oldAcceleration = acceleration;
        acceleration = (velocity - oldVelocity) / Time.fixedDeltaTime;

        var oldAngularVelocity = angularVelocity;

        angularVelocity = transform.rotation.eulerAngles - lastRot;

        angularAcceleration = (angularVelocity - oldAngularVelocity) / Time.fixedDeltaTime;


        // add clamped velocity to wobble
        //+ (angularAcceleration.z * 0.1f)
        if (Math.Abs(acceleration.x) - Math.Abs(oldAcceleration.x) > 0)
            wobbleAmountToAddX += Mathf.Clamp((acceleration.x * 0.2f) * MaxWobble, -MaxWobble, MaxWobble);

        //+ (angularAcceleration.x * 0.1f)
        if (Math.Abs(acceleration.z) - Math.Abs(oldAcceleration.z) > 0)
            wobbleAmountToAddZ += Mathf.Clamp((acceleration.z * 0.2f) * MaxWobble, -MaxWobble, MaxWobble);

        // keep last position
        lastPos = transform.position;
        lastRot = transform.rotation.eulerAngles;
    }



}