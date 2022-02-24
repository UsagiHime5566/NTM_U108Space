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
            GetRandomVideo(x => {
                item.SetupVideo(x);
            });
        }
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

    public void GetRandomVideoList(int count, System.Action<List<string>> callback){
        string geturl = NetworkManager.instance.serverURL + NetworkManager.instance.api_upload_Video;

        NetworkManager.instance.API_GetURL(geturl, x => {
            var temp = JsonUtility.FromJson<VideoListRoot>(JsonWithParent(x));

            List<string> s = new List<string>();
            temp.videoListRoot.ForEach(x => s.Add(x.media_filename));

            List<string> rand_urls = new List<string>();
            for (int i = 0; i < count; i++)
            {
                string r = s[Random.Range(0, s.Count)];
                rand_urls.Add(r);
                s.Remove(r);
            }
        });
    }

    string JsonWithParent(string json){
        var fullJson = "{\"videoListRoot\":" + json + "}";
        //Debug.Log(fullJson);
        return fullJson;
    }
}
