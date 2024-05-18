using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
//using UnityEngine.UI;

public class Lerp : MonoBehaviour
{
    readonly float Step = 180;
    [SerializeField][Range(0, 180)] float _Lerp = 0;
    Vector3 vec1 = Vector3.zero;
    Vector3 vec2 = Vector3.zero;
    Quaternion Qua1 = Quaternion.identity;
    Quaternion Qua2 = Quaternion.identity;
    [SerializeField] GameObject LerpObject;



    void Start()
    {
        vec1.Set(10, 0.5f, 65);
        vec2.Set(10, 0.5f, 50);
        Qua1.Set(0, 0, 0, 0);
        Qua2.Set(0, 0.5f, 0, 1);
    }
    void Update()
    {
        LerpObject.transform.SetPositionAndRotation(
            Vector3.Lerp(vec1, vec2, _Lerp / Step),
            Quaternion.RotateTowards(Quaternion.identity, Quaternion.Euler(0, Step, 0), _Lerp));
    }
}