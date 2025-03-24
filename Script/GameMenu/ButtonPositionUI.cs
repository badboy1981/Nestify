using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using MazeScreenManagement;
using TMPro;

namespace GameMenu
{
    public class ButtonPositionUI : MonoBehaviour
    {
        [SerializeField] OrientationChangeEvent _OrientationChangeEvent;
        [SerializeField] RectTransform _RectTransform;
        [SerializeField] RectTransform _ChildRect;
        [SerializeField] GridLayoutGroup ButtonGrid;
        [SerializeField] GridLayoutGroup ChildGr;
        [SerializeField] TextMeshProUGUI LevelText;
        [SerializeField] string Massage;
        [SerializeField] ScreenInit _ButtonInit;
        [SerializeField] ScreenInit _ImageInit;

        [SerializeField] GridRatio AGridRatio;
        [SerializeField] GridInit AGridInit;

        [SerializeField] GridRatio AChildGridRatio;
        [SerializeField] GridInit AChildInit;

        [SerializeField] FontSizeRatio _FontSizeRatio;

        private void Awake()
        {
            _RectTransform = GetComponent<RectTransform>();
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
            Excute();
        }

        private void Init()
        {
            _ButtonInit.SafeArea = new(Screen.safeArea.width, Screen.safeArea.height);
            _RectTransform.sizeDelta = _ButtonInit.SafeArea;
        }
        private void GridCustomize()
        {
            float Wimg = Persentage(_ButtonInit.SafeArea.x, _ButtonInit.CellSizeRetio);
            float Himg = HightImage(Wimg);
            ButtonGrid.cellSize = new(Wimg, Himg);

            ButtonGrid.spacing = new(Persentage(Wimg, _ButtonInit.CellSpaceRetioW), Persentage(Himg, _ButtonInit.CellSpaceRetioH));

            ButtonGrid.padding.left = (int)Persentage(_ButtonInit.SafeArea.x, _ButtonInit.PaddingLeftRatio);
            ButtonGrid.padding.top = (int)(Persentage(_ButtonInit.SafeArea.y, _ButtonInit.PaddingTopRatio) - Himg / 2);

            ButtonGrid.startCorner = _ButtonInit.StartCorner;
            ButtonGrid.startAxis = _ButtonInit.startAxis;
            ButtonGrid.constraint = _ButtonInit.constraint;
            ButtonGrid.childAlignment = _ButtonInit.childAlignment;

            InitChild();
        }
        private void InitChild()
        {
            //float ImgSize = Persentage(ButtonGrid.cellSize.y, _ImageInit.CellSizeRetio);
            float ImgSize = Persentage(ButtonGrid.cellSize.y, _ImageInit.CellSizeRetio);
            Vector2 ImageSize = new(ImgSize, ImgSize);            
            int ImgVerticalPos = (int)(_ImageInit.PaddingTopRatio * ButtonGrid.cellSize.y);

            for (int i = 0; i < _RectTransform.childCount; i++)
            {
                if (_RectTransform.transform.GetChild(i).CompareTag("UiButton1"))
                {
                    ChildGr = _RectTransform.transform.GetChild(i).GetComponent<GridLayoutGroup>();
                    ChildGr.cellSize = ImageSize;

                    ChildGr.padding.top = ImgVerticalPos;
                    ChildGr.padding.left = (int)_ImageInit.PaddingLeftRatio;
                    ChildGr.padding.right = 0;
                    ChildGr.padding.bottom = 0;

                    ChildGr.startCorner = _ImageInit.StartCorner;
                    ChildGr.startAxis = _ImageInit.startAxis;
                    ChildGr.constraint = _ImageInit.constraint;
                    ChildGr.childAlignment = _ImageInit.childAlignment;

                    LevelText = _RectTransform.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                    LevelText.fontSize = _FontSizeRatio.SizeRatio * ImgSize;
                }
            }
        }
        private int Persentage(float value, float persentage)
        {
            return (int)(value * persentage);
        }
        private int WideImage(float NewHeight)
        {
            return (int)((_ButtonInit.ButtonImageSize.x * NewHeight) / (_ButtonInit.ButtonImageSize.y));
        }
        private int HightImage(float NewWide)
        {
            return (int)((_ButtonInit.ButtonImageSize.y * NewWide) / (_ButtonInit.ButtonImageSize.x));
        }


        public void Excute()
        {
            Init();
            GridCustomize();
        }
    }

    [CustomEditor(typeof(ButtonPositionUI))]
    public class ButtonPosition : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ButtonPositionUI progressBar = (ButtonPositionUI)target;
            if (GUILayout.Button("Drow Button!"))
            {
                progressBar.Excute();
            }
        }
    }
}