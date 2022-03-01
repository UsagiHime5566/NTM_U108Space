using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CanvasVideoRandom : MonoBehaviour
{
    public List<RectVideo> videos;
    void Start()
    {
        foreach (var item in videos)
        {
            UserVideoManager.instance.GetRandomVideoUrl(x => {
                item.SetupVideo(x);
            });
        }
    }
}
