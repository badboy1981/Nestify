using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StoneHatchKeyAlarm : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform Parent;
    [SerializeField] Collectable.StoneHatchKey Keys;
    [SerializeField] SaveSystem.SaveLevelDataSObject GateActivatorKey;
    [SerializeField] GameObject KeyA;
    [SerializeField] GameObject KeyB;
    [SerializeField] GameObject KeyC;

    private void Start()
    {
        transform.position = Parent.position;
        animator.StopPlayback();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GetMissingKeys().Count() > 0)
        {            
            KeyA.SetActive(true);
            KeyB.SetActive(true);
            KeyC.SetActive(true);
            animator.StartPlayback();
            animator = GetComponent<Animator>();
            animator.SetBool("LostKey", true);
            string Msg = "Find the lost pieces.";
            Debug.Log(Msg);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (GetMissingKeys() != null)
        {
            animator.StopPlayback();
            animator.SetBool("LostKey", false);
            KeyA.SetActive(false);
            KeyB.SetActive(false);
            KeyC.SetActive(false);
        }
    }
    private List<string> GetMissingKeys()
    {
        var Ref = Keys.Gates.Find(g => g.HatchName == Parent.name).Keys;
        var Target = GateActivatorKey.CollectedGateActivatorListKey;
        return Ref.Except(Target).ToList();
    }
}