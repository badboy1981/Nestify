using System;
using System.Collections.Generic;

namespace GateSystem3
{
    [Serializable]
    public class ElementPosition
    {
        public string SceneName;
        public string PuzzleName;
        public ElementProperty gate;
        public ElementProperty stoneHatch;
        public List<ElementProperty> keys;
        public List<ElementProperty> gateHandle;
    }
}