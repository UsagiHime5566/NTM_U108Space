using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RectVideo : MonoBehaviour
{
    public RawImage show;
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.prepareCompleted += x => {
            x.Play();
        };
    }

    public void SetupVideo(string _url){
        videoPlayer.url = _url;
        videoPlayer.Prepare();

        RenderTexture rt = new RenderTexture(300, 200, 0);
        videoPlayer.targetTexture = rt;
        show.texture = rt;
    }
}
