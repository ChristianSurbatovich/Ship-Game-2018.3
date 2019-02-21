using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using ShipGame.Destruction;
using UnityEditor.SceneManagement;
public class SetArrays{

    [MenuItem("Destruction/SetArrays")]
    static void SetGraphArrays()
    {
        foreach (Transform T in Selection.transforms)
        {
            DestroyableObject root = T.GetComponentInChildren<DestroyableObject>();
            if (root)
            {
                Undo.RecordObject(root, "root");
                root.fragments = root.GetComponentsInChildren<Health>();
                root.visited = new bool[root.fragments.Length];
                root.nodes = new TreeNode[root.fragments.Length];
                root.edges = new ListWrapper[root.fragments.Length];
                root.parents = new int[root.fragments.Length];
                root.activeItem = new bool[root.fragments.Length];
                root.treeSize = new int[root.fragments.Length];
                root.allNodes = root.GetComponentsInChildren<TreeNode>();
                root.roots = new List<TreeNode>();
                Undo.RecordObjects(root.fragments, "fragments");
                Undo.RecordObjects(root.allNodes, "nodes");

                for (int i = 0; i < root.fragments.Length; i++)
                {
                    root.visited[i] = false;
                    root.fragments[i].index = i;
                    root.fragments[i].SetStructure(root);
                    root.nodes[i] = root.fragments[i].GetComponent<TreeNode>();
                    root.edges[i] = new ListWrapper();
                    root.activeItem[i] = true;
                    root.parents[i] = i;

                    root.fragments[i].gameObject.GetComponent<BoxCollider>().size *= 1.1F;
                }
                root.modified = true;

                int treeIndex = 0;
                foreach (TreeNode t in root.allNodes)
                {
                    t.children = new List<TreeNode>();
                    t.parent = t.transform.parent.GetComponent<TreeNode>();
                    t.colliders = t.GetComponents<Collider>();
                    t.treeIndex = treeIndex++;
                    if (t.parent == null)
                    {
                        t.root = true;
                    }
                    else
                    {
                        t.parent.children.Add(t);
                        /*
                        MeshRenderer mr = GetComponent<MeshRenderer>();
                        if (mr)
                        {
                            GetComponent<MeshRenderer>().receiveShadows = false;
                            GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                        }
                        */
                    }
                    if (t.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        t.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    }
                    t.h = t.GetComponent<Health>();
                    if (t.h != null)
                    {
                        t.hasHealth = true;
                        t.healthIndex = t.h.index;
                    }
                    t.debris = false;
                    t.active = true;
                    t.manager = t.GetComponentInParent<DestroyableObject>();
                    if (t.root)
                    {
                        root.SetAsRoot(t);
                        t.baseRoot = true;
                    }

                    t.startRoot = t.root;
                    t.startDontActivate = t.dontActivate;
                    t.startHasHealth = t.hasHealth;
                    t.startDebris = t.debris;
                    t.startActive = t.active;
                    t.startGameObjectActive = t.baseRoot;
                    t.startPosition = t.transform.localPosition;
                    t.startScale = t.transform.localScale;
                    t.startRotation = t.transform.localRotation;
                    t.parentObject = t.transform.parent;
                }

                for (int i = 0; i < root.fragments.Length; i++)
                {
                    if (root.fragments[i].isDestroyable())
                    {
                        Collider c = root.fragments[i].GetComponent<Collider>();
                        Collider[] hits = Physics.OverlapBox(c.bounds.center, c.bounds.extents);
                        if (hits != null)
                        {
                            for (int j = 0; j < hits.Length; j++)
                            {
                                Health h = hits[j].GetComponent<Health>();
                                if (h && h.getStructure() == root && i != h.index && !root.edges[i].Contains(h.index) && h.isDestroyable())
                                {
                                    root.edges[i].Add(h.index);
                                    root.edges[h.index].Add(i);
                                }
                                else if (hits[j].GetComponent<TerrainCollider>() != null)
                                {
                                    root.fragments[i].structural = true;
                                }
                            }
                        }


                        // fragments[i].gameObject.GetComponent<BoxCollider>().size *= 0.9F;
                    }
                }
                for (int i = 0; i < root.fragments.Length; i++)
                {
                    root.fragments[i].gameObject.GetComponent<BoxCollider>().size *= (1.0f/1.1f);
                }

                for (int i = 0; i < root.roots.Count; i++)
                {
                    root.disableTree(root.roots[i]);
                }

                if (root.useRandomHealth)
                {
                    for (int i = 0; i < root.fragments.Length; i++)
                    {
                        root.fragments[i].startingHealth = Mathf.Ceil(Random.Range(root.healthMin, root.healthMax));
                        root.fragments[i].setHealth(root.fragments[i].startingHealth);
                    }
                }

                for (int i = 0; i < root.fragments.Length; i++)
                {
                    Health temp = root.fragments[i];
                    temp.startStructural = temp.structural;
                    temp.startImmune = temp.immune;
                }

                EditorUtility.SetDirty(root);
                foreach (TreeNode t in root.allNodes)
                {
                    EditorUtility.SetDirty(t);
                }
                foreach (Health h in root.fragments)
                {
                    EditorUtility.SetDirty(h);
                }

            }
        }
    }
}
