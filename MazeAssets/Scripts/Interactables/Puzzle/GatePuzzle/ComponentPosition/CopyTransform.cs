using UnityEngine;

namespace GateSystem3
{
    public class CopyTransform : MonoBehaviour
    {
        [SerializeField] Transform source;
        [SerializeField] Transform destination;

        public void Copy()
        {
            destination.SetPositionAndRotation(source.position, source.rotation);
            destination.localScale = source.localScale;
        }

        private Vector3 RoundChildrenPositions(Vector3 v)
        {
            //foreach (Transform child in transform)
            //{}
                //Vector3 original = child.position;

                //float roundedX = Mathf.FloorToInt(original.x + 0.5f);
                //float roundedZ = Mathf.FloorToInt(original.z + 0.5f);

                //child.position = new Vector3(roundedX, 0.2f, roundedZ);
            return v.normalized;
        }
        private float Round(float input)
        {
            return Mathf.FloorToInt(input + 0.5f);

        }
    }
}