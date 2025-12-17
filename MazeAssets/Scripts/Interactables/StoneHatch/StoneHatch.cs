using System;
using System.Collections.Generic;
using System.Linq;
using Collectable.Gate;
using UnityEngine;

internal class StoneHatch : Interactive
{
    [Header("GateProperties")]
    [SerializeField] GatePropertyGroup gatePropertyGroup;

    [Header("Gate")]
    [SerializeField] GateProperty gateProperty;

    [Header("Key's List")]
    [SerializeField] List<GameObject> Keys;
    //[SerializeField] string[] _CollectedKey;

    [Header("Animation")]
    [SerializeField] Animator animator;

    private void Start()
    {
        gateProperty = FindGateProperty.GetGateProperty(gatePropertyGroup, name);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!gateProperty.GateState)
        {
            animator = GetComponent<Animator>();
            ActiveCollectedKeys();
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopSound("NeedKey");
        }
    }
    private void ActiveCollectedKeys()
    {
        string[] _CollectedKey = CollectedKey();
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
            PlaySound("HatchSound");
            ActiveGateHandle(true);
        }
        else
        {
            PlaySound("NeedKey");
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