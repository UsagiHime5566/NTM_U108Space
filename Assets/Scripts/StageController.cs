using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class StageController : HimeLib.SingletonMono<StageController>
{
    protected internal override void OnSingletonAwake(){
        MarkAsCrossSceneSingleton();
    }
    
    [Header("System Params")]
    public AudioSource sceneBGM;
    public CanvasGroup toFadeGroup;
    public float fadeTime = 0.7f;
    public float LoadingBuffTime = 1;

    [Header("Auto Mode")]
    public bool AutoPlay;
    public float AutoPlayDelay = 30;

    [Header("Scene Params")]
    public string Scene_0;
    public string Scene_1;
    public string Scene_2;
    public string Scene_3;
    public string Scene_4;
    public string Scene_5;
    public string Scene_6;
    public string Scene_7;
    public string Scene_8;
    public string Scene_9;
    public string Scene_10;
    public List<string> stage_names;
    public List<float> stage_times;

    
    //local params
    bool isStagePlay = false;

    void Start()
    {
        if(AutoPlay){
            StartCoroutine(DoAutoPlay());
        } else {
            StartCoroutine(EnterScene_0());
        }
    }

    void FindAudioListener(){
        AudioListener[] myListeners = FindObjectsOfType(typeof(AudioListener)) as AudioListener[];
        foreach (var item in myListeners)
        {
            Debug.Log(item.gameObject.name);
        }
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            FindAudioListener();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            GoStage(Scene_1);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)){
            GoStage(Scene_2);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3)){
            GoStage(Scene_3);
        }

        if(Input.GetKeyDown(KeyCode.Alpha4)){
            GoStage(Scene_4);
        }

        if(Input.GetKeyDown(KeyCode.Alpha5)){
            GoStage(Scene_5);
        }

        if(Input.GetKeyDown(KeyCode.Alpha6)){
            GoStage(Scene_6);
        }

        if(Input.GetKeyDown(KeyCode.Alpha7)){
            GoStage(Scene_7);
        }

        if(Input.GetKeyDown(KeyCode.Alpha8)){
            GoStage(Scene_8);
        }

        if(Input.GetKeyDown(KeyCode.Alpha9)){
            GoStage(Scene_9);
        }

        if(Input.GetKeyDown(KeyCode.Alpha0)){
            GoStage(Scene_10);
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            StartStagePlay();
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }

    IEnumerator DoAutoPlay(){
        List<string> SceneList = new List<string>(){Scene_1, Scene_2, Scene_3, Scene_4, Scene_5, Scene_6, Scene_7, Scene_8, Scene_9, Scene_10 };
        int SceneIndex = 0;

        yield return new WaitForSeconds(5);

        //sceneBGM.Play();

        while(true){
            if(Application.CanStreamedLevelBeLoaded(SceneList[SceneIndex])){
                GoStage(SceneList[SceneIndex]);
                yield return new WaitForSeconds(AutoPlayDelay);
            }
                
            yield return null;
            SceneIndex = ( SceneIndex + 1 ) % SceneList.Count;
        }
    }

    IEnumerator EnterScene_0(){
        isStagePlay = true;
        yield return new WaitForSeconds(3);
        yield return SceneManager.LoadSceneAsync(Scene_0);
        yield return null;
        isStagePlay = false;

        Debug.Log("Ready to Play Stages");
    }

    void StartStagePlay(){
        if(isStagePlay)
            return;
        
        stage_names = new List<string>(){Scene_1, Scene_2, Scene_3, Scene_4, Scene_5, Scene_6, Scene_7, Scene_8, Scene_9, Scene_10 };
        isStagePlay = true;
        StartCoroutine(DoStagePlay());
    }

    IEnumerator DoStagePlay(){
        int SceneIndex = 0;
        yield return null;
        //sceneBGM.Play();
        float playTime = Time.realtimeSinceStartup;

        for (int i = 0; i < stage_names.Count; i++)
        {
            SceneIndex = i;
            GoStage(stage_names[SceneIndex]);
            
            Debug.Log($"Play Stage in {stage_times[SceneIndex]} seconds.");
            yield return new WaitForSeconds(stage_times[SceneIndex]);
        }

        float endTime = Time.realtimeSinceStartup;

        Debug.Log($"Total play seceonds : {endTime - playTime}");

        GoStage(Scene_0);
        yield return new WaitForSeconds(3);
        isStagePlay = false;
    }

    [ContextMenu("Calcu total")]
    void CalcuTotalTime(){
        float total = 0;
        foreach (var item in stage_times)
        {
            total += item;
        }
        Debug.Log(total);
    }

    async void GoStage(string sceneName){
        await FireFadeEffect(() => {
            SceneManager.LoadSceneAsync(sceneName);
        });
    }

    async Task FireFadeEffect(System.Action doThing){
        toFadeGroup.DOFade(1, fadeTime);
        await Task.Delay(Mathf.FloorToInt(fadeTime * 1000));
        
        doThing?.Invoke();

        await Task.Delay(Mathf.FloorToInt(LoadingBuffTime * 1000));

        toFadeGroup.DOFade(0, fadeTime);
    }
}


[System.Serializable]
public class PosDataBase
{
    public List<PosData> data;
}

[System.Serializable]
public class PosData
{
    public float x;
    public float y;
}