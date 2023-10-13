using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{

    public static object log {set{
       Debug.Log(value); 
    }}
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
