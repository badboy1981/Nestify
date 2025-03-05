using UnityEngine;

namespace Collectable
{
    public class Magnet : Collectable
    {
        [SerializeField] ConstantForce Force;
        [SerializeField] Vector3 NewForce;
        [SerializeField] Vector3 NewRelForce;
        [SerializeField] Vector3 NewTorque;

        public override void OnTriggerEnter(Collider other)
        {
            Force = other.GetComponent<ConstantForce>();
            Two(other);
        }
        private void Two(Collider other)
        {
            Debug.Log($"eulerAngles: {other.transform.eulerAngles}");
            Force.force = NewForce;
            NewTorque = Vector3.Reflect(gameObject.transform.eulerAngles, other.transform.eulerAngles);
            Force.torque = NewTorque;
        }
        private void One(Collider other)
        {
            Vector3 newForce = Vector3.Reflect(other.transform.eulerAngles.normalized, NewForce);
            Force.force = newForce;
            Vector2 newRelForce = Vector3.Reflect(other.transform.eulerAngles, NewRelForce);
            Force.relativeForce = newRelForce;
            Debug.Log($"{other.gameObject.name}: New Force: {newForce} || New RelForce: {NewRelForce}");
        }

        private void OnTriggerExit(Collider other)
        {
            Force.force = Vector3.zero;
            Force.relativeForce = Vector3.zero;
        }
    }
}