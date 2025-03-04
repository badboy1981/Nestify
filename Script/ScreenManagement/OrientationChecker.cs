using UnityEngine;

public class OrientationChecker : MonoBehaviour
{
    [SerializeField] OrientationChangeEvent orientationChangeEvent;

    void Start()
    {
        orientationChangeEvent.Initialize();
    }

    void Update()
    {
        orientationChangeEvent.CheckOrientationChange();
    }
}
