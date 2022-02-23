using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S6DoorController : MonoBehaviour
{
    void Start(){
        Vector3 scale = SystemConfig.Instance.GetData<Vector3>("S6Door", allSubDoor[0].localScale);

        foreach (var item in allSubDoor)
        {
            item.localScale = scale;
        }
    }
    public List<Transform> allSubDoor;
    public void ScaleDoorX(int dir, float speed){
        if(dir == 1){
            foreach (var item in allSubDoor)
            {
                item.localScale = new Vector3(item.localScale.x + speed, item.localScale.y, item.localScale.z);
            }
        }

        if(dir == -1){
            foreach (var item in allSubDoor)
            {
                item.localScale = new Vector3(item.localScale.x -speed, item.localScale.y, item.localScale.z);
            }
        }

        SystemConfig.Instance.SaveData("S6Door", allSubDoor[0].localScale);
    }

    public void ScaleDoorZ(int dir, float speed){
        if(dir == 1){
            foreach (var item in allSubDoor)
            {
                item.localScale = new Vector3(item.localScale.x, item.localScale.y, item.localScale.z + speed);
            }
        }

        if(dir == -1){
            foreach (var item in allSubDoor)
            {
                item.localScale = new Vector3(item.localScale.x, item.localScale.y, item.localScale.z - speed);
            }
        }

        SystemConfig.Instance.SaveData("S6Door", allSubDoor[0].localScale);
    }
}
