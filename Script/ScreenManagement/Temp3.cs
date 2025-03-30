using UnityEngine;

public class Temp3 : MonoBehaviour
{
    [SerializeField] Transform Gate;
    [SerializeField] float RepelForce = 10f;

    private void Start()
    {
        Gate = GetComponent<Transform>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Drone")
        {
            Rigidbody droneRigidbody = other.GetComponent<Rigidbody>();
            if (droneRigidbody != null)
            {
                Vector3 repelDirection = (other.transform.position - Gate.position).normalized;
                droneRigidbody.AddForce(repelDirection * RepelForce, ForceMode.Impulse);
            }
        }
    }
}