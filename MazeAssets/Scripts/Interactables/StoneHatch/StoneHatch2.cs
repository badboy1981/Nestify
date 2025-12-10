using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoneHatch2 : MonoBehaviour
{
    [SerializeField] GateManagment gateManagment;
    [SerializeField] KeyManagment keyManagment;
    //[SerializeField] List<string> NeedKeyslist;
    //[SerializeField] List<string> collectedKeys;

    //[SerializeField] Animator animator;

    //private Dictionary<string, bool> KeyStatus = new();
    //private bool CreateKeyList = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        //gateManagment.UpdateKeyStatus(keyManagment.collectedKeyIDs);
        //if (CreateKeyList) NeedKeyList();
        //CheckCollectedKey();

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
}