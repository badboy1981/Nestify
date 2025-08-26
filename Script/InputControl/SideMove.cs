using UnityEngine;
using UnityEngine.Splines;


public class SideMove : MonoBehaviour
{
    [SerializeField] SplineContainer SplineCrossPath;
    [SerializeField] float ObjectPositionOnPath;

    FollowPath_Public CrossPath;

    Vector3 position = Vector3.zero;
    Quaternion rotation = Quaternion.identity;

    [SerializeField] Animator _Animator;
    [SerializeField] InputReader Input;

    [SerializeField] float SideMotion;
    [SerializeField] float SideRotate;

    private float MoveStepLen;

    private void OnEnable()
    {
        CrossPath = new FollowPath_Public(SplineCrossPath);
    }
    private void Awake()
    {
        SideMotion = 0f;
        MoveStepLen = 0.005f;
        ObjectPositionOnPath = 0.5f;
        Input.MoveStartEvent += handleSideMove_Start;
        Input.MoveCancledEvent += handleSideMove_Cancle;
    }
    void Update()
    {
        StartMove();
        StartRotate();
        position = CrossPath.EvaluatePosition(ObjectPositionOnPath);
        rotation = CrossPath.EvaluateRotation(ObjectPositionOnPath, Vector3.right);
        rotation *= Quaternion.AngleAxis(SideRotate, Vector3.forward);
        //Debug.Log($"Side Move Direction: {SideMoveDirection}");
    }
    private void LateUpdate()
    {
        transform.SetPositionAndRotation(position, rotation);
    }
    private void handleSideMove_Start(float _SideMoveStart) { SideMotion = _SideMoveStart; }
    private void handleSideMove_Cancle(float _SideMoveStop) { SideMotion = 0; }
    private void StartRotate()
    {
        _Animator.SetFloat("Rotate", SideMotion);
    }
    private void StartMove()
    {
        ObjectPositionOnPath += MoveStepLen * SideMotion;
        if (ObjectPositionOnPath > 0.99f) { ObjectPositionOnPath = 0.99f; }
        if (ObjectPositionOnPath < 0.01f) { ObjectPositionOnPath = 0.01f; }
    }
}
