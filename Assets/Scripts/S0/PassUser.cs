using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassUser : MonoBehaviour
{
    public SignalServer server;
    //public PosManager getPos;

    public List<Vector2> pos;
    

    void Start()
    {
        StartCoroutine(LoopSendPosCloset());
    }

    IEnumerator LoopSendPosCloset(){
        yield return new WaitForSeconds(3);
    }
    
    void Update()
    {
        
    }
}
