using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Controller_S6 : MonoBehaviour
{
    public float firstTime;
    public float delayOpen;
    public List<Transform> allDoors;

    public float doorMove = 1;
    public float doorTime = 3;





    public RandomHuman Prefab_Human;
    public Transform CreatePointLeft;
    public Transform CreatePointRight;
    public float CreateYBase = -4;
    public float CreateYRange = 2;
    public float moveTime = 20;
    public float CreateDelay = 3;


    public float moveSpeed = 1;

    public List<RandomHuman> randomHumans;
    void Start()
    {
        StartCoroutine(OpenDoors());
    }

    IEnumerator OpenDoors(){
        yield return new WaitForSeconds(firstTime);

        foreach (var door in allDoors)
        {
            door.DOLocalMoveY(doorMove, doorTime);

            yield return new WaitForSeconds(delayOpen);
        }

        yield return new WaitForSeconds(delayOpen);

        while(true){
            StartRandomWalker();

            yield return new WaitForSeconds(CreateDelay);
        }
    }

    void StartRandomWalker(){
        Vector3 pos;
        Quaternion face;
        if(Random.Range(0, 100) > 50){
            pos = new Vector3(CreatePointLeft.position.x, 0, CreateYBase + Random.Range(-CreateYRange, CreateYRange));
            face = Quaternion.Euler(0, -90, 0);
        }
        else {
            pos = new Vector3(CreatePointRight.position.x, 0, CreateYBase + Random.Range(-CreateYRange, CreateYRange));
            face = Quaternion.Euler(0, 90, 0);
        }

        var temp = Instantiate(Prefab_Human, pos, face);
        
        // temp.transform.DOLocalMoveX(-pos.x, moveTime).OnComplete(() => {
        //     Destroy(temp.gameObject, 3);
        // });

        float moveTime = Random.Range(1, 15);
        Sequence seq = DOTween.Sequence();
        seq.Append(temp.transform.DOScale(Vector3.one, moveTime).OnUpdate(() => {
            temp.transform.Translate(-pos.x * moveSpeed * Time.deltaTime, 0, 0, Space.World);
        }).OnComplete(() => {
            temp.transform.rotation = Quaternion.Euler(0, 0, 0);
            temp.GetComponent<Animator>().SetTrigger("Stand");
        }));
        seq.AppendInterval(5);
        seq.AppendCallback(() => {
            temp.transform.rotation = face;
            temp.GetComponent<Animator>().SetTrigger("Move");
        });
        seq.Append(temp.transform.DOScale(Vector3.one, 20).OnUpdate(() => {
            temp.transform.Translate(-pos.x * moveSpeed * Time.deltaTime, 0, 0, Space.World);
        }).OnComplete(() => {
            Destroy(temp.gameObject, 3);
        }));


        randomHumans.Add(temp);
    }
}
