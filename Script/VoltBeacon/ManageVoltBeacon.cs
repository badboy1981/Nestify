using CellMap;
using UnityEngine;

namespace TelePort
{
    public class ManageVoltBeacon : MonoBehaviour
    {
        [Header("Volt Beacon")]
        [SerializeField] MazeMapSB CellProperty;
        [SerializeField] PointProperty TeleportPositionData;
        [SerializeField] float Power = -1000f;


        //[Header("Animation")]
        //[SerializeField] Animator CreateBeaconAnimation;

        private void OnTriggerEnter(Collider other)
        {
            var Push = other.GetComponent<ConstantForce>();
            Push.relativeForce = new Vector3(0, 0, Power);
            gameObject.SetActive(false);            
        }
        public void CreateBeacon()
        {
            if (!gameObject.activeSelf)
            {
                transform.position = CellProperty.MazeCellProperty.CellPosition;
                TeleportPositionData.Position = CellProperty.MazeCellProperty.CellPosition;
                gameObject.SetActive(true);
                Debug.Log("VoltBeacon Initialized. Anchoring complete.");
            }
            else
            {
                Debug.Log("Warning: VoltBeacon already active. Duplicate registration blocked.");
            }
        }
    }
}

//🛰️ ⚡ VoltBeacon & Portal System Alerts ⚡ 🛰️
//- VoltBeacon Initialized. Anchoring complete.
//- Warning: VoltBeacon already active. Duplicate registration blocked.
//- Portal established. Return channel to VoltBeacon activated.
//- Warning: Portal already deployed. Duplicate creation blocked.
//- Error: Portal range exceeded. Return function disabled.