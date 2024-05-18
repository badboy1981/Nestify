using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMap : MonoBehaviour
{
    [SerializeField] Vector2 PanelSize;
    [SerializeField] GameObject MapPanel;
    [SerializeField] GameObject Canvas;
    private RectTransform recT;
    private RectTransform paneL;

    private void Awake()
    {        
        paneL = MapPanel.GetComponent<RectTransform>();
        recT = Canvas.GetComponent<RectTransform>();
        MapPanel.SetActive(false);
    }
    public void MapOpener()
    {        
        paneL.sizeDelta = recT.sizeDelta - new Vector2(10, 10);
        PanelSize= paneL.sizeDelta;
        if (MapPanel != null) MapPanel.SetActive(!MapPanel.activeSelf);
        //MapPanel?.SetActive(!MapPanel.activeSelf);
    }
}