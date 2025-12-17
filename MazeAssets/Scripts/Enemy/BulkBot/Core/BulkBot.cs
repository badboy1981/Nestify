using UnityEngine;

[RequireComponent(typeof(Animator))]
internal class BulkBot : Enemy
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
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (DroneForce == null)
        {
            DroneForce = other.GetComponent<ConstantForce>();
        }
        DroneForce.relativeForce = new(0, 0, Power);
        //Debug.Log($"Self Name: {name} || Input Name: {other.tag}");
        //DecreaseStealCharge(EnemyList.BulkBot);

    }
}