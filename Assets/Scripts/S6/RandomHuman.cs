using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RandomHuman : MonoBehaviour
{
    public VideoPlayer vp;
    public MeshRenderer faceMat;
    void Start()
    {
        vp.prepareCompleted += v => {
            v.Play();
        };

        UserVideoManager.instance.GetRandomVideoUrl(x => {
            vp.url = x;
            vp.Prepare();
        });
    }
}
