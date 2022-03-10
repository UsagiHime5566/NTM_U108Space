using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieveDeviceRotate : MonoBehaviour
{
    public float minX = -8;
    public float maxX = 9;
    public float baseX = -0.55f;

    public float baseY = -0.4f;

    Vector3 targetPosition;

    private void OnEnable() {
        ReceiveArduino.instance.OnRecieveArduinoAngle += ParseData;
    }

    private void OnDisable() {
        ReceiveArduino.instance.OnRecieveArduinoAngle -= ParseData;
    }

    void Start(){
        targetPosition = transform.position;
    }

    void Update(){
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
    }


    void ParseData(string msg, float angle){
        float range = maxX - minX;
        float runDis = (angle / 360) * range;

        runDis = runDis + baseX;
        if(runDis > maxX) runDis = runDis - range;

        //transform.position = new Vector3(runDis, baseY, 0);
        targetPosition = new Vector3(runDis, baseY, 0);
    }
}
