using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge_S6 : JudgeBase
{
    public Transform Building;
    public S6DoorController s6Controller;

    public float scaleSpeed = 0.01f;

    void Start(){
        Building.localPosition = SystemConfig.Instance.GetData<Vector3>(SaveKey, Building.localPosition);
    }

    public override void VerticalJudge(int direct){
        if(direct == 1)
            Building.localPosition = new Vector3(Building.localPosition.x, Building.localPosition.y + moveSpeed, Building.localPosition.z);
        if(direct == -1)
            Building.localPosition = new Vector3(Building.localPosition.x, Building.localPosition.y - moveSpeed, Building.localPosition.z);

        SystemConfig.Instance.SaveData(SaveKey, Building.localPosition);
    }

    public override void HorizenJudge(int direct)
    {
        if(direct == 1)
            Building.localPosition = new Vector3(Building.localPosition.x + moveSpeed, Building.localPosition.y, Building.localPosition.z);
        if(direct == -1)
            Building.localPosition = new Vector3(Building.localPosition.x -moveSpeed, Building.localPosition.y, Building.localPosition.z);

        SystemConfig.Instance.SaveData(SaveKey, Building.localPosition);
    }

    public override void ScaleX(int direct)
    {
        if(direct == 1)
            s6Controller.ScaleDoorX(1, scaleSpeed);
        if(direct == -1)
            s6Controller.ScaleDoorX(-1, scaleSpeed);
    }

    public override void ScaleZ(int direct)
    {
        if(direct == 1)
            s6Controller.ScaleDoorZ(1, scaleSpeed);
        if(direct == -1)
            s6Controller.ScaleDoorZ(-1, scaleSpeed);
    }
}
