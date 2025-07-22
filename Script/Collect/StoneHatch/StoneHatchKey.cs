using System.Collections.Generic;
using UnityEngine;

namespace Collectable.Gate
{
    [CreateAssetMenu(fileName = "StoneHatchKey", menuName = "My Asset/Stone Hatch Key")]
    public class StoneHatchKeyListRef : ScriptableObject
    {
        public List<GateProperty> GatesPropertyList = new();
    }
}