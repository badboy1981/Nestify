using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class StoneHatch : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] float GateOpen = 5;
    [SerializeField] float GateOpenDuration = 3f;

    [Header("Key's List")]
    [SerializeField] Collectable.Gate.StoneHatchKeyListRef KeysListRef;
    [SerializeField] List<GameObject> Keys;
    //[SerializeField] SaveSystem.SaveLevelDataSObject GateActivatorKey;
    //[SerializeField] MeshFilter KeyMesh;
    //[SerializeField] Mesh _Mesh;
    [SerializeField] string[] _CollectedKey;

    [Header("Gate")]
    [SerializeField] string GateSign;
    [SerializeField] Transform Gate;
     
        private readonly string DroneName = "Drone";

    //public event UnityAction<bool> ActiveGateHandleEvent = delegate { };
    //public void GateHandelEvent(bool Handel)
    //{
    //    ActiveGateHandleEvent?.Invoke(Handel);
    //}

    private void Start()
    {
        GateSign = name.ElementAt(5).ToString();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.name == DroneName)
        {
            animator = GetComponent<Animator>();
            _CollectedKey = CollectedKey();
            foreach (var Item in Keys)
            {
                if (_CollectedKey.Contains(Item.name))
                {
                    Item.SetActive(true);
                    //StartCoroutine(HatchKeyAnimate(Item));
                    //HatchKeyAnimate(Item);
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
    }
    private string[] CollectedKey()
    {
        return KeysListRef.GatesPropertyList.Find(g => g.SignLabel == GateSign).keysLists.Where(k => k.Collected).Select(k => k.KeyName).ToArray();
    }
    private void ActiveGateHandle(bool State)
    {
        KeysListRef.GatesPropertyList.Find(g => g.SignLabel == GateSign).ActiveGateHandleState = State;
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
    //private void TriggerEnterRoleB()
    //{
    //    KeysList.GatesPropertyList.Find(g => g.HatchName == name).keysLists.Find(g => g.KeyName == "").Collected = true;
    //}
    //private void TriggerEnterRoleA()
    //{
    //    Debug.Log($"Collected Keys: {string.Join(',', CollectedKey())}");
    //    var MissingKey = GetMissingKeys();
    //    if (MissingKey.Count > 0)
    //    {
    //        animator = GetComponent<Animator>();
    //        string Msg = $"Find The Lost pieces for {name}: {string.Join(',', MissingKey)}";
    //        Debug.Log(Msg);
    //    }
    //    else
    //    {
    //        animator.SetBool("OpenGate", true);
    //    }
    //}

    //private List<string> CollectedKey()
    //{
    //    var Kkeys = KeysList.GatesPropertyList.Find(g => g.HatchName == name).keysLists.Find(k => k.Collected == true);
    //    var keys = KeysList.GatesPropertyList.Find(g => g.HatchName == name).keysLists;
    //    //var CollectedKey = keys.Find(k => k.Collected == true).KeyName;
    //    var KeysCollected = new List<string>();
    //    foreach (var key in keys)
    //    {
    //        if (key.Collected == true) KeysCollected.Add(key.KeyName);
    //    }
    //    return KeysCollected;
    //}
    //private List<string> GetMissingKeys()
    //{
    //    //var Ref = KeysList.GatesPropertyList.Find(g => g.HatchName == name).Keys;
    //    //var Target = GateActivatorKey.CollectedGateActivatorListKey;
    //    //return Ref.Except(Target).ToList();
    //}

    //private bool CheckActivatorKeyList(SaveSystem.SaveLevelDataSObject gateActivatorKey)
    //{
    //    //return gate.Keys.All(Key => Keys.Gates.Contains(Key));
    //    //return GateActivatorKey.GateKeyActivatorList.All(key => gate.Keys.Contains(key));
    //    //return GateActivatorKey.GateKeyActivatorList.All(key => gateActivatorKey.GateKeyActivatorList.Contains(key));
    //    //return KeysList.GatesPropertyList.All(key => GateActivatorKey.CollectedGateActivatorListKey.Contains(KeysList.GatesPropertyList[0].Keys[0]));
    //}
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
    private void GateState(TargetState ds)
    {
        switch (ds)
        {
            case TargetState.Open:
                Debug.Log($"State One: {ds}");
                break;
            case TargetState.Close:
                Debug.Log($"State Two: {ds}");
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
    private void Test()
    {
        //TestSpace.ListTest ts = new();
        //ts.Ls.Add("One");
        //ts.Ls.Add("Two");
        //ts.Ls.Add("Three");

        //Debug.Log(ts.Ls.IndexOf("One"));
        //Debug.Log(JsonUtility.ToJson(Keys.Gates[0]));
        //Debug.Log(Keys.Gates.Find(g => g.TargetGateName == "GateB"));
    }
}