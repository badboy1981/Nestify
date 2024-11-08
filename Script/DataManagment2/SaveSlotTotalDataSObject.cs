using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [CreateAssetMenu(fileName = "SlotTotalData", menuName = "Save System/Slot Total Data")]
    public class SaveSlotTotalDataSObject : ScriptableObject
    {
        public Data2.SlotLastGameLevel SlotLastGameLevel;
    }
}
