using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(fileName = "GateManagment", menuName = "Gate/Gate Managment")]

[System.Serializable]
public class GateConfig
{    
    public GameEntityProperty gate;
    public GameEntityProperty stoneHatch;
    public List<GameEntityProperty> handle;
    public List<GameEntityProperty> RequiredKeys;
}