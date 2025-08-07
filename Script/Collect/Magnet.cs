using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] Transform _Magnet;
    [SerializeField] float RepelForce = 10f;

    private void Start()
    {
        _Magnet = GetComponent<Transform>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Drone")
        {
            if (other.TryGetComponent<Rigidbody>(out var droneRigidbody))
            {
                Vector3 repelDirection = (other.transform.position - _Magnet.position).normalized;
                droneRigidbody.AddForce(repelDirection * RepelForce, ForceMode.Impulse);
            }
        }
    }
}