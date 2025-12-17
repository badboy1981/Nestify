using System.Collections.Generic;
using UnityEngine;

public class GateManagmentReset : MonoBehaviour
{
    [SerializeField] List<GateManagment> gateManagments;

    private void Start()
    {
        foreach (var item in gateManagments)
        {
            item.AllKeyCollected = false;
            item.KeyCollectedCounter = 0;
            item.gateConfig.RequiredKeys.Clear();
        }
    }
}