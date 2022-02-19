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

    public CanvasGroup toFadeGroup;
    public float fadeTime = 0.7f;
    public float LoadingBuffTime = 1;

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
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)){
            GoStage(Scene_1);
        }

        if(Input.GetKeyDown(KeyCode.F2)){
            GoStage(Scene_2);
        }

        if(Input.GetKeyDown(KeyCode.F3)){
            GoStage(Scene_3);
        }

        if(Input.GetKeyDown(KeyCode.F4)){
            GoStage(Scene_4);
        }

        if(Input.GetKeyDown(KeyCode.F5)){
            GoStage(Scene_5);
        }

        if(Input.GetKeyDown(KeyCode.F6)){
            GoStage(Scene_6);
        }

        if(Input.GetKeyDown(KeyCode.F7)){
            GoStage(Scene_7);
        }

        if(Input.GetKeyDown(KeyCode.F8)){
            GoStage(Scene_8);
        }

        if(Input.GetKeyDown(KeyCode.F9)){
            GoStage(Scene_9);
        }

        if(Input.GetKeyDown(KeyCode.F10)){
            GoStage(Scene_10);
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
