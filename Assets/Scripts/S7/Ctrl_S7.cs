using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Ctrl_S7 : MonoBehaviour
{
    public float rect_x = 4800;
    public float rect_y_min = 80;
    public float rect_y_max = 525;

    public RectVideo Prefab_rect;
    public RectTransform VideoParent;

    public float CreateDelay = 3;
    public float AliveTime = 30;

    void Start()
    {
        StartCoroutine(CreateVideo());
    }

    IEnumerator CreateVideo(){
        
        yield return null;

        while(true){

            for (int i = 0; i < 2; i++)
            {
                Vector2 randPos = new Vector2(Random.Range(-rect_x,rect_x), Random.Range(rect_y_min, rect_y_max));
                var temp = Instantiate(Prefab_rect, VideoParent);
                temp.GetComponent<RectTransform>().anchoredPosition = randPos;

                GetRandomVideo((x) => {
                    temp.SetupVideo(x);
                    Destroy(temp.gameObject, AliveTime);
                });
            }
            
            yield return new WaitForSeconds(CreateDelay);;
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

    string JsonWithParent(string json){
        var fullJson = "{\"videoListRoot\":" + json + "}";
        //Debug.Log(fullJson);
        return fullJson;
    }
}
