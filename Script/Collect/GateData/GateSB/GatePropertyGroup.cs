using System.Collections.Generic;
using Collectable.Gate;
using UnityEngine;

namespace Collectable.Gate
{
    [CreateAssetMenu(fileName = "GatePropertyGroup", menuName = "Gate/Gate Property Group")]
    public class GatePropertyGroup : ScriptableObject
    {
        public List<GateProperty> gateProperties;
    }
}