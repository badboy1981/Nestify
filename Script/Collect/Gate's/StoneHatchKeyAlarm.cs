using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoneHatchKeyAlarm : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform Parent;
    [SerializeField] Collectable.Gate.StoneHatchKeyListRef Keys;
    [SerializeField] SaveSystem.SaveLevelDataSObject GateActivatorKey;
    [SerializeField] GameObject KeyA;
    [SerializeField] GameObject KeyB;
    [SerializeField] GameObject KeyC;

    private void Start()
    {
        transform.position = Parent.position;
        animator = GetComponent<Animator>();
        animator.StartPlayback();
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (GetMissingKeys().Count() > 0)
        //{
        //    KeyA.SetActive(true);
        //    KeyB.SetActive(true);
        //    KeyC.SetActive(true);

        //    animator.StopPlayback();
        //    //animator.SetBool("LostKey", true);            
        //    string Msg = $"{name}: Find the lost pieces.";
        //    //Debug.Log(Msg);
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        //if (GetMissingKeys() != null)
        //{
        //    animator.StartPlayback();
        //    //animator.SetBool("LostKey", false);            
        //    KeyA.SetActive(false);
        //    KeyB.SetActive(false);
        //    KeyC.SetActive(false);
        //}
    }

    //private List<string> GetMissingKeys()
    //{
    //    var Ref = Keys.GatesPropertyList.Find(g => g.HatchName == Parent.name).Keys;
    //    var Target = GateActivatorKey.CollectedGateActivatorListKey;
    //    return Ref.Except(Target).ToList();
    //}
}