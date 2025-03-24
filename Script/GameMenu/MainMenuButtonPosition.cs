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
        [SerializeField] RectTransform _RectTransform;
        [SerializeField] MazeScreenManagement.ScreenInit _ScreenInit;

        private void Start()
        {
            ButtonGrid = GetComponent<GridLayoutGroup>();
            _RectTransform = GetComponent<RectTransform>();

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



   
        private void GridCustomize()
        {
            float Wimg = Persentage(_ScreenInit.SafeArea.x, _ScreenInit.CellSizeRetio);
            float Himg = HightImage(Wimg);
            ButtonGrid.cellSize = new(Wimg, Himg);

            ButtonGrid.spacing = new(Persentage(Wimg, _ScreenInit.CellSpaceRetioW), Persentage(Himg, _ScreenInit.CellSpaceRetioH));

            ButtonGrid.padding.left = (int)Persentage(_ScreenInit.SafeArea.x, _ScreenInit.PaddingLeftRatio);
            //ButtonGrid.padding.right = _ScreenInit.right;
            ButtonGrid.padding.top = (int)(Persentage(_ScreenInit.SafeArea.y, _ScreenInit.PaddingTopRatio) - Himg / 2);
            //ButtonGrid.padding.bottom = _ScreenInit.bottom;

            ButtonGrid.startCorner = _ScreenInit.StartCorner;
            ButtonGrid.startAxis = _ScreenInit.startAxis;
            ButtonGrid.constraint = _ScreenInit.constraint;
            ButtonGrid.childAlignment = _ScreenInit.childAlignment;
        }
        private int Persentage(float value, float persentage)
        {
            return (int)(value * persentage);
        }
        private int WideImage(float NewHeight)
        {
            return (int)((_ScreenInit.ButtonImageSize.x * NewHeight) / (_ScreenInit.ButtonImageSize.y));
        }
        private int HightImage(float NewWide)
        {
            return (int)((_ScreenInit.ButtonImageSize.y * NewWide) / (_ScreenInit.ButtonImageSize.x));
        }
        public void Excute()
        {
            _ScreenInit.SafeArea = new(Screen.safeArea.width, Screen.safeArea.height); 
            _RectTransform.sizeDelta = _ScreenInit.SafeArea;
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