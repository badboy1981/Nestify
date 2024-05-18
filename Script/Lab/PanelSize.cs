using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSize : MonoBehaviour
{
    [SerializeField] Vector2 CanvasSize;
    [SerializeField] GameObject Panel;
    RectTransform recT = new();
    //se Start is called before the first frame update

    private void Awake()
    {
        recT = GetComponent<RectTransform>();        
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CanvasSize = recT.rect.size;
        Debug.Log($"{recT.rect.size} || {CanvasSize}");
    }
}