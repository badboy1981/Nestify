using System;
using System.Collections.Generic;

namespace TestSpace
{
    [Serializable]
    public class Gate
    {
        public string HatchName;
        public string TargetGateName;
        public List<string> Keys = new();
    }

    public class ListTest
    {
        public List<Gate> Gates = new();
    }
}