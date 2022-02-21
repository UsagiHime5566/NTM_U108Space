using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class S0_Rotate : MonoBehaviour
{
    public float spanTime = 5;
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), spanTime, RotateMode.FastBeyond360 ).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }
}
