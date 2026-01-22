using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GateSystem3
{
    public class ReadWriteElementPosition : MonoBehaviour
    {
        [SerializeField] List<Transform> _gatePuzzleList;
        [SerializeField] List<ElementPosition> _elementPositionList;

        public void GateTransformPuzzleList()
        {
            transform.position = new(0,0,0);
            if (_gatePuzzleList != null)
            {
                _gatePuzzleList.Clear();
            }
            foreach (Transform item in gameObject.transform)
            {
                _gatePuzzleList.Add(item);
            }
        }
        public void ClearAllList()
        {
            _gatePuzzleList.Clear();
            _elementPositionList.Clear();
        }

        //Read Puzzle element data from json file and fill to element
        public void ReadJsonPuzzleData()
        {
            foreach (Transform item in _gatePuzzleList)
            {
                var elPos = ElementPosition(ReadPuzzleDataFromJson(item));

                var element = item.Find("Gate");
                element.SetPositionAndRotation(elPos.gate.Position, Quaternion.Euler(elPos.gate.Rotation));
                element.localScale = elPos.gate.Scale;

                element = item.Find("StoneHatch");
                element.SetPositionAndRotation(elPos.stoneHatch.Position, Quaternion.Euler(elPos.stoneHatch.Rotation));
                element.localScale = elPos.stoneHatch.Scale;

                element = item.Find("Collectable_Key");
                foreach (Transform key in element)
                {
                    var keyProp = elPos.keys.Find(s => s.Name == key.name);
                    key.SetPositionAndRotation(keyProp.Position, Quaternion.Euler(keyProp.Rotation));
                    key.localScale = keyProp.Scale;
                }

                element = item.Find("GateController");
                foreach (Transform handle in element)
                {
                    var hdProp = elPos.gateHandle.Find(s => s.Name == handle.name);
                    handle.SetPositionAndRotation(hdProp.Position, Quaternion.Euler(hdProp.Rotation));
                    handle.localScale = hdProp.Scale;
                }
            }
        }
        private string ReadPuzzleDataFromJson(Transform puzzle)
        {
            using var gData = new StreamReader($"{FileProperty.FilePath()}{FileProperty.FileName(puzzle.name)}");
            return gData.ReadToEnd();
        }
        private ElementPosition ElementPosition(string json)
        {
            return JsonUtility.FromJson<ElementPosition>(json);
        }

        //Read puzzle element Tranform date and write to json file
        public void WriteJsonPazzleData()
        {
            if (_elementPositionList != null)
            {
                _elementPositionList.Clear();
            }
            int C = 0;
            foreach (var item in _gatePuzzleList)
            {
                ElementPosition _elementPosition = new()
                {
                    SceneName = SceneManager.GetActiveScene().name,
                    PuzzleName = item.name
                };

                var element = item.Find("Gate").transform;
                _elementPosition.gate = new()
                {
                    Name = element.name,
                    Position = element.position,
                    Rotation = element.rotation.eulerAngles,
                    Scale = element.localScale
                };

                element = null;
                element = item.Find("StoneHatch").transform;
                _elementPosition.stoneHatch = new()
                {
                    Name = element.name,
                    Position = element.position,
                    Rotation = element.rotation.eulerAngles,
                    Scale = element.localScale
                };

                element = null;
                element = item.Find("Collectable_Key");
                _elementPosition.keys = new();
                foreach (Transform el in element)
                {
                    ElementProperty keyProp = new()
                    {
                        Name = el.name,
                        Position = el.position,
                        Rotation = el.rotation.eulerAngles,
                        Scale = el.localScale
                    };
                    _elementPosition.keys.Add(keyProp);
                }

                element = null;
                element = item.Find("GateController");
                _elementPosition.gateHandle = new();
                foreach (Transform el in element)
                {
                    ElementProperty handleProp = new()
                    {
                        Name = el.name,
                        Position = el.position,
                        Rotation = el.rotation.eulerAngles,
                        Scale = el.localScale
                    };
                    _elementPosition.gateHandle.Add(handleProp);
                }
                SaveToJsonFile($"{FileProperty.FileName(_elementPosition.PuzzleName)}", _elementPosition);
                _elementPositionList.Add(_elementPosition);
                C++;
            }
        }
        private void SaveToJsonFile(string fileName, ElementPosition element)
        {
            using var elPos = new StreamWriter($"{FileProperty.FilePath()}{fileName}");
            elPos.Write(JsonUtility.ToJson(element));
        }
    }
}