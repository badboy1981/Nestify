//using UnityEngine;
//using UnityEditor;
//using System.Linq;

//[CustomPropertyDrawer(typeof(GateConfig))]
//public class GateConfigDrawer : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        EditorGUI.BeginProperty(position, label, property);

//        // Layout
//        var line = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

//        // Draw default nested properties
//        EditorGUI.PropertyField(line, property, label, true);
//        line.y += EditorGUI.GetPropertyHeight(property, label, true) + 4;

//        // Button
//        if (GUI.Button(line, "Auto-Fill Dependencies"))
//        {
//            // Find sub-properties
//            var gateProp = property.FindPropertyRelative("gate");
//            var hatchProp = property.FindPropertyRelative("stoneHatch");
//            var handleProp = property.FindPropertyRelative("handle");
//            var keysProp = property.FindPropertyRelative("RequiredKeys");

//            // Resolve GateID from gate.ID or fallback
//            string gateID = "A";
//            var gateIDProp = gateProp?.FindPropertyRelative("ID");
//            if (gateIDProp != null && !string.IsNullOrEmpty(gateIDProp.stringValue))
//                gateID = gateIDProp.stringValue;

//            var allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

//            // Helper to set GameEntityProperty
//            void FillEntity(SerializedProperty entityProp, string name, EntityTypeEnum type)
//            {
//                if (entityProp == null) return;
//                entityProp.FindPropertyRelative("ID").stringValue = gateID;
//                entityProp.FindPropertyRelative("Name").stringValue = name;
//                entityProp.FindPropertyRelative("IsCollected").boolValue = false;
//                entityProp.FindPropertyRelative("Type").enumValueIndex = (int)type;
//            }

//            // Gate/Hatch/Handle presence check by name pattern (optional)
//            if (allObjects.Any(o => o.name == gateID + "Gate"))
//                FillEntity(gateProp, "Gate", EntityTypeEnum.Gate);

//            if (allObjects.Any(o => o.name == gateID + "Hatch"))
//                FillEntity(hatchProp, "Hatch", EntityTypeEnum.Hatch);

//            if (allObjects.Any(o => o.name == gateID + "Handle"))
//                FillEntity(handleProp, "Handle", EntityTypeEnum.Handle);

//            // Keys list
//            keysProp.ClearArray();
//            var keys = allObjects
//                .Where(o => o.name.StartsWith(gateID))
//                .Where(o => o.name.EndsWith("Key"))
//                .Select(o => o.name.Replace(gateID, "")) // "AAKey" -> "AKey"
//                .ToList();

//            for (int i = 0; i < keys.Count; i++)
//            {
//                keysProp.InsertArrayElementAtIndex(i);
//                var item = keysProp.GetArrayElementAtIndex(i);
//                item.FindPropertyRelative("ID").stringValue = gateID;
//                item.FindPropertyRelative("Name").stringValue = keys[i];
//                item.FindPropertyRelative("IsCollected").boolValue = false;
//                item.FindPropertyRelative("Type").enumValueIndex = (int)EntityTypeEnum.GateKey;
//            }

//            property.serializedObject.ApplyModifiedProperties();
//        }

//        EditorGUI.EndProperty();
//    }

//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        // Height for the expanded property + button
//        return EditorGUI.GetPropertyHeight(property, label, true) + EditorGUIUtility.singleLineHeight + 8;
//    }
//}