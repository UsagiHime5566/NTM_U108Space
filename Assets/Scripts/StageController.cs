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

    

    void Start()
    {
        if(AutoPlay)
            StartCoroutine(DoAutoPlay());
    }

    
    void Update()
    {
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
            sceneBGM.Play();
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }

    IEnumerator DoAutoPlay(){
        List<string> SceneList = new List<string>(){Scene_1, Scene_2, Scene_3, Scene_4, Scene_5, Scene_6, Scene_7, Scene_8, Scene_9, Scene_10 };
        int SceneIndex = 0;

        yield return new WaitForSeconds(5);

        sceneBGM.Play();

        while(true){
            if(Application.CanStreamedLevelBeLoaded(SceneList[SceneIndex])){
                GoStage(SceneList[SceneIndex]);
                yield return new WaitForSeconds(AutoPlayDelay);
            }
                
            yield return null;
            SceneIndex = ( SceneIndex + 1 ) % SceneList.Count;
        }
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
