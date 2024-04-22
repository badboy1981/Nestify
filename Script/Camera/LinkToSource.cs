using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkToSource : MonoBehaviour
{
    public GameObject ObjectPositionRead;
    private Vector3 ObjPos;
    private Quaternion ObjRot;

    void Start() { }
    void Update()
    {
        ObjPos = ObjectPositionRead.transform.position;
        ObjRot = ObjectPositionRead.transform.rotation;
    }
    private void LateUpdate()
    {
        transform.SetPositionAndRotation(ObjPos, ObjRot);
    }
}