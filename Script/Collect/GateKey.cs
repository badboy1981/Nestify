using UnityEngine;
using System.Collections;

public class GateKey : MonoBehaviour
{
    [SerializeField] Transform Gate;
    [SerializeField] float GateOpen;
    [SerializeField] float GateOpenDuration = 3f;
    private float initialGateY;

    private void Start()
    {
        initialGateY = Gate.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(OpenGate());
    }

    private IEnumerator OpenGate()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = Gate.position;
        Vector3 targetPosition = new(Gate.position.x, Gate.position.y + GateOpen, Gate.position.z);

        while (elapsedTime < GateOpenDuration)
        {
            Gate.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / GateOpenDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Gate.position = targetPosition;
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(CloseGate());
    }

    private IEnumerator CloseGate()
    {
        yield return new WaitForSeconds(2f);

        float elapsedTime = 0f;
        Vector3 initialPosition = Gate.position;
        Vector3 targetPosition = new(initialPosition.x, initialGateY, initialPosition.z);

        while (elapsedTime < GateOpenDuration)
        {
            Gate.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / GateOpenDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Gate.position = targetPosition;
    }
}