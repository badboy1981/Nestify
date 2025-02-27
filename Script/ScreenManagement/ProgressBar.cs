using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace MazeScreenManagement
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] RectTransform ProgressBarRect;
        [SerializeField] GridLayoutGroup ProgressBarGridLayout;
        [SerializeField] GridLayoutGroup IconGridLayout;
        [SerializeField] TextMeshProUGUI IconText;

        [SerializeField] Vector2 SafeArea;
        [SerializeField] Vector2 CellSize;
        [SerializeField] float CellSizeRatio;
        [SerializeField] float TableGap;

        [SerializeField] float FontRatio1;
        [SerializeField] float FontRatio2;

        private void Awake()
        {
            Excute();
        }
        private void Init()
        {
            SafeArea = new(Screen.safeArea.width, Screen.safeArea.height);
            ProgressBarRect = GetComponent<RectTransform>();
            ProgressBarRect.sizeDelta = SafeArea;
            CellSize = new(600, 270);
            CellSizeRatio = 6;
            TableGap = 30;
            FontRatio1 = 2.5f;
            FontRatio2 = 1.5f;
        }
        private void GridCustomize()
        {
            ProgressBarGridLayout = GetComponent<GridLayoutGroup>();

            float CellWide = SafeArea.x / CellSizeRatio;
            float CellHeight = CellWide * CellSize.y / CellSize.x;

            ProgressBarGridLayout.padding.left = 0;
            ProgressBarGridLayout.padding.right = 0;
            ProgressBarGridLayout.padding.top = 15;
            ProgressBarGridLayout.padding.bottom = 0;


            ProgressBarGridLayout.cellSize = new(CellWide, CellHeight);
            ProgressBarGridLayout.spacing = new(SafeArea.x / TableGap, SafeArea.y / TableGap);

            ProgressBarGridLayout.startCorner = 0;
            ProgressBarGridLayout.startAxis = 0;
            ProgressBarGridLayout.constraint = 0;
            ProgressBarGridLayout.childAlignment = TextAnchor.UpperCenter;
        }
        private void InitIcon()
        {
            float IconCellheight = ProgressBarGridLayout.cellSize.y / 1.5f;
            Vector2 IconSize = new(IconCellheight, IconCellheight);
            float FontSize;
            for (int i = 0; i < transform.childCount; i++)
            {
                IconGridLayout = transform.GetChild(i).GetComponent<GridLayoutGroup>();
                IconGridLayout.padding.left = 0;
                IconGridLayout.padding.right = 0;
                IconGridLayout.padding.top = 0;
                IconGridLayout.padding.bottom = 0;

                IconGridLayout.cellSize = IconSize;
                IconGridLayout.spacing = new(50, 0);

                IconGridLayout.startCorner = 0;
                IconGridLayout.startAxis = 0;
                IconGridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                IconGridLayout.constraintCount = 2;
                IconGridLayout.childAlignment = TextAnchor.MiddleCenter;

                IconText = transform.GetChild(i).transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();

                if (IconText.text.Count() < 3)
                {
                    FontSize = IconCellheight / FontRatio2;
                }
                else
                {
                    FontSize = IconCellheight / FontRatio1;
                }
                IconText.fontSize = FontSize;
                IconText.horizontalAlignment = HorizontalAlignmentOptions.Left;
                IconText.verticalAlignment = VerticalAlignmentOptions.Middle;
            }
        }
        public void Excute()
        {
            Init();
            GridCustomize();
            InitIcon();
        }
    }
}