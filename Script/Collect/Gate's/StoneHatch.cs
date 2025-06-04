using System.Collections;
using UnityEngine;

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
    public void OnTriggerEnter(Collider other)
    {
        if (other.name == DroneName)
        {
            animator = GetComponent<Animator>();
            animator.SetBool("OpenGate", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == DroneName)
        {
            animator.SetBool("OpenGate", false);
        }
    }
    private Collectable.StoneHatchKey FillStoneHatchKey()
    {
        Collectable.Gate GateA = new()
        {
            TargetGateName = "GateA",
            HatchName = "HatchA",
            Keys = new() { "KeyAA", "KeyAB", "KeyAC" }
        };
        Collectable.Gate GateB = new()
        {
            TargetGateName = "GateB",
            HatchName = "HatchB",
            Keys = new() { "KeyBA", "KeyBB", "KeyBC" }
        };
        Collectable.Gate GateC = new()
        {
            TargetGateName = "GateC",
            HatchName = "HatchC",
            Keys = new() { "KeyCA", "KeyCB", "KeyCC" }
        };

        Collectable.StoneHatchKey Hatch = new();
        Hatch.Gates.Add(GateA);
        Hatch.Gates.Add(GateB);
        Hatch.Gates.Add(GateC);
        return Hatch;
    }
    private bool CheckActivatorKeyList()
    {
        return true;
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