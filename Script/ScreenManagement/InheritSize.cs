using UnityEngine;

public class InheritSize : MonoBehaviour
{
    [SerializeField] RectTransform FatherRect;

    private void Start()
    {
        FatherRect = GetComponent<RectTransform>();
    }

}
