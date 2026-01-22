using UnityEngine;

namespace GateSystem3
{
    internal class FillGateManagement : MonoBehaviour
    {
        [SerializeField] GateManagement gateManagement;
        private void Start()
        {
            gateManagement = GetComponentInParent<GatePuzzleManager>().gateManagement;
        }
    }
}