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
                UserVideoManager.instance.GetRandomVideoUrl(x => {
                    temp.SetupVideo(x);
                });

                Destroy(temp.gameObject, AliveTime);
            }
            
            yield return new WaitForSeconds(CreateDelay);;
        }
    }
}
