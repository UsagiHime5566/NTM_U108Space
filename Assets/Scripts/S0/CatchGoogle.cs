using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ideafixxxer.CsvParser;
using TMPro;
using DG.Tweening;

public class CatchGoogle : MonoBehaviour
{
    public string webService;
    public string sheetID;
    public string Infos_PageID;

    public float msg_y_min = 80;
    public float msg_y_max = 575;
    public float msg_b_begin = 5500;
    public float msg_liveTime = 60;

    public TextMeshProUGUI Prefab_txt;
    public RectTransform parentText;
    int lastCount = 0;

    void Start()
    {
        
        StartCoroutine(CatchDataLoop());
    }

    IEnumerator CatchDataLoop(){
        while (true)
        {
            DownloadManager.GoogleGetCSV(ImportData, webService, sheetID, Infos_PageID);

            yield return new WaitForSeconds(20);
        }
    }

    void ImportData(string csvFile){
        //讀入 CSV 檔案，使其分為 string 二維陣列
        CsvParser csvParser = new CsvParser();
        string[][] csvTable = csvParser.Parse(csvFile);

        List<string> listMsg = new List<string>();
        for (int i = 1; i < csvTable.Length; i++)
        {
            string m = csvTable[i][1];

            listMsg.Add(m);
        }

        for (int i = lastCount; i < listMsg.Count; i++)
        {
            StartCoroutine(CreateMessage(listMsg[i]));
        }

        Debug.Log($"total count {lastCount}");

        lastCount = listMsg.Count;
    }

    IEnumerator CreateMessage(string msg){
        yield return new WaitForSeconds(Random.Range(0, 10));

        var temp = Instantiate(Prefab_txt, parentText);
        temp.text = msg;

        temp.rectTransform.anchoredPosition = new Vector2(msg_b_begin, Random.Range(msg_y_min, msg_y_max));
        temp.rectTransform.DOAnchorPos(new Vector2(-msg_b_begin, temp.rectTransform.anchoredPosition.y), msg_liveTime).OnComplete(() => {
            Destroy(temp.gameObject, 3);
        });
    }
}
