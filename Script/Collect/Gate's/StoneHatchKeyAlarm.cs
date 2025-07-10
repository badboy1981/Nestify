using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoneHatchKeyAlarm : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform Parent;
    [SerializeField] Collectable.Gate.StoneHatchKeyListRef KeysListRef;
    [SerializeField] string[] _CollectedKey;

    [SerializeField] List<GameObject> Keys;


    private void Start()
    {
        transform.position = Parent.position;
        animator = GetComponent<Animator>();
        animator.StartPlayback();
    }
    private void OnTriggerEnter(Collider other)
    {
        _CollectedKey = CollectedKey();
        if (_CollectedKey != null)
        {
            foreach (var Item in Keys)
            {
                if (!_CollectedKey.Contains(Item.name)) { Item.SetActive(true); }
            }

            animator.StopPlayback();
            //animator.SetBool("LostKey", true);            
            string Msg = $"{name}: Find the lost pieces.";
            //Debug.Log(Msg);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (CollectedKey() != null)
        {
            animator.StartPlayback();
            //animator.SetBool("LostKey", false);            
            foreach (var Item in Keys) { Item.SetActive(false); }
        }
    }
    private string[] CollectedKey()
    {
        return KeysListRef.GatesPropertyList.Find(g => g.SignLabel == name.ElementAt(5).ToString()).keysLists.Where(k => k.Collected).Select(k => k.KeyName).ToArray();
    }

    //private List<string> GetMissingKeys()
    //{
    //    var Ref = Keys.GatesPropertyList.Find(g => g.HatchName == Parent.name).Keys;
    //    var Target = GateActivatorKey.CollectedGateActivatorListKey;
    //    return Ref.Except(Target).ToList();
    //}
}