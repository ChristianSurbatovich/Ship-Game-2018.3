using UnityEngine;
using UnityEditor;

namespace ShipGame.EditorTools
{
    public class Recursive : Editor
    {

        [MenuItem("My Tools/SetAllActive")]

        // Update is called once per frame

        public static void RecursiveActivate()
        {
            foreach (Transform transform in Selection.transforms)
            {

               ActivateAll(transform);
            }

        }

        private static void ActivateAll(Transform t)
        {
            t.gameObject.SetActive(true);
            foreach (Transform transform in t)
            {
                ActivateAll(transform);
            }
        }
    }
}