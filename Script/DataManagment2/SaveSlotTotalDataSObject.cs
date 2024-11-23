using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [CreateAssetMenu(fileName = "SlotTotalData", menuName = "Save System/Slot Total Data")]
    public class SaveSlotTotalDataSObject : ScriptableObject
    {
        //public List<Data2.SlotUnlockedLevelList> SlotList;
        public List<Data2.UnlockLevel> UnlockLevels;
        
    }
}