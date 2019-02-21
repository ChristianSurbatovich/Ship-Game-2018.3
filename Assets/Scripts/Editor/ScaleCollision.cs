using UnityEngine;
using UnityEditor;
using ShipGame.Destruction;
namespace ShipGame.EditorTools
{
    public class ScaleCollision : EditorWindow
    {

        float scale;
        bool debrisOnly;
        [MenuItem("My Tools/ScaleCollision")]
        static void Init()
        {
            var window = (ScaleCollision)GetWindow(typeof(ScaleCollision));
            window.Show();
        }
        // Update is called once per frame

        private void OnGUI()
        {
            scale = EditorGUILayout.FloatField("New Scale: ", scale);
            debrisOnly = EditorGUILayout.Toggle("Only Scale Debris: ", debrisOnly);
            if (GUILayout.Button("Scale"))
            {
                if (!Selection.activeGameObject)
                {
                    Debug.Log("Select a GameObject");
                    return;
                }

                ScaleAll(Selection.activeTransform);
            }
        }

        private void ScaleAll(Transform t)
        {

            BoxCollider[] temp = t.GetComponents<BoxCollider>();
            if (temp != null && (!debrisOnly || t.GetComponent<Health>()))
            {
                foreach(BoxCollider bc in temp)
                {
                    Undo.RecordObject(bc, "Scaling Collider");
                    bc.size *= scale;
                }
            }
            foreach (Transform transform in t)
            {
                ScaleAll(transform);
            }
        }
    }
}