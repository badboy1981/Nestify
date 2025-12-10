using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyManagment", menuName = "Keys/Key Managment")]
public class KeyManagment : ScriptableObject
{
    public KeyCollectedEvent keyGetEvent;
    public List<string> collectedKeyIDs;
}