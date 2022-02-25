using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VfxMoving : MonoBehaviour
{
    public float StartX = -5;
    public float EndX = 5;
    public float duration = 10;
    void Start()
    {
        transform.position = new Vector3(StartX, 0, 0);
        transform.DOLocalMoveX(EndX, duration).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

}
