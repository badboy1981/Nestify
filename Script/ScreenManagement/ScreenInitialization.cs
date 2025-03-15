using UnityEngine;
using UnityEngine.UI;

namespace MazeScreenManagement
{
    [CreateAssetMenu(fileName = "ScreenInitialization", menuName = "My Asset/Screen Initialization")]
    public class ScreenInitialization : ScriptableObject
    {
        public Vector2 ScreenSize = new(Screen.width, Screen.height);
        public Vector2 SafeArea;// = new(Screen.safeArea.width, Screen.safeArea.height);

        public float CellSizeRetio = 6.5f;
        public float CellSpaceRetio = 1.6f;
        public float PaddingBottomRatio = 50;

        public GridLayoutGroup.Corner StartCorner = 0;
        public GridLayoutGroup.Axis startAxis = 0;
        public GridLayoutGroup.Constraint constraint = 0;
        public TextAnchor childAlignment = TextAnchor.LowerCenter;

    }
}