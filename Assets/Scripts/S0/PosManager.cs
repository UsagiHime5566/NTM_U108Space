using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosManager : HimeLib.SingletonMono<PosManager>
{
   public SignalClient client;
   public Action<List<PosData>> OnRecievePosData;
    List<PosData> posList;
    PosData minPos = new PosData();
    void Start()
    {
        client.OnSignalReceived.AddListener(RecieveData);
    }

    void RecieveData(string data){
        string[] result = data.Split("{\"data\"");
        string f_data;
        if(result.Length > 1){
            f_data = "{\"data\""  + result[1];

            //Debug.Log(f_data);
            var obj = JsonUtility.FromJson<PosDataBase>(f_data);
            OnRecievePosData?.Invoke(obj.data);

            posList = obj.data;

            float distance = 9999;
            PosData t = new PosData();
            foreach (var item in obj.data)
            {
                float dis = Mathf.Sqrt((item.x * item.x) + (item.y + item.y));
                if(dis < distance){
                    distance = dis;
                    t = item;
                }
            }

            if(distance < 9999){
                Debug.Log($">> {t.x}, {t.y}");
                
                minPos = t;

                // if(ReadyToSend)
                //     SendSerial(new Vector2(t.x, t.y));
            }
            
            //Debug.Log(obj.data.Count);
        }
    }
}
