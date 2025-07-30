using CellMap;
using UnityEngine;

namespace TelePort
{
    public class ManageVoltBeacon : MonoBehaviour
    {
        [Header("Volt Beacon")]
        [SerializeField] MazeMapSB CellProperty;
        [SerializeField] PointProperty TeleportPositionData;

        [Header("Animation")]
        [SerializeField] Animator CreateBeaconAnimattion;

        private void OnTriggerEnter(Collider other)
        {
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