using System.Collections.Generic;

namespace Collectable.Gate
{
    [System.Serializable]
    public class GateProperty
    {
        public string SignLabel;
        public string HatchName;
        public string TargetGateName;
        public List<KeysList> keysLists = new();
    }

    [System.Serializable]
    public class KeysList
    {
        public string KeyName;
        public bool Collected;
    }
}