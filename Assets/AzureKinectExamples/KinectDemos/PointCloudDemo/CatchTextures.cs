using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CatchTextures : MonoBehaviour
{
    public SignalServer signalServer;
    public RawImage rawImage;
    byte[] buffer = null;
    private Queue<byte[]> datas;
    
    int readTimes = 0;
    bool receState = true;
    
    void Start()
    {
        datas = new Queue<byte[]>();
        buffer = new byte[1024 * 1024 * 10];
        signalServer.OnSignalReceivedByte += OnRecieveData;
    }

    // Update is called once per frame
    void Update()
    {
        if (datas.Count > 0)
        {
            // 处理纹理数据，并显示
            Texture2D texture2D = new Texture2D(Screen.width, Screen.height);
            texture2D.LoadImage(datas.Dequeue());
            rawImage.texture = texture2D;

            Debug.Log("Draw it");
        }
    }

    MemoryStream ms = null;
	public void BytesToImage(int count, byte[] bytes)
    {
        try
        {
            ms = new MemoryStream(bytes, 0, count);
            datas.Enqueue(ms.ToArray());    // 将数据存储在一个队列中，在主线程中解析数据。这是一个多线程的处理。

            readTimes++;
            Debug.Log(readTimes);

            if (readTimes > 5000)
            {
                readTimes = 0;
                GC.Collect(2);  // 达到一定次数的时候，开启GC，释放内存
            }
        }
        catch
        {

        }
        receState = true;
    }

    public void OnRecieveData(byte[] buffer){
        receState = false;
        BytesToImage(buffer.Length, buffer);
    }
}
