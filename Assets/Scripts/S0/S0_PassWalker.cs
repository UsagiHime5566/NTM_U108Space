using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S0_PassWalker : MonoBehaviour
{
    public ParticleSystem Prefab_particle;
    public Transform Prefab_parent;

    public float x_scale = 5;
    public float y_scale = 5;
    public int maxEffectCount = 50;
    
    void Start()
    {
        
    }

    private void OnEnable() {
        if(PosManager.instance)
            PosManager.instance.OnRecievePosData += ShowPos;
    }

    private void OnDisable() {
        if(PosManager.instance)
            PosManager.instance.OnRecievePosData -= ShowPos;
    }

    void ShowPos(List<PosData> posData){
        foreach (var v in posData)
        {
            if(Prefab_parent.childCount > maxEffectCount)
                break;
                
            var temp = Instantiate(Prefab_particle, Prefab_parent);
            temp.transform.localPosition = ConvertToScenePos(v.x, v.y);
            
            StartCoroutine(LiveEffect(temp));
        }
    }

    Vector3 ConvertToScenePos(float x, float y){
        return new Vector3(x * x_scale, 0, -y * y_scale);
    }

    IEnumerator LiveEffect(ParticleSystem ps){
        yield return new WaitForSeconds(3);

        ps.Stop();
        Destroy(ps.gameObject, 7);
    }
}
