using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JapanPlayer : MonoBehaviour
{
    public RawImage japanView;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);

        japanView.gameObject.SetActive(false);
        japanView.transform.localScale = new Vector3(1.5f, 1, 1);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            japanView.gameObject.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.P)){
            japanView.gameObject.SetActive(true);
        }
    }
}
