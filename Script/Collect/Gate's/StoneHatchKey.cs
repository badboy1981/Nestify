using System.Collections.Generic;
using UnityEngine;


namespace Collectable
{
    [System.Serializable]
    public class Gate
    {
        public string HatchName;
        public string TargetGateName;
        public List<string> Keys = new();
    }

    [CreateAssetMenu(fileName = "StoneHatchKey", menuName = "My Asset/Stone Hatch Key")]
    public class StoneHatchKey : ScriptableObject
    {
        public List<Gate> Gates = new();

        //private StoneHatchKey FillStoneHatchKey()
        //{
        //    Gate GateA = new()
        //    {
        //        TargetGateName = "GateA",
        //        HatchName = "HatchA",
        //        Keys = new() { "KeyAA", "KeyAB", "KeyAC" }
        //    };
        //    Gate GateB = new()
        //    {
        //        TargetGateName = "GateB",
        //        HatchName = "HatchB",
        //        Keys = new() { "KeyBA", "KeyBB", "KeyBC" }
        //    };
        //    Gate GateC = new()
        //    {
        //        TargetGateName = "GateC",
        //        HatchName = "HatchC",
        //        Keys = new() { "KeyCA", "KeyCB", "KeyCC" }
        //    };

        //    StoneHatchKey Hatch = new();
        //    Hatch.Gates.Add(GateA);
        //    Hatch.Gates.Add(GateB);
        //    Hatch.Gates.Add(GateC);
        //    return Hatch;
        //}
    }
}