using UnityEngine;

public class G1Setter : MonoBehaviour
{
    public BoolData data;

    public void UnlockGate(bool state)
    {
        data.Value = state;
    }
    private void OnTriggerEnter(Collider other)
    {
        UnlockGate(true);
    }
    private void OnTriggerExit(Collider other)
    {
        UnlockGate(false);
    }
}
