using UnityEngine;

public class OrientationChecker : MonoBehaviour
{
    [SerializeField] OrientationChangeEvent orientationChangeEvent;

    private void Start()
    {
        orientationChangeEvent.Initialize();
    }

    private void Update()
    {
        orientationChangeEvent.CheckOrientationChange();
    }
}