using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public Animator animator;

    public PulseControler pulseControler;

    public Light flowerLight;

    public bool open;

    public bool half;

    private bool _open;

    private bool _half;
    public bool Open
    {
        get
        {
            return _open;
        }
        set
        {
            if (value == _open)
                return;

            _open = value;
            animator.SetBool("Open", _open);
        }
    }

    public bool Half
    {
        get
        {
            return _half;
        }
        set
        {
            if (value == _half)
                return;

            _half = value;
            animator.SetBool("Half", _half);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pulseControler.pulseStatusChanged += PulseStatusChanged;
    }

    void PulseStatusChanged(PulseStatus status)
    {
        switch (status)
        {
            case PulseStatus.LOW:
                {
                    Open = true;
                    Half = false;
                    flowerLight.intensity = 1.0f;
                    break;
                }
            case PulseStatus.HIGH:
                {
                    Open = false;
                    Half = false;
                    flowerLight.intensity = 0.05f;
                    break;
                }
            case PulseStatus.NORMAL:
                {
                    Half = true;
                    flowerLight.intensity = 0.01f;
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnValidate()
    {
        Open = open;
        Half = half;
    }
}
