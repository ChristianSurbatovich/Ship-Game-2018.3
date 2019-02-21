using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ShipGame.Destruction;
namespace ShipGame.EditorTools
{
    public class DisableNodes : Editor
    {

        [MenuItem("My Tools/DisableNodes")]

        // Update is called once per frame

        public static void RecursiveActivate()
        {
            foreach (Transform transform in Selection.transforms)
            {

                DeactivateNodes(transform);
            }

        }

        private static void DeactivateNodes(Transform t)
        {

            foreach (Transform transform in t)
            {
                DeactivateNodes(transform);
            }
            TreeNode node = t.GetComponent<TreeNode>();
            if (node && !node.baseRoot)
            {
                t.gameObject.SetActive(false);
            }


        }
    }
}
