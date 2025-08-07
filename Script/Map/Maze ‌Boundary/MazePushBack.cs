using CellMap;
using UnityEngine;

public class MazePushBack : MonoBehaviour
{
    [SerializeField] string voltLayerName = "Player";
    [SerializeField] MazeMapSB mapSB;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(voltLayerName))
        {
            ReturnVoltInside(other.gameObject);
        }
    }
    private void ReturnVoltInside(GameObject volt)
    {
        volt.transform.position = mapSB.MazeCellProperty.CellPosition;
        Debug.Log("Volt returned to the maze!");
    }
}