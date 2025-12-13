using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class StoneHatch2 : MonoBehaviour
{
    [SerializeField] GateManagment gateManagment;
    [SerializeField] KeyManagment keyManagment;
    [SerializeField] Animator animator;
    [SerializeField] List<GameObject> KeysList;
    [SerializeField] List<GameEntityProperty> NeedKeyslist;

    private Dictionary<string, bool> KeyStatus = new();

    private bool keyCheck = false;

    private event UnityAction<List<GameEntityProperty>> OnAllKeysCollectedEvent;
    private event UnityAction<List<GameEntityProperty>> OnKeysMissingEvent;

    private void Start()
    {
        foreach (var key in KeysList)
        {
            KeyStatus.Add(key.name, false);
        }
        foreach (var key in KeysList)
        {
            key.SetActive(false);
        }
        //KeyStatus[KeyStatus.ElementAt(0).Key] = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        animator = GetComponent<Animator>();
        for (int i = 0; i < gateManagment.KeyCollectedCounter; i++)
        {
            //KeyStatus[KeyStatus.ElementAt(i).Key] = true;
            KeysList[i].SetActive(true);
        }

        if (gateManagment.KeyCollectedCounter >= 3)
        {            
            animator.SetBool("ActiveKey", true);
        }
        else
        {
            foreach (var key in KeysList)
            {
                key.SetActive(true);
            }
            animator.SetBool("MissingKey", true);
        }
        //if (!gateManagment.AllKeyCollected) return;
        //keyCheck = true;
        //animator = GetComponent<Animator>();
        ////animator.SetBool("MissingKey", true);
        //if(gateManagment.AllKeyCollected)
        //{
        //    //OnAllKeysCollectedEvent?.Invoke(gateManagment.gateConfig.RequiredKeys);
        //}
        //else
        //{
        //    OnKeysMissingEvent?.Invoke(gateManagment.gateConfig.RequiredKeys);
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        //if (gateManagment.KeyCollectedCounter >= 3) return;// || gateManagment.KeyCollectedCounter <= 0) return;
        for (int i = 2 - gateManagment.KeyCollectedCounter; i >= 0; i--)
        {
            Debug.Log($"i={i}");
            KeysList[i].SetActive(false);
        }
        //keyCheck = false;
        //foreach (var key in KeysList)
        //{
        //    key.SetActive(false);
        //}
        animator.SetBool("MissingKey", false);
    }
    private void OnEnable()
    {
        OnAllKeysCollectedEvent += OnAllKeysCollected;
        OnKeysMissingEvent += OnKeysMissing;
    }
    private void OnDisable()
    {
        OnAllKeysCollectedEvent -= OnAllKeysCollected;
        OnKeysMissingEvent -= OnKeysMissing;
    }
    private void OnAllKeysCollected(List<GameEntityProperty> property)
    {
        animator.SetBool("AllKeysCollected", true);
    }
    private void OnKeysMissing(List<GameEntityProperty> keyList)
    {
        foreach(var key in keyList)
        {
         
        }
        animator.SetBool("MissingKey", keyCheck);
    }
}
    //private void CheckCollectedKey()
    //{       
    //    KeyStatus = NeedKeyslist.ToDictionary(key => key, key => false);
    //    foreach (var key in NeedKeyslist)
    //    {
    //        if (keyManagment.collectedKeyIDs.Contains(key))
    //            KeyStatus[key] = true;
    //    }
    //}
    //private void NeedKeyList()
    //{
    //    var Sign = new string[3] { "A", "B", "C" };
    //    NeedKeyslist.Clear();
    //    foreach (string item in Sign)
    //    {
    //        NeedKeyslist.Add(name[0] + item);
    //    }
    //    CreateKeyList = false;
    //}
//}