using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 m_RotateValue;

    void Update()
    {
        transform.Rotate(m_RotateValue*Time.deltaTime);
    }
}
