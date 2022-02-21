using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PunChatReciever : MonoBehaviour
{
    public float msg_y_min = 80;
    public float msg_y_max = 575;
    public float msg_b_begin = 5500;
    public float msg_liveTime = 60;

    public TextMeshProUGUI Prefab_txt;
    public RectTransform parentText;
    void Start()
    {
        PunChatManager.instance.OnNewMessageComing += OnRecieveMsg;

        PunChatManager.instance.GarenteeConnect();
    }

    void OnRecieveMsg(string msg){
        var temp = Instantiate(Prefab_txt, parentText);
        temp.text = msg;

        temp.rectTransform.DOAnchorPos(new Vector2(msg_b_begin, Random.Range(msg_y_min, msg_y_max)), msg_liveTime).OnComplete(() => {
            Destroy(temp.gameObject, 3);
        });
    }
}
