using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas360Control : MonoBehaviour
{
    public GameObject targetGameObject;
    private void Start() {
        targetGameObject.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
            targetGameObject.SetActive(!targetGameObject.activeSelf);
        }
    }
}
