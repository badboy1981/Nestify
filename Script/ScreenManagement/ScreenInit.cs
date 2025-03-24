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
        public float CellSpaceRetioW; // = 1.6f;
        public float CellSpaceRetioH; // = 1.6f;
        public float PaddingTopRatio;
        public float PaddingLeftRatio; // = 50;

        //public int left;
        //public int right;
        //public int top;
        //public int bottom;


        public GridLayoutGroup.Corner StartCorner = 0;
        public GridLayoutGroup.Axis startAxis = (GridLayoutGroup.Axis)0;
        public GridLayoutGroup.Constraint constraint = (GridLayoutGroup.Constraint)0;
        public TextAnchor childAlignment = TextAnchor.MiddleCenter;
    }

    [Serializable]
    public class GridRatio
    {
        public Vector2 CellSize; //Image Size
        public float CellSizeRetio;
        public float CellSpaceRetioW;
        public float CellSpaceRetioH;
        public float PaddingTopRatio;
        public float PaddingLeftRatio;
    }

    [Serializable]
    public class GridInit
    {
        public GridLayoutGroup.Corner StartCorner = 0;
        public GridLayoutGroup.Axis startAxis = 0;
        public GridLayoutGroup.Constraint constraint = 0;
        public TextAnchor childAlignment = TextAnchor.MiddleCenter;
    }

    [Serializable]
    public class FontSizeRatio
    {
        public float SizeRatio;
    }
}