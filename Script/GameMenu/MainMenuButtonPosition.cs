using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using MazeScreenManagement;

namespace GameMenu
{
    public class MainMenuButtonPosition : MonoBehaviour
    {
        [SerializeField] OrientationChangeEvent _OrientationChangeEvent;
        //[SerializeField] ScreenInitialization ScreenInit;
        [SerializeField] GridLayoutGroup ButtonGrid;
        [SerializeField] MazeScreenManagement.ScreenInit _ScreenInit;

        private void Start()
        {
            ButtonGrid = GetComponent<GridLayoutGroup>();
            
            Excute();
        }
        private void OnEnable()
        {
            _OrientationChangeEvent.OnOrientationChangedEvent += OrientationChanged;
        }
        private void OnDisable()
        {
            _OrientationChangeEvent.OnOrientationChangedEvent -= OrientationChanged;
        }
        private void OrientationChanged(ScreenOrientation orientation)
        {
            Debug.Log($"Controller Orientation changed to: {orientation}");
            Excute();
        }

        private MazeScreenManagement.ScreenInit ScInit()
        {
            return new()
            {
                SafeArea = AppConstant.SafeArea,
                ButtonImageSize = new(332f, 163f),
                CellSizeRetio = 6.5f,
                CellSpaceRetio = 1.6f,
                PaddingBottomRatio = 50,
                left = 0,
                top = 0,
                right = 0,
                bottom = (int)(_ScreenInit.SafeArea.y / _ScreenInit.PaddingBottomRatio),
            };
        }

        private void GridCustomize()
        {
            ButtonGrid.padding.left = 0;
            ButtonGrid.padding.right = 0;
            ButtonGrid.padding.top = 0;           
            ButtonGrid.padding.bottom = (int)(AppConstant.SafeArea.y / _ScreenInit.PaddingBottomRatio);

            ButtonGrid.cellSize = new
                (
                _ScreenInit.ButtonImageSize.x / _ScreenInit.CellSizeRetio,
                _ScreenInit.ButtonImageSize.y / _ScreenInit.CellSizeRetio
                );

            ButtonGrid.spacing = new(_ScreenInit.SafeArea.x / _ScreenInit.CellSpaceRetio, 0);

            ButtonGrid.startCorner = _ScreenInit.StartCorner;
            ButtonGrid.startAxis = _ScreenInit.startAxis;
            ButtonGrid.constraint = _ScreenInit.constraint;
            ButtonGrid.childAlignment = _ScreenInit.childAlignment;
        }
        public void Excute()
        {
            _ScreenInit.SafeArea = new(Screen.safeArea.width, Screen.safeArea.height);
                //AppConstant.SafeArea;
            GridCustomize();
        }
    }

    [CustomEditor(typeof(MainMenuButtonPosition))]
    public class ControllerPositionUI : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MainMenuButtonPosition progressBar = (MainMenuButtonPosition)target;
            if (GUILayout.Button("Drow Button!"))
            {
                progressBar.Excute();
            }
        }
    }
}