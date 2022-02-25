using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlitRender : MonoBehaviour
{
    public RenderTexture source;
    public RenderTexture target;
    public float CatchPeriod = 10;
    void Start()
    {
        StartCoroutine(CatchRender());
    }

    IEnumerator CatchRender(){
        while(true){
            Graphics.Blit(source, target);
            Debug.Log("Catch Dapth Finished");
            yield return new WaitForSeconds(Mathf.Max(1, CatchPeriod));
        }
    }
}
