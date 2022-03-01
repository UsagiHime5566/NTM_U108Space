using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class UserVideoManager : HimeLib.SingletonMono<UserVideoManager>
{
    public NetworkManager networkManager;

    public float catchPeriod = 60;

    string PathFolder;
    string VideoFolder = "Videos";
    string geturl;
    VideoListRoot userLists;

    void Start(){
        userLists = new VideoListRoot();
        geturl = networkManager.serverURL + networkManager.api_upload_Video;

        PathFolder = Path.Combine (Application.persistentDataPath, VideoFolder);
        if (!Directory.Exists(PathFolder))
            Directory.CreateDirectory(PathFolder);

        StartCoroutine(LoopCatchList());
    }

    IEnumerator LoopCatchList(){
        while (true)
        {
            networkManager.API_GetURL(geturl, x => {
                userLists = JsonUtility.FromJson<VideoListRoot>(JsonWithParent(x));
                //Debug.Log(temp);
                //Debug.Log(temp.videoListRoot);
                Debug.Log("Updated playlist finished.");
            });
            yield return new WaitForSeconds(catchPeriod);
        }
    }

    public void GetRandomVideoUrl(System.Action<string> callback){
        string fileName = userLists.videoListRoot[Random.Range(0, userLists.videoListRoot.Count)].media_filename;
        string fullUrl = networkManager.API_GetMedia(fileName);

        StartCoroutine(LoadVideoFromThisURL(fileName, fullUrl, callback));
    }

    private IEnumerator LoadVideoFromThisURL(string fileName, string fullUrl, System.Action<string> callback)
    {
        string _pathToFile = Path.Combine (PathFolder, $"{fileName}.mp4");

        if(IsPathFileExist(_pathToFile)){
            callback?.Invoke(_pathToFile);
            yield break;
        }

        UnityWebRequest _videoRequest = UnityWebRequest.Get (fullUrl);

        var asyncOp = _videoRequest.SendWebRequest();

        while(!asyncOp.isDone){
            yield return null;
        }

        if (_videoRequest.isDone == false || _videoRequest.error != null)
        {
            Debug.Log ("Video Request >> " + _videoRequest.error );
            yield break;
        }

        Debug.Log ("Video Request Done >> " + _videoRequest.isDone);

        byte[] _videoBytes = _videoRequest.downloadHandler.data;

        File.WriteAllBytes (_pathToFile, _videoBytes);
        
        Debug.Log ($"Save video to {_pathToFile}");

        callback?.Invoke (_pathToFile);
        yield return null;
    }

    bool IsPathFileExist(string path){
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Debug.LogWarning($"Directory does not exist : {path}, try to download.");
            return false;
        }

        if (!File.Exists(path))
        {
            Debug.Log($"File does not exist : {path}, try to download.");
            return false;
        }

        Debug.Log($"Read video from catch : {path}");
        return true;
    }

    string JsonWithParent(string json){
        var fullJson = "{\"videoListRoot\":" + json + "}";
        //Debug.Log(fullJson);
        return fullJson;
    }
}
