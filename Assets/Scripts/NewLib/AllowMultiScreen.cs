using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowMultiScreen : MonoBehaviour
{
    void Awake()
    {
        ActiveMultiScreen();
    }

    void ActiveMultiScreen(){
        Debug.Log("Monitor connected count: " + Display.displays.Length);
        for (int i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }
}
