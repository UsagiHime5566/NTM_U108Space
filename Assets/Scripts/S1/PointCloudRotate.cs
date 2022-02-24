using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloudRotate : MonoBehaviour
{
    public float CurrentFaceAngle;
    public PosData pdata;
    void Start()
    {
        
    }

    private void OnValidate() {
        //MoveObject(CurrentFaceAngle + 90);
    }

    // Update is called once per frame
    void Update()
    {
        if(PosManager.instance == null)
            return;
        
        float angle = Mathf.Atan2(-PosManager.instance.minPos.y, PosManager.instance.minPos.x) * Mathf.Rad2Deg;

        CurrentFaceAngle = angle;
        pdata = PosManager.instance.minPos;

        float r = angle + 90;
        //Debug.Log("r1:" + r);
        while(r < 0){
            r = r + 360;
        }
        //Debug.Log("r2:" + r);
        while(r > 360){
            r = r - 360;
        }
        //Debug.Log("r3:" + r);
        //CurrentFaceAngle = r;
        //angle = angle < 0 ? angle + 360 : angle;

        MoveObject(r);
    }

    void MoveObject(float r){
        transform.position = new Vector3(14 - (r/360)*28, 0, 0);
    }
}
