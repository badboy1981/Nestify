using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerObjectScript : MonoBehaviour
{
    [SerializeField] SavePlayerData playerData;
    [SerializeField] Vector3 PlayerPosition = Vector3.zero;
    [SerializeField] float TestValue = 0;
    private void Update()
    {
        PlayerPosition = playerData.PlayerPosition;
        TestValue = playerData.TestFloat;
    }
}
