using UnityEngine;

namespace TelePort
{
    [CreateAssetMenu(fileName = "PointProperty", menuName = "Game Data/Point Property")]
    public class PointProperty : ScriptableObject
    {
        public Vector3 Position;
        //public Vector3 EulerAngles;
        //public Quaternion QuaternionAngle;

        public void SetPosition()
        {
            Position = GameObject.Find("Drone").GetComponent<Transform>().position;
        }
    }
}