using UnityEngine;

namespace MazeScreenManagement
{
    public class ControllerPosition : MonoBehaviour
    {
        [SerializeField] RectTransform MoveStickPos;
        [SerializeField] RectTransform FreeViewPos;

        [SerializeField] Vector2 SafeArea;
        [SerializeField] float PosRatioX;// 5
        [SerializeField] float PosRatioY;// 5

        [SerializeField] Vector2 MoveStickPosition;
        [SerializeField] Vector2 FreeViewPosition;

        private void Awake()
        {
            Excute();
        }
        public void Excute()
        {
            Init();
            BottomPosition();
        }
        private void Init()
        {
            SafeArea = new(Screen.safeArea.width, Screen.safeArea.height);
            GetComponent<RectTransform>().sizeDelta = SafeArea;

            MoveStickPos = transform.GetChild(0).GetComponent<RectTransform>();
            FreeViewPos = transform.GetChild(1).GetComponent<RectTransform>();
        }
        private void BottomPosition()
        {
            Vector2 AnchorPoint = new(0.5f, 0.5f);
            float PosX = SafeArea.x / PosRatioX;
            float PosY = SafeArea.y / PosRatioY;

            MoveStickPosition = new(PosX, PosY);
            FreeViewPosition = new(-PosX, PosY);

            Vector2 ButtonSize = new(250, 250);

            MoveStickPos.sizeDelta = ButtonSize;
            MoveStickPos.anchorMin = AnchorPoint;
            MoveStickPos.anchorMax = AnchorPoint;
            MoveStickPos.anchoredPosition = AnchorPoint;
            //MoveStickPos.anchoredPosition = MoveStickPosition; // استفاده از anchoredPosition

            FreeViewPos.sizeDelta = ButtonSize;
            FreeViewPos.anchorMin = AnchorPoint;
            FreeViewPos.anchorMax = AnchorPoint;
            FreeViewPos.anchoredPosition = AnchorPoint;
            //FreeViewPos.anchoredPosition = FreeViewPosition; // استفاده از anchoredPosition

            MoveStickPos.transform.position = MoveStickPosition;
            FreeViewPos.transform.position = FreeViewPosition;

            //Vector2 PosTest = Vector2.zero;
            //MoveStickPos.position = PosTest;
            //FreeViewPos.position = PosTest;

        }
    }
}