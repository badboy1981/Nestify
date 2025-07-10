using System.Linq;
using Collectable.Gate;
using UnityEngine;

public class GateHandle2 : MonoBehaviour
{
    [SerializeField] StoneHatchKeyListRef GateData;
    //[SerializeField] StoneHatch GateHandel;
    [SerializeField] string SignLabel;

    [SerializeField] GateProperty TargetGate;

    private void Start()
    {
        SignLabel = name.ElementAt(0).ToString();
    }

    private void OnEnable()
    {
        TargetGate = GateData.GatesPropertyList.Find(g => g.SignLabel == SignLabel);
        TargetGate?.OnStateChangedEvent.AddListener(HandleStateChange);
    }
    private void OnDisable()
    {
        TargetGate?.OnStateChangedEvent.RemoveListener(HandleStateChange);
    }
    private void HandleStateChange(bool newState)
    {
        Debug.Log($"Current State {TargetGate.TargetGateName} To {newState} Change!");
    }
}