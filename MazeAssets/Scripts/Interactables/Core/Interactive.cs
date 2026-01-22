using UnityEngine;
using Assets.MazeAssets.Scripts.Parent;

internal class Interactive : Parent
{
    protected bool isActive;

    protected override void Awake()
    {
        base.Awake();
        isActive = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        //if (other.CompareTag("Player") && !isActive)
        //{
        //    Debug.Log($"Interactive: Player entered {gameObject.name}");
        //    isActive = true;
        //    //PlaySound("Activate");
        //}
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        //if (other.CompareTag("Player") && isActive)
        //{
        //    Debug.Log($"Interactive: Player exited {gameObject.name}");
        //    isActive = false;
        //    //StopSound("Activate");
        //}
    }
}