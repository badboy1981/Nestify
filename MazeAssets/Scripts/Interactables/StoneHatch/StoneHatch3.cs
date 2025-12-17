using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
internal class StoneHatch3 : Interactive
{
    [SerializeField] GateManagment gateManagment;
    [SerializeField] Animator animator;
    //[SerializeField] List<GameObject> KeysList;

    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        animator = GetComponent<Animator>();
  
        if (gateManagment.KeyCollectedCounter >= 3)
        {
            PlaySound("HatchSound");
            animator.SetBool("ActiveKey", true);
        }
        else
        {
            PlaySound("NeedKey");
            animator.SetBool("MissingKey", true);
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        //if (gateManagment.KeyCollectedCounter == 0) return;
        if (gateManagment.KeyCollectedCounter < 3)
        {
            //for (int i = 0; i < 3 - gateManagment.KeyCollectedCounter; i++)
            //{
            //    KeysList[i].SetActive(false);
            //}
            animator.SetBool("MissingKey", false);
        }
    }
}