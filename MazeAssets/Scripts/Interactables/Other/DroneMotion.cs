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
        IdelMotion();
    }

    private void IdelMotion()
    {
        animator.SetBool("Stop", constantForce.relativeForce.z == 0);
        if (animator.GetBool("Stop"))
        {
            DronePosition = transform.position;
            DronePosition.y = IdleMotion;
            gameObject.transform.position = DronePosition;
        }
    }
}