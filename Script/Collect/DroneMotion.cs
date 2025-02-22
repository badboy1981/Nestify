using UnityEngine;

public class DroneMotion : MonoBehaviour
{

    [SerializeField] new ConstantForce constantForce;
    [SerializeField] Animator animator;
    [SerializeField] float IdleMotion;

    private Vector3 DronePosition;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        IdleMotion = 0.5f;
    }
    private void Update()
    {
        animator.SetBool("Motion", constantForce.relativeForce.z != 0);
        DronePosition = transform.position;
        DronePosition.y = IdleMotion;
        gameObject.transform.position = DronePosition;
    }
}