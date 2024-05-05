using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectableMoveControll : MonoBehaviour
{
    [SerializeField] float _X, _Y, _Z, _W;
    private void Start()
    {
        _X = 0;
        _Y = 0;
        _Z = 0;
        _W = 100;
    }
    private void Update()
    {
        transform.rotation = Quaternion.Euler(_X, _Y, _Z);
        _Y += _W * Time.deltaTime;
    }
}