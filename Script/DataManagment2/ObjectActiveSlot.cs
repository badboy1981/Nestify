using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [CreateAssetMenu(fileName = "ActiveSlot", menuName = "Save System/Active Slot")]
    public class ObjectActiveSlot : ScriptableObject
    {
        public string ActiveSlot;
    }
}