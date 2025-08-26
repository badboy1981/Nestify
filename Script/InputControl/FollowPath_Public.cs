using UnityEngine;
using UnityEngine.Splines;

public class FollowPath_Public
{
    private SplineContainer PathToFollow;
    public FollowPath_Public(SplineContainer InputSpline)
    {
        PathToFollow = InputSpline;
    }
    public Vector3 EvaluatePosition(float ObjectPositionOnPath)
    {
        return PathToFollow.EvaluatePosition(ObjectPositionOnPath);
    }
    public Quaternion EvaluateRotation(float ObjectPositionOnPath,Vector3 Forward)
    {
        //Vector3 forward = Vector3.right;
        Vector3 forward = Forward;
        Vector3 up = Vector3.up;
        Quaternion axisRemapRotation = Quaternion.Inverse(Quaternion.LookRotation(forward, up));
        forward = Vector3.Normalize(PathToFollow.EvaluateTangent(ObjectPositionOnPath));
        up = PathToFollow.EvaluateUpVector(ObjectPositionOnPath);
        return Quaternion.LookRotation(forward, up) * axisRemapRotation;
    }
}