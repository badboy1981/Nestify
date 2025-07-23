using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UI.Image;
public class ChargeThiefBig : MonoBehaviour
{
    [SerializeField] Vector3 PointA;
    [SerializeField] Vector3 PointB;
    [SerializeField] float speed = 1f;
    [SerializeField] Transform ObjectA;
    [SerializeField] Transform ObjectB;
    [SerializeField] ConstantForce DroneForce;
    [SerializeField] float Power;

    private void Start()
    {        
        Power = 3000f;
        PointA = ObjectA.position;
        PointB = ObjectB.position;
        StartCoroutine(MoveRobotThief());
    }
    private float Rnd(float inp)
    {
        return Mathf.FloorToInt(inp + 0.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (DroneForce == null)
        {
            DroneForce = other.GetComponent<ConstantForce>();
        }
        DroneForce.relativeForce = new(0,0,Power);
        Debug.Log($"Self Name: {name} || Input Name: {other.tag}");

    }
    private IEnumerator MoveRobotThief()
    {
        while (true)
        {
            // Move from PointA to PointB
            yield return StartCoroutine(MoveToPosition(gameObject, PointA, PointB));

            // Rotate 180 degrees
            yield return StartCoroutine(RotateRobotThief(180f));

            // Move from PointB to PointA
            yield return StartCoroutine(MoveToPosition(gameObject, PointB, PointA));

            // Rotate 180 degrees
            yield return StartCoroutine(RotateRobotThief(180f));
        }
    }

    private IEnumerator MoveToPosition(GameObject obj, Vector3 start, Vector3 end)
    {
        float journeyLength = Vector3.Distance(start, end);
        float startTime = Time.time;

        while (Vector3.Distance(obj.transform.position, end) > 0.01f)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            obj.transform.position = Vector3.Lerp(start, end, fractionOfJourney);

            // Rotate to face the direction of movement
            Vector3 direction = (end - start).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, lookRotation, Time.deltaTime * speed);

            yield return null;
        }

        obj.transform.position = end;
    }

    private IEnumerator RotateRobotThief(float angle)
    {
        Quaternion startRotation = gameObject.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, angle, 0);
        float rotationSpeed = 1f;
        float startTime = Time.time;

        while (Quaternion.Angle(gameObject.transform.rotation, endRotation) > 0.1f)
        {
            float t = (Time.time - startTime) * rotationSpeed;
            gameObject.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        gameObject.transform.rotation = endRotation;
    }
}