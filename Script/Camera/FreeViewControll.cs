using UnityEngine;

public class FreeViewControll : MonoBehaviour
{
    [SerializeField] MyInputInit _InputControl;
    [SerializeField] Vector3 _Rotation;
    //[SerializeField] float _RotationSpeed;
    [SerializeField] float mappedY;
    [SerializeField] float mappedX;
    [SerializeField] Vector3 DefaultView = new(0, 180f, 0);

    [SerializeField] Vector4 fView;
    [SerializeField] Vector2 fViewRatio;
    private void Start()
    {
        fViewRatio = new(60f, 15f);
        fView = new(180f - fViewRatio.x, 180f + fViewRatio.x, 0 + fViewRatio.y, 0 - fViewRatio.y);
        _Rotation.y = 180f;
        transform.eulerAngles = DefaultView;
        _InputControl.MobileSteeringWheelEvent += FreeView;
    }
    private void FreeView(Vector2 View)
    {
        mappedY = Mathf.Lerp(fView.x, fView.y, ShiftFloat(View.x));
        mappedX = Mathf.Lerp(fView.z, fView.w, ShiftFloat(View.y));

        _Rotation.y = mappedY;
        _Rotation.x = mappedX;
        transform.localEulerAngles = _Rotation;
    }
    private float ShiftFloat(float Inp)
    {
        return (Inp + 1) / 2;
    }
}