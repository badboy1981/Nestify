using UnityEngine;

public class GateKeyGetEventListener : MonoBehaviour
{
    [SerializeField] KeyManagment keyManagment;
    private void OnEnable()
    {
        keyManagment.keyGetEvent.OnKeyGet += OnKeyGet;
    }
    private void OnDisable()
    {
        keyManagment.keyGetEvent.OnKeyGet -= OnKeyGet;
    }
    private void OnKeyGet(KeyProperty keyProperty)
    {
        if (!keyManagment.collectedKeyIDs.Contains(keyProperty.ID))
            keyManagment.collectedKeyIDs.Add(keyProperty.ID);
    }
}