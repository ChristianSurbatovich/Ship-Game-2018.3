using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShipGame;
namespace ShipGame.Destruction
{
    [System.Serializable]
    public class TreeNode : MonoBehaviour
    {
        public TreeNode parent;
        public bool root, dontActivate, hasHealth, debris, active;
        [SerializeField]
        public List<TreeNode> children;
        public DestroyableObject manager;
        public Health h;
        public Collider[] colliders;
        public bool baseRoot = false;
        public MeshRenderer[] mr;
        public bool forceDisable = false;
        public int treeIndex, healthIndex;
        public bool startRoot, startDontActivate, startHasHealth, startDebris, startActive, startGameObjectActive;
        public Vector3 startPosition, startScale;
        public Quaternion startRotation;

        public bool managedByDebrisHolder, addedToHits;
        public DebrisHolder holder;
        public Transform parentObject;

        public FloatingObjectData floatingData;


        public void ResetToStartingValues()
        {
            if (managedByDebrisHolder)
            {
                holder.ReturnObject();
            }
            if (transform.parent != parentObject)
            {
                transform.parent = parentObject.transform;
            }
            root = startRoot;
            dontActivate = startDontActivate;
            debris = startDebris;
            active = startActive;
            transform.localPosition = startPosition;
            transform.localScale = startScale;
            transform.localRotation = startRotation;
            gameObject.SetActive(startGameObjectActive);
            if (hasHealth)
            {
                h.ResetToStartingValues();
            }
            for (int i = 0; i < children.Count; i++)
            {
                children[i].ResetToStartingValues();
            }
        }

        public void Setup()
        {

            parent = transform.parent.GetComponent<TreeNode>();
            colliders = GetComponents<Collider>();
            if (parent == null)
            {
                root = true;
            }
            else
            {
                parent.children.Add(this);
                /*
                MeshRenderer mr = GetComponent<MeshRenderer>();
                if (mr)
                {
                    GetComponent<MeshRenderer>().receiveShadows = false;
                    GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                }
                */
            }
            if (gameObject.GetComponent<Rigidbody>() != null)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            h = GetComponent<Health>();
            if (h != null)
            {
                hasHealth = true;
            }
            debris = false;
            active = true;
            manager = GetComponentInParent<DestroyableObject>();
            if (root)
            {
                manager.SetAsRoot(this);
                baseRoot = true;
            }

        }

        public bool IsParent()
        {
            return children.Count > 0;
        }

        public void Activate()
        {
            if (dontActivate)
            {
                return;
            }
            gameObject.SetActive(true);
            if (!root)
            {
                manager.addRoot(this);
                transform.SetParent(manager.transform);
            }

        }

        public void Deactivate()
        {
            dontActivate = true;
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Activate();
            }
            if (!root)
            {
                parent.Deactivate();
            }
            else
            {
                manager.removeRoot(this);
            }
            if (baseRoot && !forceDisable)
            {
                ShadowsOnly();
            }
            else
            {
                gameObject.SetActive(false);
            }

        }

        public void SetColliders()
        {
         //   colliders = GetComponents<Collider>();
        }

        public void ShadowsOnly()
        {
            for (int i = 0; i < mr.Length; i++)
            {
                mr[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }

            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = false;
            }
        }
    }

}

/*
void Awake()
{

    if (transform.childCount > 0)
    {
        children = new List<TreeNode>();
        foreach (Transform t in transform)
        {
            if (t.GetComponent<TreeNode>())
            {
                children.Add(t.gameObject.GetComponent<TreeNode>());
            }
        }
    }
    parent = transform.parent.GetComponent<TreeNode>();

    if (parent == null)
    {
        root = true;
    }
    else
    {
        /*
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr)
        {
            GetComponent<MeshRenderer>().receiveShadows = false;
            GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }

    }
    if (gameObject.GetComponent<Rigidbody>() != null)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    h = GetComponent<Health>();
    if (h != null)
    {
        hasHealth = true;
    }
    debris = false;
    active = true;
    manager = GetComponentInParent<DestroyableObject>();
    if (root)
    {
        manager.SetAsRoot(this);
        baseRoot = true;
    }

}
*/
