using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [SerializeField] float PushForce = 1;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody _rb = hit.collider.attachedRigidbody;
        if (_rb != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0; forceDirection.Normalize();
            _rb.AddForceAtPosition(forceDirection * PushForce, transform.position, ForceMode.Impulse);
        }
    }
}
