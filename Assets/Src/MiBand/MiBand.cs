using System;
using System.Diagnostics;
using UnityEngine;

public class MiBand : MonoBehaviour
{

    public event Action<int> Pulse;

    private bool _startApp = false;

    public bool startApp;

    private Process proc;

    public bool StartApp
    {
        get
        {
            return _startApp;
        }
        set
        {
            if (_startApp != value)
            {
                _startApp = value;

                startApp = _startApp;

                if (_startApp)
                {
                    StartApplication();
                }
                else
                {
                    StopApplication();
                }
            }
        }
    }

    public string processPath = "./MiBand_Build/miband.exe";
    // Start is called before the first frame update
    void Start()
    {
        // console.writeline(output);
        StartApp = true;
    }

    void StartApplication()
    {
        proc = new Process
        {

            StartInfo = new ProcessStartInfo(processPath)
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            }
        };

        proc.OutputDataReceived += (s, d) =>
        {
            var packet = JsonUtility.FromJson<Packet>(d.Data);

            UnityEngine.Debug.Log("JSON: " + d.Data);

            switch (packet.type)
            {
                case Types.HEARTRATE:
                    UnityEngine.Debug.Log("HEARTRATE: " + packet.value);
                    Pulse?.Invoke(int.Parse(packet.value));
                    break;

                case Types.MESSAGE:
                    UnityEngine.Debug.Log("MESSAGE: " + packet.value);
                    break;
            }

        };

        UnityEngine.Debug.Log("Process status: " + proc.Start());
        proc.BeginOutputReadLine();
    }

    void StopApplication()
    {
        proc.Close();
    }

    void OnValidate()
    {
        StartApp = startApp;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
