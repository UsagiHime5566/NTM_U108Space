
using UnityEngine;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Collections.Concurrent;
 
public class TcpImageStream : MonoBehaviour
{
    Thread m_NetworkThread;
    bool m_NetworkRunning;
    ConcurrentQueue<byte[]> dataQueue = new ConcurrentQueue<byte[]>();
    private void OnEnable()
    {
        m_NetworkRunning = true;
        m_NetworkThread = new Thread(NetworkThread);
        m_NetworkThread.Start();
        
    }
    private void OnDisable()
    {
        m_NetworkRunning = false;
        if (m_NetworkThread != null)
        {
            if (!m_NetworkThread.Join(100))
            {
                m_NetworkThread.Abort();
            }
        }
    }
    private void NetworkThread()
    {
        TcpClient client = new TcpClient();
        client.Connect("127.0.0.1", 1234);
        using (var stream = client.GetStream())
        {
            BinaryReader reader = new BinaryReader(stream);
            try
            {
                while (m_NetworkRunning && client.Connected && stream.CanRead)
                {
                    int length = reader.ReadInt32();
                    byte[] data = reader.ReadBytes(length);
                    dataQueue.Enqueue(data);
                }
            }
            catch
            {
 
            }
        }
    }
 
    public Material mat;
    public Texture2D tex = null;
 
    void Update()
    {
        byte[] data;
        if (dataQueue.Count > 0 && dataQueue.TryDequeue(out data))
        {
            if (tex == null)
                tex = new Texture2D(1, 1);
            tex.LoadImage(data);
            tex.Apply();
            mat.mainTexture = tex;
        }
    }
}