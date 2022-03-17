using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Judge_S0 : JudgeBase
{
    public Transform Building;

    public VideoPlayer vp;

    void Start(){
        Building.localPosition = SystemConfig.Instance.GetData<Vector3>(SaveKey, Building.localPosition);
        if(vp)
            vp.targetTexture.Release();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            if(vp)
                vp.Play();
        }
    }

    public override void VerticalJudge(int direct){
        if(direct == 1)
            Building.localPosition = new Vector3(Building.localPosition.x, Building.localPosition.y + moveSpeed, Building.localPosition.z);
        if(direct == -1)
            Building.localPosition = new Vector3(Building.localPosition.x, Building.localPosition.y - moveSpeed, Building.localPosition.z);

        SystemConfig.Instance.SaveData(SaveKey, Building.localPosition);
    }
}
