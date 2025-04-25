using UnityEngine;
using System.Collections;

public class GateKey : MonoBehaviour
{
    [Header("Gate")]
    [SerializeField] Transform Gate;
    [SerializeField] Transform KeyHandle;
    [SerializeField] float GateOpen=5;
    [SerializeField] float GateOpenDuration = 3f;

    [Header("Key Handle")]
    [SerializeField] Vector3 KeyHandleRotation;
    private Quaternion initialRotation; 
    private Quaternion targetRotation;

    private float initialGateY;

    private void Start()
    {
        initialRotation = Quaternion.Euler(-50f, -90f, 90f);
        targetRotation = Quaternion.Euler(-140, -90f, 90f);
        initialGateY = Gate.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        //StartCoroutine(RotateHandleKey(KeyHandleRotation));
        StartCoroutine(RotateHandleKey(90f));
        StartCoroutine(OpenGate());
    }
    private void OnTriggerExit(Collider other)
    {
        //StartCoroutine(RotateHandleKey(KeyHandleRotation * -1));
        StartCoroutine(RotateHandleKey(-90f));
        StartCoroutine(CloseGate());
    }
    private IEnumerator RotateHandleKey(float degreesY)
    {
        float elapsedTime = 0f;
        Quaternion initialRotation = KeyHandle.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0, degreesY, 0); 

        while (elapsedTime < GateOpenDuration)
        {
            KeyHandle.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / GateOpenDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        KeyHandle.rotation = targetRotation;
    }
    //private IEnumerator RotateHandleKey(Vector3 Deg)
    //{
    //    float elapsedTime = 0f;
    //    Quaternion initialRotation = KeyHandle.rotation;
    //    Quaternion targetRotation = Quaternion.Euler(KeyHandle.rotation.x + Deg.x, KeyHandle.rotation.y + Deg.y, KeyHandle.rotation.z + Deg.z);
    //    while (elapsedTime < GateOpenDuration)
    //    {
    //        KeyHandle.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / GateOpenDuration);
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //    KeyHandle.rotation = targetRotation;
    //}
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