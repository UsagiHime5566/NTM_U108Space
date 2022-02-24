using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveArduino : MonoBehaviour
{
    public SignalServer arduinoServer;
    void Start()
    {
        arduinoServer.OnSignalReceived.AddListener(OnRotateAngle);
    }

    void OnRotateAngle(string msg){
        Debug.Log(msg);
    }

}
