using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class MiBandClient : MonoBehaviour
{

    private TcpClient socket;

    private StreamReader reader;

    private Task<string> readTask;

    private Coroutine coroutine;

    public event Action<string> OnData;

    private volatile bool work;

    void ProcessConnection()
    {
        while (work)
        {
            try
            {
                print("Connecting 127.0.0.1:58689...");

                socket = new TcpClient();

                socket.Connect("127.0.0.1", 58689);

                var stream = socket.GetStream();

                reader = new StreamReader(stream, Encoding.UTF8);

                print("start read");
                while (true)
                {
                    var res = reader.ReadLine();
                    OnData.Invoke(res);
                    if (reader.EndOfStream)
                        break;
                }

            }
            catch (Exception e)
            {
                print(e.Message);
            }
            finally
            {
                socket.Close();
            }

            //await Task.Delay(3000);

            Thread.Sleep(3000);
        }
    }

    void OnEnable()
    {
        work = true;
        Thread thread = new Thread(new ThreadStart(ProcessConnection));
        thread.Start();
    }

    void OnDisable()
    {
        work = false;
    }
}