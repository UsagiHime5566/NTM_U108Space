using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentationDeactive : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        if(!StageController.instance){
            Debug.Log("Not Stage instance");
            yield break;
        }

        if(!StageController.instance.PresantationMode){
            Debug.Log("Self Deactive");
            gameObject.SetActive(false);
        }
    }
}
