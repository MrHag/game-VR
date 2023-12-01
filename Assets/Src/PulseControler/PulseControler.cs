using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PulseStatus
{
    LOW,
    HIGH,
    NORMAL
}

public class PulseControler : MonoBehaviour
{

    public MiBand miBand;

    private PulseStatus _pulseStatus = PulseStatus.NORMAL;

    public PulseStatus pulseStatus
    {
        get
        {
            return _pulseStatus;
        }

        set
        {
            if (_pulseStatus != value)
            {
                _pulseStatus = value;
                pulseStatusChanged?.Invoke(_pulseStatus);
                print("PULSE IS: " + _pulseStatus.ToString());
            }
        }
    }

    public event Action<PulseStatus> pulseStatusChanged;

    public int pulse;
    // Start is called before the first frame update
    void Start()
    {
        miBand.Pulse += PulseChanged;
    }

    void PulseChanged(int pulse)
    {
        this.pulse = pulse;

        if (pulse <= 94)
        {
            pulseStatus = PulseStatus.LOW;
        }
        else if (pulse >= 100)
        {
            pulseStatus = PulseStatus.HIGH;
        }
        else
        {
            pulseStatus = PulseStatus.NORMAL;
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
