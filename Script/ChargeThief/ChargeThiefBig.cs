using UnityEngine;
using System.Collections;

public class ChargeThiefBig : MonoBehaviour
{
    [SerializeField] Vector3 PointA;
    [SerializeField] Vector3 PointB;
    [SerializeField] GameObject RobotThief;
    [SerializeField] float speed = 1f;
    [SerializeField] Transform ObjectA;
    [SerializeField] Transform ObjectB;

    private void Start()
    {
        PointA = ObjectA.position;
        PointB = ObjectB.position;
        StartCoroutine(MoveRobotThief());
    }

    private IEnumerator MoveRobotThief()
    {
        while (true)
        {
            // Move from PointA to PointB
            yield return StartCoroutine(MoveToPosition(RobotThief, PointA, PointB));

            // Rotate 180 degrees
            yield return StartCoroutine(RotateRobotThief(180f));

            // Move from PointB to PointA
            yield return StartCoroutine(MoveToPosition(RobotThief, PointB, PointA));

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
        Quaternion startRotation = RobotThief.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, angle, 0);
        float rotationSpeed = 1f;
        float startTime = Time.time;

        while (Quaternion.Angle(RobotThief.transform.rotation, endRotation) > 0.1f)
        {
            float t = (Time.time - startTime) * rotationSpeed;
            RobotThief.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        RobotThief.transform.rotation = endRotation;
    }
}