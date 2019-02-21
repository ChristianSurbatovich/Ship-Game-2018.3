using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace ShipGame.EditorTools
{
    public class SetShadows : Editor
    {

        [MenuItem("My Tools/SetShadows")]

        // Update is called once per frame

        static void TurnOffShadows()
        {
            foreach (Transform transform in Selection.transforms)
            {

                ShadowHelper(transform);
            }

        }

        private static void ShadowHelper(Transform t)
        {
            MeshRenderer mr = t.GetComponent<MeshRenderer>();
            if (mr)
            {
                mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }
            foreach (Transform transform in t)
            {
                ShadowHelper(transform);
            }
        }
    }
}

