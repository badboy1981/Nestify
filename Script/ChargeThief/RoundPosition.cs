using UnityEngine;

public class RoundPosition : MonoBehaviour
{
    public void RoundChildrenPositions()
    {
        foreach (Transform child in transform)
        {
            Vector3 original = child.position;

            float roundedX = Mathf.FloorToInt(original.x + 0.5f);
            float roundedZ = Mathf.FloorToInt(original.z + 0.5f);

            child.position = new Vector3(roundedX, 0.2f, roundedZ);
        }
    }
}
