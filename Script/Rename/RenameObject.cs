using UnityEngine;
using UnityEditor;

public class RenamePrefabsWindow : EditorWindow
{
    private string newNamePattern = ""; // الگوی نام جدید
    private string[] parentNameOptions = { "ParkingBarrier", "Sign", "Key", "Coin", "Gate", "CrainBox", "CrainHandle" }; // نام‌های پیش‌فرض
    private string[] childNameOptions = { "CH" }; // نام‌های پیش‌فرض زیرمجموعه‌ها
    private int selectedParentOptionIndex = 0; // ایندکس گزینه انتخاب‌شده والد
    private bool[] selectedChildOptions; // آرایه انتخاب شده برای زیرمجموعه‌ها
    private readonly string Div = "_";

    [MenuItem("Tools/Rename Prefabs")]
    public static void ShowWindow()
    {
        GetWindow<RenamePrefabsWindow>("Rename Prefabs");
    }

    private void OnGUI()
    {
        GUILayout.Label("Enter New Name Pattern or Select from Options", EditorStyles.boldLabel);

        // تکست باکس برای وارد کردن نام جدید
        newNamePattern = EditorGUILayout.TextField("Custom Name Pattern:", newNamePattern);

        // رادیو باتن‌ها برای انتخاب نام پیش‌فرض والد
        GUILayout.Label("Select a default name pattern for Parent:", EditorStyles.label);
        for (int i = 0; i < parentNameOptions.Length; i++)
        {
            if (GUILayout.Toggle(selectedParentOptionIndex == i, parentNameOptions[i]))
            {
                selectedParentOptionIndex = i; // به‌روزرسانی ایندکس گزینه انتخاب‌شده والد
            }
        }

        // چک باکس‌ها برای انتخاب نام پیش‌فرض زیرمجموعه
        GUILayout.Label("Select default name patterns for Children:", EditorStyles.label);
        if (selectedChildOptions == null || selectedChildOptions.Length != childNameOptions.Length)
        {
            selectedChildOptions = new bool[childNameOptions.Length]; // ایجاد آرایه انتخاب شده
        }

        for (int i = 0; i < childNameOptions.Length; i++)
        {
            selectedChildOptions[i] = GUILayout.Toggle(selectedChildOptions[i], childNameOptions[i]);
        }

        // دکمه تغییر نام
        if (GUILayout.Button("Rename Selected Prefabs"))
        {
            RenameSelectedPrefabs();
        }
    }

    private void RenameSelectedPrefabs()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        int counter = 1; // شمارنده برای شماره‌گذاری

        foreach (GameObject selectedObject in selectedObjects)
        {
            if (PrefabUtility.IsPartOfPrefabInstance(selectedObject))
            {
                // تغییر نام پرفب
                string newName = string.IsNullOrEmpty(newNamePattern) ? parentNameOptions[selectedParentOptionIndex] + Div + counter : newNamePattern + Div + counter;
                selectedObject.name = newName;
                Debug.Log($"Renamed {selectedObject.name} to {newName}");

                // تغییر نام زیرمجموعه‌ها فقط اگر چک باکس انتخاب شده باشد
                Transform[] children = selectedObject.GetComponentsInChildren<Transform>(true);
                for (int i = 1; i < children.Length; i++) // شروع از 1 برای نادیده گرفتن خود پرفب
                {
                    // بررسی اینکه آیا چک باکس مربوط به این زیرمجموعه انتخاب شده است
                    if (i - 1 < selectedChildOptions.Length && selectedChildOptions[i - 1]) // بررسی ایندکس
                    {
                        string childNewName = string.IsNullOrEmpty(newNamePattern) ? childNameOptions[i - 1] + Div + counter : newNamePattern + Div + counter;
                        children[i].name = childNewName;
                        Debug.Log($"Renamed child {children[i].name} to {childNewName}");
                    }
                }

                counter++; // افزایش شمارنده برای نام بعدی
            }
        }

        // بستن پنجره پس از تغییر نام
        //Close();
    }
}