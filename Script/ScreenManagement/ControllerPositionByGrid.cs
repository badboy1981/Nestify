using UnityEngine;
using UnityEngine.UI;
namespace MazeScreenManagement
{
    public class ControllerPositionByGrid : MonoBehaviour
    {
        [SerializeField] RectTransform ScreenArea;
        [SerializeField] GridLayoutGroup ControlGrid;
        [SerializeField] OrientationChangeEvent _OrientationChangeEvent;

        [SerializeField] RectTransform MoveStickRect;
        [SerializeField] RectTransform FreeViewRect;

        [SerializeField] Vector2 SafeArea;
        [SerializeField] float CellSizeRetio;
        [SerializeField] float CellSpaceRetio;
        [SerializeField] int PaddingBottomRatio;

        
        private void Awake()
        {
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
        private void Init()
        {
            SafeArea = new(Screen.safeArea.width, Screen.safeArea.height);
            CellSizeRetio = 6.5f;// 11;
            CellSpaceRetio = 1.6f;
            PaddingBottomRatio = 50;// 9;
            ScreenArea = GetComponent<RectTransform>();
            ControlGrid = GetComponent<GridLayoutGroup>();
        }
        private void InitScreen()
        {
            ScreenArea.sizeDelta = SafeArea;
        }
        private void InitAnchorPoint()
        {
            Vector2 AnchorPoint = new(0.5f, 0.5f);
            MoveStickRect = transform.GetChild(0).GetComponent<RectTransform>();
            FreeViewRect = transform.GetChild(0).GetComponent<RectTransform>();

            MoveStickRect.anchorMin = AnchorPoint;
            MoveStickRect.anchorMax = AnchorPoint;

            FreeViewRect.anchorMin = AnchorPoint;
            FreeViewRect.anchorMax = AnchorPoint;

            MoveStickRect.anchoredPosition = Vector2.zero;
            FreeViewRect.anchoredPosition = Vector2.zero;
        }
        private void GridCustomize()
        {
            ControlGrid.padding.left = 0;
            ControlGrid.padding.right = 0;
            ControlGrid.padding.top = 0;
            ControlGrid.padding.bottom = (int)SafeArea.y / PaddingBottomRatio;

            float CellSize = SafeArea.x / CellSizeRetio;
            ControlGrid.cellSize = new(CellSize, CellSize);
            ControlGrid.spacing = new((SafeArea.x / CellSpaceRetio), 0);

            ControlGrid.startCorner = 0;
            ControlGrid.startAxis = 0;
            ControlGrid.constraint = 0;
            ControlGrid.childAlignment = TextAnchor.LowerCenter;
        }
        public void Excute()
        {
            Init();
            InitScreen();
            //InitAnchorPoint();
            GridCustomize();
        }
    }
}