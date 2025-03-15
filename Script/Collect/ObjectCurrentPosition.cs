using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectCurrentPosition", menuName = "My Asset/Object Current Position")]
public class ObjectCurrentPosition : ScriptableObject
{
    //public Transform Mtransform;
    public Vector3 Mposition;
    public Vector3 MeulerAngles;
    public Quaternion Mrotation;

    public Vector3 MeulerAnglesMirror;
}