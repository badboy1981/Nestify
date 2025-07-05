using System;
using System.Collections.Generic;
using Collectable.Gate;
using UnityEngine;

namespace Assets.Script.Collect.Gate_s
{
    public class FillStoneHatchKey : MonoBehaviour
    {
        [SerializeField] StoneHatchKeyListRef ListRef;
        //[SerializeField] List<GateProperty> ListGateProperty;
        //[SerializeField] List<string> Test;
        private void Start()
        {
            FillAABBCC();
        }
        private void FillAABBCC()
        {
            ListRef.GatesPropertyList.Clear();
            ListRef.GatesPropertyList.Add(gatePropertyA());
            ListRef.GatesPropertyList.Add(gatePropertyB());
            ListRef.GatesPropertyList.Add(gatePropertyC());
        }
        private GateProperty gatePropertyA()
        {
            var KlA = new KeysList() { KeyName = "KeyAA", Collected = false };
            var KlB = new KeysList() { KeyName = "KeyAB", Collected = false };
            var KlC = new KeysList() { KeyName = "KeyAC", Collected = false };
            return new()
            {
                SignLabel = "A",
                HatchName = "HatchA",
                TargetGateName = "GateA",
                //Keys = new List<string>() { "KeyAA", "KeyAB", "KeyAC" },
                keysLists = new List<KeysList>() { KlA, KlB, KlC }
            };
        }
        private GateProperty gatePropertyB()
        {
            var KlA = new KeysList() { KeyName = "KeyBA", Collected = false };
            var KlB = new KeysList() { KeyName = "KeyBB", Collected = false };
            var KlC = new KeysList() { KeyName = "KeyBC", Collected = false };
            return new()
            {
                SignLabel = "B",
                HatchName = "HatchB",
                TargetGateName = "GateB",
                //Keys = new List<string>() { "KeyBA", "KeyBB", "KeyBC" },
                keysLists = new List<KeysList>() { KlA, KlB, KlC }
            };
        }
        private GateProperty gatePropertyC()
        {
            var KlA = new KeysList() { KeyName = "KeyCA", Collected = false };
            var KlB = new KeysList() { KeyName = "KeyCB", Collected = false };
            var KlC = new KeysList() { KeyName = "KeyCC", Collected = false };
            return new()
            {
                SignLabel = "C",
                HatchName = "HatchC",
                TargetGateName = "GateC",
                //Keys = new List<string>() { "KeyCA", "KeyCB", "KeyCC" },
                keysLists = new List<KeysList>() { KlA, KlB, KlC }
            };
        }
    }
}
        //private StoneHatchKeyListRef listRefs()
        //{
        //    var KlA = new KeysList() { KeyName = "KeyAA", Collected = false };
        //    var KlB = new KeysList() { KeyName = "KeyAB", Collected = false };
        //    var KlC = new KeysList() { KeyName = "KeyAC", Collected = false };
        //    GateProperty GtpA = new()
        //    {
        //        SignLabel = "A",
        //        HatchName = "HatchA",
        //        TargetGateName = "GateA",
        //        Keys = new List<string>() { "KeyAA", "KeyAB", "KeyAC" },
        //        keysLists = new List<KeysList>() { KlA, KlB, KlC }
        //    };
        //    StoneHatchKeyListRef li = new();
        //    li.GatesPropertyList.Add(GtpA);
        //    return li;
        //}
        //private void FillRef()
        //{
        //    string[] Sign = { "A", "B", "C" };
        //    string[] Name = { "Hatch", "Gate", "Key" };
        //    var KeyList = new KeysList();
        //    GateProperty Gtp = new();

        //    //StoneHatchKeyListRef ListRef = new();

        //    for (int i = 0; i > Sign.Length; i++)
        //    {
        //        Gtp.SignLabel = Sign[i];
        //        Gtp.HatchName = Name[0] + Sign[i];
        //        Gtp.TargetGateName = Name[1] + Sign[i];
        //        for (int j = 0; j > Sign.Length; j++)
        //        {
        //            Gtp.Keys.Add(Name[2] + Sign[i] + Sign[j]);

        //            KeyList.KeyName = Name[2] + Sign[i] + Sign[j];
        //            KeyList.Collected = false;
        //            Gtp.keysLists.Add(KeyList);
        //            //KeyList = null;
        //        }
        //        ListRef.GatesPropertyList.Add(Gtp);
        //    }
        //}
