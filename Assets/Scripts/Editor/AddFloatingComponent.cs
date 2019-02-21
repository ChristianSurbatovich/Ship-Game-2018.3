using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using ShipGame.Destruction;
using UnityEditor.SceneManagement;

namespace ShipGame.EditorTools
{
    public class AddFloatingComponent : EditorWindow
    {
        static Dictionary<string, bool> materialList;
        string materialName;
        bool shouldFloat;
        [MenuItem("Destruction/AddFloatingComponents")]
        static void Init()
        {
            materialList = new Dictionary<string, bool>();
            materialList["Dark Wood"] = true;
            materialList["Light Wood"] = true;
            materialList["Medium Wood"] = true;
            materialList["Wood"] = true;
            materialList["Sail"] = true;
            materialList["Palm Tree"] = true;
            var window = (AddFloatingComponent)GetWindow(typeof(AddFloatingComponent));
            window.Show();
        }

        private void OnGUI()
        {
            materialName = EditorGUILayout.TextField("Material Name: ", materialName);
            shouldFloat = EditorGUILayout.Toggle("Should Objects with this material float: ", shouldFloat);
            if(GUILayout.Button("Add Material"))
            {
                materialList.Add(materialName, shouldFloat);
            }
            if (GUILayout.Button("Add Components"))
            {
                if (!Selection.activeGameObject)
                {
                    Debug.Log("Select a GameObject");
                    return;
                }

                ComponentHelper(Selection.activeTransform);
            }
            this.Repaint();
        }

        private void ComponentHelper(Transform t)
        {
            Health h = t.GetComponent<Health>();
            FloatingObjectData objectData;
            if (h)
            {
                objectData = t.gameObject.GetComponent<FloatingObjectData>();
                if (objectData == null)
                {
                    objectData = t.gameObject.AddComponent<FloatingObjectData>();
                }
                t.GetComponent<TreeNode>().floatingData = objectData;
                MeshRenderer mr = t.GetComponent<MeshRenderer>();
                if (materialList.ContainsKey(mr.sharedMaterial.name))
                {
                    objectData.shouldFloat = materialList[mr.sharedMaterial.name];
                }
            }
            foreach (Transform transform in t)
            {
                ComponentHelper(transform);
            }
            

        }
    }
}
