using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using ShipGame.Destruction;

namespace ShipGame.EditorTools
{
    public class DestroyableObjectSetup : Editor
    {

        [MenuItem("My Tools/Static Copy...")]

        // Update is called once per frame

        static void CopyToDynamic()
        {
            GameObject tempObj;
            foreach (Transform transform in Selection.transforms)
            {

                tempObj = new GameObject(transform.gameObject.name);
                GameObjectUtility.SetParentAndAlign(tempObj, transform.gameObject);
                // copy the components over
                foreach (Component component in transform.gameObject.GetComponents<Component>())
                {
                    ComponentUtility.CopyComponent(component);
                    ComponentUtility.PasteComponentAsNew(tempObj);
                }
                DestroyImmediate(transform.gameObject.GetComponent<Health>());
                transform.gameObject.isStatic = true;
            }

        }
    }
}

