using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer)), RequireComponent(typeof(RawImage))]
public class StaticVideo : MonoBehaviour
{
    public VideoClip clip;
    VideoPlayer vp;
    RawImage img;
    void Awake(){
        vp = GetComponent<VideoPlayer>();
        img = GetComponent<RawImage>();
    }

    void Start()
    {
        vp.prepareCompleted += x => {
            x.Play();
        };

        vp.clip = clip;
        RenderTexture rt = new RenderTexture(720, 960, 0);
        vp.targetTexture = rt;
        img.texture = rt;
        
        vp.Prepare();
    }

    private void Reset() {
        vp = GetComponent<VideoPlayer>();
        vp.isLooping = true;
        vp.audioOutputMode = VideoAudioOutputMode.None;
    }    
}
