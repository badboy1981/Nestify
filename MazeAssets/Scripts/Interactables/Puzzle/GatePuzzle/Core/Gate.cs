using UnityEngine;

namespace GateSystem3
{
    internal class Gate : Interactive
    {
        [SerializeField] GateManagement gateManagement;
        private void Start()
        {
            gateManagement =
                GetComponentInParent<Transform>().
                GetComponentInParent<GatePuzzleManager>().gateManagement;
        }
        //Use in animation Eenent
        private void GateSound()
        {
            PlaySound("OpenGateSound");
        }
        //Use in animation Eenent
        private void OnGateClose()
        {
            gateManagement.gateEvent.OnPushHandle = false;
        }
    }
}