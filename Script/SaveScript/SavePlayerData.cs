using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SavePlayerData", menuName = "My Asset/SavePlayerData")]

public class SavePlayerData : ScriptableObject
{
	[SerializeField] Vector3 _PlayerPosition;
	[SerializeField] float _TestFloat;

	//public SavePlayerData()
	//{
	//	_PlayerPosition = PlayerPosition;
	//	_TestFloat = TestFloat;
	//}

	public Vector3 PlayerPosition
	{
		get { return _PlayerPosition; }
		set
		{
			if (value != null)
			{
				_PlayerPosition = value;
			}
			else
			{
				_ = new Vector3(10, 20, 30);
			}
		}
	}
	public float TestFloat
	{
		get => _TestFloat;
		set => _TestFloat = value;
	}
}