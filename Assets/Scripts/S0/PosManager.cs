using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosManager : HimeLib.SingletonMono<PosManager>
{
    public SignalClient client;
    public Action<List<PosData>> OnRecievePosData;
    public List<PosData> posList;
    public PosData minPos = new PosData();
    public float angleDelta;
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
                //Debug.Log($">> {t.x}, {t.y}");
                
                minPos = t;

                // if(ReadyToSend)
                //     SendSerial(new Vector2(t.x, t.y));
            }
            
            //Debug.Log(obj.data.Count);
        }
    }
    public float currentAtan;
    public float GetCurrentAngle(){
        float angle = Mathf.Atan2(minPos.y, minPos.x) * Mathf.Rad2Deg;
        angle = angle + angleDelta;

        currentAtan = angle;
        
        return angle;
    }

    [EasyButtons.Button]
    void GetAtan(){
        Debug.Log(Mathf.Atan2(minPos.y, minPos.x) * Mathf.Rad2Deg) ;
    }
}
