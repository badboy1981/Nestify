using System;
using UnityEngine;
using UnityEngine.UI;

namespace MazeScreenManagement
{
    [Serializable]
    public class ScreenInit
    {
        public Vector2 SafeArea;
        public Vector2 ButtonImageSize;
        public float CellSizeRetio; // = 6.5f;
        public float CellSpaceRetio; // = 1.6f;
        public float PaddingBottomRatio; // = 50;

        public int left;
        public int right;
        public int top;
        public int bottom;


        public GridLayoutGroup.Corner StartCorner = 0;
        public GridLayoutGroup.Axis startAxis = (GridLayoutGroup.Axis)0;
        public GridLayoutGroup.Constraint constraint = (GridLayoutGroup.Constraint)1;
        public TextAnchor childAlignment = TextAnchor.LowerCenter;
    }
}