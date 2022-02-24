using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTo108 : MonoBehaviour
{
    public SignalServer server;
    public PosManager getPos;

    public float sendPeriod = 0.5f;
    

    void Start()
    {
        StartCoroutine(LoopSendPosCloset());
    }

    IEnumerator LoopSendPosCloset(){
        yield return new WaitForSeconds(3);

        while (true)
        {
            PosData min = PosManager.instance.minPos;
            string toSend = $"{min.x},{min.y}";
            server.SocketSend(toSend);
            yield return new WaitForSeconds(sendPeriod);
        }
    }
}
