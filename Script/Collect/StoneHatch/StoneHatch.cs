using System;
using System.Collections.Generic;
using System.Linq;
using Collectable.Gate;
using UnityEngine;

public class StoneHatch : MonoBehaviour
{
    [Header("GateProperties")]
    [SerializeField] GatePropertyGroup gatePropertyGroup;

    [Header("Gate")]
    [SerializeField] GateProperty gateProperty;

    [Header("Key's List")]
    [SerializeField] List<GameObject> Keys;
    [SerializeField] string[] _CollectedKey;

    [Header("Animation")]
    [SerializeField] Animator animator;

    private void Start()
    {
        gateProperty = FindGateProperty.GetGateProperty(gatePropertyGroup, name);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
}