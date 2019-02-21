using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using ShipGame.Destruction;
namespace ShipGame.EditorTools
{
    public class NodeFixer : Editor
    {

        [MenuItem("My Tools/FixNodes")]

        // Update is called once per frame

        public static void SetNodes()
        {
            foreach (Transform transform in Selection.transforms)
            {

                DestroyableObject dest = transform.GetComponentInChildren<DestroyableObject>();
                int treeindex = 0;
                foreach (TreeNode t in dest.allNodes)
                {
                    t.treeIndex = treeindex++;
                    Health h = t.GetComponent<Health>();
                    if (h != null)
                    {
                        t.healthIndex = h.index;
                    }
                }
            }

        }

    }
}

