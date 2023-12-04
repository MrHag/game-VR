using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public class Environment
{
    public Material panoramic;

    public GameObject sun;

    [ColorUsage(false, true)]
    public Color ambientColor;

    public Potion potion;

    public float reflectionIntensity;
    // Start is called before the first frame update

}
