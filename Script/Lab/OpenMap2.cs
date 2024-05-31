using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMap2 : MonoBehaviour
{
    [SerializeField] GameObject ImageMap;

    [SerializeField] Vector2 ScreenSize;
    [SerializeField] Rect SafeArea;
    [SerializeField] Vector2 ImageSize;
    [SerializeField] Vector2 ImagePosition;



    private void Awake()
    {
        LoadOnAwake();
    }
    private void LoadOnAwake()
    {
        GetScreenSize();
        GetSafeAreaSize();
        SetMapImageSizeAndPosition();
    }
    private void GetScreenSize()
    {
        ScreenSize = new Vector2(Screen.width, Screen.height);
    }
    private void GetSafeAreaSize()
    {
        SafeArea = Screen.safeArea;
    }
    private void SetMapImageSizeAndPosition()
    {
        float Offset = 50;
        float Size = SafeArea.height - Offset;
        RectTransform ImageMapRect;
        ImageMapRect = ImageMap.GetComponent<RectTransform>();
        ImageMapRect.sizeDelta = new Vector2(Mathf.Floor(Size * 1.77778f), Size);
        ImageMapRect.position = new Vector2(SafeArea.xMin + SafeArea.width / 2, SafeArea.yMin + SafeArea.height / 2);
    }
}