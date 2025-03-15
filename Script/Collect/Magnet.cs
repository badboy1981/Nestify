using MazeScreenManagement;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Collectable
{
    public class Magnet : Collectable
    {
        [SerializeField] ConstantForce Force;
        [SerializeField] Collision collision;
        [SerializeField] Vector3 NewForce;
        [SerializeField] Vector3 NewRelForce;
        [SerializeField] Vector3 NewTorque;


        public override void OnTriggerEnter(Collider other)
        {
            Force = other.GetComponent<ConstantForce>();
            Force.force = NewForce;
            Force.relativeForce = NewRelForce;
        }
        private void Four()
        {

        }
        private void Three(Collider other)
        {
            Debug.Log("injAAAAAAAAAAAAAAAAAAA");
            Vector3 currentRotation = other.transform.eulerAngles;
            //currentRotation.y = (currentRotation.y + 180) % 360;
            currentRotation.y = (currentRotation.y + 90) - 360;
            //currentRotation.y = currentRotation.y - 270;
            other.transform.eulerAngles = currentRotation;
        }
        private void Two(Collider other)
        {

        }
        private void One()
        {

        }

        private void OnTriggerExit(Collider other)
        {
            Force.force = Vector3.zero;
            Force.relativeForce = Vector3.zero;
        }
        public void Excute()
        {

        }
    }

    [CustomEditor(typeof(Magnet))]
    public class ControllerPositionUI : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Magnet progressBar = (Magnet)target;
            if (GUILayout.Button("Drow Button!"))
            {
                progressBar.Excute();
            }
        }
    }
}