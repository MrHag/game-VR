using System;
using System.Collections;
using System.Collections.Concurrent;
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
    public event Action<string> OnData;

    volatile bool work = false;

    ConcurrentQueue<string> cq;

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
                while (work)
                {
                    var res = reader.ReadLine();

                    cq.Enqueue(res);

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
        cq = new ConcurrentQueue<string>();
        work = true;
        Thread thread = new Thread(new ThreadStart(ProcessConnection));
        thread.Start();
    }

    void OnDisable()
    {
        work = false;
    }

    void Update()
    {
        if (!cq.IsEmpty && cq.TryDequeue(out string res))
        {
            OnData?.Invoke(res);
        }
    }
}