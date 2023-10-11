using System.Diagnostics;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public string processPath = "./MiBand_Build/miband.exe";
    // Start is called before the first frame update
    void Start()
    {
        var proc = new Process
        {

            StartInfo = new ProcessStartInfo(processPath)
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            }
        };

        proc.OutputDataReceived += (s, d) => { 
            var packet = JsonUtility.FromJson<Packet>(d.Data);
            
            switch(packet.type)
            {
                case Types.HEARTRATE:
                    UnityEngine.Debug.Log("HEARTRATE: "+packet.value); 
                    break;

                case Types.MESSAGE:
                    UnityEngine.Debug.Log("MESSAGE: "+packet.value); 
                    break;
            }

        };

        UnityEngine.Debug.Log("Process status: " + proc.Start());
        proc.BeginOutputReadLine();
        // console.writeline(output);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
