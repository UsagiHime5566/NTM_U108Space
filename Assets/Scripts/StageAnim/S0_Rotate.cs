using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class S0_Rotate : MonoBehaviour
{
    public float spanTime = 5;

    Tweener tweener;
    void Start()
    {
        
    }

    bool isClick = false;

    void Update(){
        if(Input.GetKeyDown(KeyCode.M)){
            if(isClick) return;
            tweener = transform.DOLocalRotate(new Vector3(0, 360, 0), spanTime, RotateMode.FastBeyond360 ).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
            //isClick = true;
        }

        if(Input.GetKeyDown(KeyCode.P)){
            if(tweener != null){
                tweener.Kill();
            }
        }
    }
}
