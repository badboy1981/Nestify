using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StoneHatch : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] float GateOpen = 5;
    [SerializeField] float GateOpenDuration = 3f;

    [Header("Key's List")]
    [SerializeField] Collectable.StoneHatchKey Keys;
    [SerializeField] SaveSystem.SaveLevelDataSObject GateActivatorKey;
    //[SerializeField] MeshFilter KeyMesh;
    //[SerializeField] Mesh _Mesh;

    [Header("Gate")]
    [SerializeField] Transform Gate;

    private readonly string DroneName = "Drone";

    private void Start()
    {
        TestSpace.ListTest ts = new();
        ts.Ls.Add("One");
        ts.Ls.Add("Two");
        ts.Ls.Add("Three");

        Debug.Log(ts.Ls.IndexOf("One"));
        Debug.Log(JsonUtility.ToJson(Keys.Gates[0]));
        Debug.Log(Keys.Gates.Find(g => g.TargetGateName == "GateB"));

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.name == DroneName)
        {
            if (GetMissingKeys().Count > 0)
            {
                animator = GetComponent<Animator>();
                Debug.Log($"Lost pieces: {string.Join(',', GetMissingKeys())}");
            }
            else
            {
                animator.SetBool("OpenGate", true);
            }
        }
    }
    private List<string> GetMissingKeys()
    {
        var Ref = Keys.Gates.Find(g => g.HatchName == name).Keys;
        var Target = GateActivatorKey.CollectedGateActivatorListKey;
        return Ref.Except(Target).ToList();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == DroneName)
        {
            animator.SetBool("OpenGate", false);
        }
    }

    private bool CheckActivatorKeyList(SaveSystem.SaveLevelDataSObject gateActivatorKey)
    {
        //return gate.Keys.All(Key => Keys.Gates.Contains(Key));
        //return GateActivatorKey.GateKeyActivatorList.All(key => gate.Keys.Contains(key));
        //return GateActivatorKey.GateKeyActivatorList.All(key => gateActivatorKey.GateKeyActivatorList.Contains(key));
        return Keys.Gates.All(key => GateActivatorKey.CollectedGateActivatorListKey.Contains(Keys.Gates[0].Keys[0]));
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
    //private void test()
    private enum TargetState
    {
        Open = 1,
        Close = -1
    }
}