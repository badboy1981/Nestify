using UnityEngine;

namespace GameMenu
{
    public class ButtonPositionUiChild : ButtonPositionUI
    {
        public override void Excute()
        {
            base.Excute();
        }
    }

    [UnityEditor.CustomEditor(typeof(ButtonPositionUiChild))]
    public class IconPosition : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ButtonPositionUiChild progressBar = (ButtonPositionUiChild)target;
            if (GUILayout.Button("Drow Button!"))
            {
                progressBar.Excute();
            }
        }
    }
}