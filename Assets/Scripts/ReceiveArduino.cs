using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveArduino : HimeLib.SingletonMono<ReceiveArduino>
{
    public SignalServer arduinoServer;

    public Action<string,float> OnRecieveArduinoAngle;
    void Start()
    {
        arduinoServer.OnSignalReceived.AddListener(OnRotateAngle);
    }

    void OnRotateAngle(string msg){
        //Debug.Log(msg);
        float f = 0;
        float.TryParse(msg, out f);
        OnRecieveArduinoAngle?.Invoke(msg, f);
    }

}
