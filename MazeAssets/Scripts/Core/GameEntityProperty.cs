using UnityEngine;

[CreateAssetMenu(fileName = "GameEntityProperty", menuName = "Public Property/Game Entity Property")]
[System.Serializable]
public class GameEntityProperty : ScriptableObject
{
    public string ID;                           // Unique identifier for the stage or group (e.g., A, B, C)
    public string Name;                         // Name of the object type (e.g., Gate, Hatch, Handle, Key1)
    public bool IsCollected;                    // Current status (true if collected/unlocked, false otherwise)
    public EntityTypeEnum Type;                     // Category of the entity (helps to distinguish Gate, Hatch, Handle, Key)
    public string UniqueID => $"{ID}_{Name}";  // Composite unique key generated from ID + Name
}