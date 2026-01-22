#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GateSystem3
{
    [CustomEditor(typeof(CopyTransform))]
    public class CopyTransformEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var elm = (CopyTransform)target;

            if (GUILayout.Button("Copy"))
            {
                elm.Copy();
            }
        }
    }

}
#endif