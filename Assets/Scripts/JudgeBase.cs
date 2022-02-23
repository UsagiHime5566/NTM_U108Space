using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeBase : MonoBehaviour
{
    public string SaveKey;
    public float moveSpeed = 0.05f;
    public virtual void VerticalJudge(int direct){}

    public virtual void HorizenJudge(int direct){}
    public virtual void ScaleX(int direct){}
    public virtual void ScaleZ(int direct){}

    void Update(){
        if(Input.GetKey(KeyCode.W)){
            VerticalJudge(1);
        }
        if(Input.GetKey(KeyCode.S)){
            VerticalJudge(-1);
        }
        if(Input.GetKey(KeyCode.A)){
            HorizenJudge(1);
        }
        if(Input.GetKey(KeyCode.D)){
            HorizenJudge(-1);
        }

        if(Input.GetKey(KeyCode.Z)){
            ScaleX(1);
        }
        if(Input.GetKey(KeyCode.X)){
            ScaleX(-1);
        }
        if(Input.GetKey(KeyCode.C)){
            ScaleZ(1);
        }
        if(Input.GetKey(KeyCode.V)){
            ScaleZ(-1);
        }
    }
}
