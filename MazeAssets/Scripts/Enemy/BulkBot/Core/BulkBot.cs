using System.Collections.Generic;
using UnityEngine;

public class BulkBot : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] ConstantForce DroneForce;
    [SerializeField] float Power = 3000f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        float randomStartTime = Random.Range(0f, 1f);
        animator.Play("WalkCycle", 0, randomStartTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (DroneForce == null)
        {
            DroneForce = other.GetComponent<ConstantForce>();
        }
        DroneForce.relativeForce = new(0, 0, Power);
        Debug.Log($"Self Name: {name} || Input Name: {other.tag}");
    }
}