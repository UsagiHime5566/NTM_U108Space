using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleToPosition : MonoBehaviour
{
    public float angle;
    void Start()
    {
        
    }

    // 14 ~ 7 ~ 0 ~ -7
    //-90 ~ 0 ~ 90 ~ 180
    private void OnValidate() {
        float r = angle + 90;
        while(r < 0){
            r = r + 360;
        }
        while(r > 360){
            r = r - 360;
        }
        //angle = angle < 0 ? angle + 360 : angle;
        transform.position = new Vector3(14 - (r/360)*28, -0.85f, 0);
    }
}
