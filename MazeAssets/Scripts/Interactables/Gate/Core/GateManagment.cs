using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GateManagment", menuName = "Gate/Gate Managment")]
public class GateManagment : ScriptableObject
{
    //public KeyCollectedEvent keyCollectedEvent;
    public GateConfig gateConfig;
    public bool AllKeyCollected;
    public int KeyCollectedCounter;

    private UnityAction<List<GameEntityProperty>> OnActiveHandle;

    public void UpdateKeyStatus(string KeyName)
    {
        //gateConfig.RequiredKeys.Find(s => s.UniqueID == KeyName).IsCollected = true;
        AllKeyCollected = AllKeysCollected();
        //if (!AllKeyCollected) return;
        //OnActiveHandle?.Invoke(gateConfig.handle);
    }

    private bool AllKeysCollected()
    {
        // Returns true only if every key in RequiredKeys has IsCollected = true
        return gateConfig.RequiredKeys != null && gateConfig.RequiredKeys.All(k => k.IsCollected);
    }
    //private void OnEnable()
    //{
    //    OnActiveHandle += ActiveHandle;
    //}
    //private void OnDisable()
    //{
    //    OnActiveHandle -= ActiveHandle;
    //}
    //private void ActiveHandle(List<GameEntityProperty> HandleList)
    //{
    //    List<string> handleIDs = null;
    //    //handleIDs.Clear();
    //    foreach (var item in HandleList)
    //    {
    //        handleIDs.Add(item.UniqueID);
    //    }
    //}
}



//public GameObject Gate;
//public StoneHatch2 StoneHatch;
//[SerializeField] List<string> GateKeysNeed;
//    private Dictionary<string, bool> KeyStatus = new();

//    public void InitializeKeys()
//    {
//        //KeyStatus = GateKeysNeed.ToDictionary(key => key, key => false);


//    }
//    public void UpdateSingleKeyStatus(string KeyName)
//    {
//        if(KeyStatus.ContainsKey(KeyName))
//        {           
//            KeyStatus[KeyName] = true;
//        }
//    }
//    public void UpdateKeyStatus(List<string> collectedKeysName)
//    {
//        foreach (var key in collectedKeysName)
//        {
//            if (KeyStatus.ContainsKey(key))
//            {
//                KeyStatus[key] = true;
//            }
//        }
//    }
//    public void PrintKeyStatus()
//    {
//        //Debug.Log("Current Key Status:");
//        foreach (var kvp in KeyStatus)
//        {
//            Debug.Log($"Name: {kvp.Key} => Value: {kvp.Value}");
//        }
//    }
//}