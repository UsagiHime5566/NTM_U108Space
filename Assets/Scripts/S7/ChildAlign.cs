using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAlign : MonoBehaviour
{
    public float everyWidth;
    [EasyButtons.Button]
    void RunIt(){
        float currentx = 0;
        foreach (Transform item in transform)
        {
            var rect = item.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(everyWidth * currentx, 0);
            rect.sizeDelta = new Vector2(everyWidth, 500);

            currentx++;
        }
    }
}
