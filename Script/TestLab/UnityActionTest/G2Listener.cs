using UnityEngine;

public class G2Listener : MonoBehaviour
{
    public BoolData data;

    void OnEnable()
    {
        data.onChangedToTrue += OnTrue;
        data.onChangedToFalse += OnFalse;
    }

    void OnDisable()
    {
        data.onChangedToTrue -= OnTrue;
        data.onChangedToFalse -= OnFalse;
    }

    void Respond()
    {
        Debug.Log($"G2 = {data._value}");        
    }
    void OnTrue()
    {
        Debug.Log("G2 = true");
    }

    void OnFalse()
    {
        Debug.Log("G2 = false");
    }
}