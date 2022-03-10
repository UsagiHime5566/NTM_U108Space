using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class S0_Rotate : MonoBehaviour
{
    public float spanTime = 5;
    public bool isStartRun = true;

    Tweener tweener;
    void Start()
    {
        if(isStartRun){
            tweener = transform.DOLocalRotate(new Vector3(0, 360, 0), spanTime, RotateMode.FastBeyond360 ).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }
    }

    bool isClick = false;

    void Update(){
        if(Input.GetKeyDown(KeyCode.M)){
            if(tweener != null){
                tweener.Kill();
            }
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
