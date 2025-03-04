using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    public OrientationChangeEvent orientationChangeEvent;

    void OnEnable()
    {
        orientationChangeEvent.OnOrientationChangedEvent += HandleOrientationChange;
    }

    void OnDisable()
    {
        orientationChangeEvent.OnOrientationChangedEvent -= HandleOrientationChange;
    }

    void HandleOrientationChange(ScreenOrientation orientation)
    {
        Debug.Log("Orientation changed to: " + orientation);
        // سایر کدهای مورد نظر برای تغییر جهت صفحه
    }
}