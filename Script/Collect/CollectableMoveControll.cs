using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectableMoveControll : MonoBehaviour
{
    //[SerializeField] float Move1;
    //[SerializeField] float Rotation1;
    [SerializeField] Quaternion Rotation2;
    //private Vector3 ObjectPosition;
    //[Range(0, 1)]
    //[SerializeField] float RotationChange;

    private Quaternion from;
    private Quaternion to;
    readonly float speed = 100f;
    //float timeCount = 0.0f;
    [SerializeField] float _Time;
    [SerializeField] float _Time_Speed;
    private void Start()
    {
        from = new Quaternion(0, 0, 0, 1);
        to = new Quaternion(1, 0, 0, 1);
        //Move1 = 1;
        //Rotation1 = 0;
        //ObjectPosition = transform.position;
    }
    private void Update()
    {
        //transform.position = new Vector3(ObjectPosition.x, Move1, ObjectPosition.z);
        //transform.rotation = Quaternion.Lerp(new Quaternion(Rotation1, 0, 0, 0), Rotation2, RotationChange);
        //transform.rotation = Rotation2;
        _Time = Time.deltaTime;
        _Time_Speed = Time.deltaTime * speed;
        Rotation2 = Quaternion.Lerp(from, to, Time.deltaTime * speed);
        transform.rotation = Rotation2;
        //timeCount += Time.deltaTime;
    }
}