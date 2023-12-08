using System;
using System.Diagnostics;
using UnityEngine;

public class MiBandNet : MonoBehaviour
{

    public event Action<int> Pulse;

    public MiBandClient miBandClient;

    void Start()
    {

        miBandClient.OnData += (data) =>
        {
            var packet = JsonUtility.FromJson<Packet>(data);

            UnityEngine.Debug.Log("JSON: " + data);

            switch (packet.type)
            {
                case Types.HEARTRATE:
                    UnityEngine.Debug.Log("HEARTRATE: " + packet.value);
                    Pulse.Invoke(int.Parse(packet.value));
                    break;

                case Types.MESSAGE:
                    UnityEngine.Debug.Log("MESSAGE: " + packet.value);
                    break;
            }
        };

        // UnityEngine.Debug.Log("Process status: " + proc.Start());
        // proc.BeginOutputReadLine();
    }
}
