using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CanvasVideoRandom : MonoBehaviour
{
    public List<RectVideo> videos;
    public float waitSecond = 5;
    void Start()
    {
        StartCoroutine(LoopChange());
    }

    IEnumerator LoopChange(){
        while (true)
        {
            SetRandomVideos();
            yield return new WaitForSeconds(waitSecond);
        }
    }

    void SetRandomVideos(){
        foreach (var item in videos)
        {
            UserVideoManager.instance.GetRandomVideoUrl(x => {
                item.SetupVideo(x);
            });
        }
    }
}
