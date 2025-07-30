using UnityEngine;
public static class CellUtils
{

    //public static Vector3 RoundVector(Vector3 pos)
    //{
    //    return new Vector3(
    //        RoundFloatByThreshold(pos.x),
    //        RoundFloatByThreshold(pos.y),
    //        RoundFloatByThreshold(pos.z)
    //    );
    //}
    public static Vector3 RoundVector(Vector3 v)
    {
        return new Vector3(
            Mathf.Round(v.x),
            Mathf.Round(v.y),
            Mathf.Round(v.z)
        );
    }

    private static float RoundFloatByThreshold(float value)
    {
        float decimalPart = value - Mathf.Floor(value);
        return (decimalPart >= 0.5f) ? Mathf.Ceil(value) : Mathf.Floor(value);
    }
}