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
        
        GetRandomVideo((x) => {
            vp.url = x;
            vp.Prepare();
        });
    }

    public void GetRandomVideo(System.Action<string> callback){
        string geturl = NetworkManager.instance.serverURL + NetworkManager.instance.api_upload_Video;

        NetworkManager.instance.API_GetURL(geturl, x => {
            var temp = JsonUtility.FromJson<VideoListRoot>(JsonWithParent(x));
            //Debug.Log(temp);
            //Debug.Log(temp.videoListRoot);

            string targetUrl = temp.videoListRoot[Random.Range(0, temp.videoListRoot.Count)].media_filename;

            string fullUrl = NetworkManager.instance.API_GetMedia(targetUrl);

            callback?.Invoke(fullUrl);
        });
    }

    string JsonWithParent(string json){
        var fullJson = "{\"videoListRoot\":" + json + "}";
        //Debug.Log(fullJson);
        return fullJson;
    }

    [System.Serializable]
    public class VideoListRoot
    {
        public List<VideoListData> videoListRoot;
    }

    [System.Serializable]
    public class VideoListData
    {
        public string id;
        public string uid;
        public string type;
        public string media_filename;
    }
}
