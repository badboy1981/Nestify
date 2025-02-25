using UnityEngine;

namespace MazeScreenManagement
{
    public class ScreenDimension : MonoBehaviour
    {
        public Vector2 ScreenSize;
        public Vector2 ScreenSafeArea;
        private void Awake()
        {
            ScreenSize = new(Screen.width, Screen.height);
            ScreenSafeArea = new(Screen.safeArea.width, Screen.safeArea.height);
        }
        private void CalElementPosition()
        {

        }
    }
}