using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMap : MonoBehaviour
{
    [SerializeField] GameObject MapPanel;
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject MapButton;
    [SerializeField] GameObject MapImage;

    [SerializeField] Vector2 ScreenSize;
    [SerializeField] Vector2 CanvasSize;
    [SerializeField] Vector2 PanelSize;
    [SerializeField] Rect SafeAria;


    private RectTransform canVas;
    private RectTransform paneL;
    private RectTransform ButToN;
    private RectTransform MapImaGe;
   
    private void Awake()
    {
        MapPanel.SetActive(false);        

        paneL = MapPanel.GetComponent<RectTransform>();
        canVas = Canvas.GetComponent<RectTransform>();
        ButToN = MapButton.GetComponent<RectTransform>();
        MapImaGe=MapImage.GetComponent<RectTransform>();

        ButtonProp();
    }
    private void initScreenSize()
    {
        ButtonProp();
        ResizeImage();

        ScreenSize = new Vector2(Screen.width, Screen.height);
        CanvasSize = new Vector2(SafeAria.width, SafeAria.height);

        paneL.sizeDelta = new Vector2(SafeAria.width - 100, SafeAria.height - 100);
        paneL.transform.position = new Vector2(SafeAria.xMin + SafeAria.width / 2, SafeAria.yMin + SafeAria.height / 2);
        //paneL.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SafeAria.width);

        PanelSize = paneL.sizeDelta;
    }

    private void ButtonProp()
    {
        SafeAria = Screen.safeArea;
        ButToN.position = new Vector2(SafeAria.xMax - 100, SafeAria.yMin + 100);
        float btSize = SafeAria.width / 15;
        ButToN.sizeDelta = new Vector2(btSize, btSize);
    }

    private void ResizeImage()
    {
        MapImaGe.sizeDelta = new Vector2(1920, 1080);
    }

    public void MapOpener()
    {
        initScreenSize();
        if (MapPanel != null) MapPanel.SetActive(!MapPanel.activeSelf);
    }
    private void tttt()
    {
        //string sdsd = null;
    }
}