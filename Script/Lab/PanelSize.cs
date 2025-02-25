using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSize : MonoBehaviour
{
    [SerializeField] Vector2 CanvasSize;
    [SerializeField] GameObject Panel;
    RectTransform recT = new();

    private void Awake()
    {
        recT = GetComponent<RectTransform>();        
    }
    void Update()
    {
        CanvasSize = recT.rect.size;
        Debug.Log($"{recT.rect.size} || {CanvasSize}");
    }
    private void ScreenDimension()
    {
        Vector2 SafeAreaSize= new(Screen.safeArea.width, Screen.safeArea.height);
        Vector2 ScreenSize = new(Screen.width, Screen.height);

    }
}