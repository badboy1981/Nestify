using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Collectable.Gate;
using UnityEngine;

public class StoneHatch : MonoBehaviour
{
    [Header("Gate")]
    [SerializeField] Transform Gate;
    [SerializeField] GateProperty gateProperty;

    [Header("Key's List")]
    [SerializeField] List<GameObject> Keys;
    private string[] _CollectedKey;

    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] float GateOpen = 5;
    [SerializeField] float GateOpenDuration = 3f;

    //private readonly string DroneName = "Drone";

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        //if (other.name == DroneName)
        {
            animator = GetComponent<Animator>();
            _CollectedKey = CollectedKey();

            ActiveCollectedKeys();  
        }
    }

    private void ActiveCollectedKeys()
    {
        foreach (var Item in Keys)
        {
            if (_CollectedKey.Contains(Item.name))
            {
                Item.SetActive(true);
            }
        }
        if (_CollectedKey.Length == 3)
        {
            animator.SetBool("ActiveKey", true);
            ActiveGateHandle(true);
        }
        else
        {
            ActiveGateHandle(false);
        }
    }
    private string[] CollectedKey()
    {
        return gateProperty.keysLists.Where(g => g.Collected).Select(g => g.KeyName).ToArray();
    }
    private void ActiveGateHandle(bool State)
    {
        gateProperty.GateState = State;
    }
    private IEnumerator HatchKeyAnimate(GameObject Key)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 2.5f)
        {
            Key.SetActive(true);
            elapsedTime += Time.deltaTime;
            Debug.Log(elapsedTime);
            yield return null;
        }
    }

    private IEnumerator OpenGate(bool GateState)
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
    private enum TargetState
    {
        Open = 1,
        Close = -1
    }
    private void GateState(TargetState tState)
    {
        switch (tState)
        {
            case TargetState.Open:
                Debug.Log($"State One: {tState}");
                break;
            case TargetState.Close:
                Debug.Log($"State Two: {tState}");
                break;
        }
    }
    private void Open()
    {
        GateState(TargetState.Open);
    }
    private void Close()
    {
        GateState(TargetState.Close);
    }
}