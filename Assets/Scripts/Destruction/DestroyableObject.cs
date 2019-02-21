using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using ShipGame.Network;
using System;
namespace ShipGame.Destruction
{


    [System.Serializable]
    public class ListWrapper
    {
        public List<int> inner;
        public int this[int index]
        {
            get
            {
                return inner[index];
            }
            set
            {
                inner[index] = value;
            }
        }

        public ListWrapper()
        {
            inner = new List<int>();
        }

        public ListWrapper(ListWrapper original)
        {
            inner = new List<int>(original.inner);
        }


        public int Count { get { return inner.Count; } }

        public void Remove(int index)
        {
            inner.Remove(index);
        }

        public void Clear()
        {
            inner.Clear();
        }

        public void Add(int value)
        {
            inner.Add(value);
        }
        public bool Contains(int value)
        {
            return inner.Contains(value);
        }

        public static ListWrapper[] Copy(ListWrapper[] old)
        {
            ListWrapper[] newList = new ListWrapper[old.Length];

            for (int i = 0; i < old.Length; i++)
            {
                newList[i] = new ListWrapper(old[i]);
            }

            return newList;
        }
    }



    [System.Serializable]
    public class DestroyableObject : MonoBehaviour
    {
        public static int frameBudget = 100;
        public static int activeDestructionObjects = 0;
        private int myBudget;
        public Health[] fragments;
        [SerializeField]
        public ListWrapper[] edges;
        public List<TreeNode> roots;
        public TreeNode[] nodes;
        public int[] parents;
        public int[] treeSize;
        public bool modified = false;
        public bool[] visited, activeItem;
        public float mass;
        public int frameDelay;
        public int updateWait;  
        public float healthMin, healthMax;
        public bool useRandomHealth;
        public string graphName;
        public DestructionGraph graphGenerator;
        public int parent1, parent2;
        Collider[] componentColliders;
        public TreeNode[] allNodes;
        public float structuralIntegrity;
        private bool updateRoutineRunning = false;
        private ListWrapper[] startingEdges;
        private int[] startingRoots;
        private List<short> modifiedVertices;
        public bool controlledByPlayer;
        private void Awake()
        {
            modifiedVertices = new List<short>();
        }
        private void Start()
        {
            startingEdges = ListWrapper.Copy(edges);
            startingRoots = new int[roots.Count];
            for(int i = 0; i < roots.Count; i++)
            {
                startingRoots[i] = roots[i].treeIndex;
            }
            for(int i = 0; i < activeItem.Length; i++)
            {
                activeItem[i] = true;
            }
        }

        private void SaveStartState() {




        }

        public void FullDestructionReset()
        {
            edges = ListWrapper.Copy(startingEdges);
            roots.Clear();
            foreach(int i in startingRoots)
            {
                roots.Add(allNodes[i]);
            }
            foreach(TreeNode r in roots)
            {
                r.ResetToStartingValues();
            }
        }

        public void RepairDamage(float percent)
        {

        }
       
        public List<int>[] generateGraph()
        {
            List<int>[] newList = new List<int>[fragments.Length];
            for (int i = 0; i < fragments.Length; i++)
            {
                newList[i] = new List<int>();
                fragments[i].gameObject.GetComponent<BoxCollider>().size *= 1.08F;
            }
            for (int i = 0; i < fragments.Length; i++)
            {
                if (fragments[i].isDestroyable())
                {
                    Collider c = fragments[i].GetComponent<Collider>();
                    Collider[] hits = Physics.OverlapBox(c.bounds.center, c.bounds.extents);
                    if (hits != null)
                    {
                        for (int j = 0; j < hits.Length; j++)
                        {
                            Health h = hits[j].GetComponent<Health>();
                            if (h && h.getStructure() == this && i != h.index && !newList[i].Contains(h.index) && h.isDestroyable())
                            {
                                newList[i].Add(h.index);
                                newList[h.index].Add(i);
                            }
                        }
                    }


                    // fragments[i].gameObject.GetComponent<BoxCollider>().size *= 0.9F;
                }
            }
            for (int i = 0; i < fragments.Length; i++)
            {
                fragments[i].gameObject.GetComponent<BoxCollider>().size *= 0.85F;
            }
            return newList;
        }
        // Update is called once per frame
        /*
        void Update()
        {
            if (!modified || frameDelay > 0)
            {
                frameDelay--;
                return;
            }   
            frameDelay = updateWait;
            for (int i = 0; i < edges.Length; i++)
            {
                parents[i] = i;
                visited[i] = false;
                treeSize[i] = 1;
            }
            for (int i = 0; i < edges.Length; i++)
            {

                for (int j = 0; j < edges[i].Count; j++)
                {
                    if (fragments[parents[i]].structural)
                    {
                        break;
                    }
                    union(i, edges[i][j]);
                }

            }

            for (int i = 0; i < fragments.Length; i++)
            {
                // if the fragments parent is not structural then the fragment should be parented ingame 
                if (parents[i] != i && !fragments[parents[i]].structural)
                {
                    fragments[i].transform.SetParent(fragments[parents[i]].transform);
                    if (fragments[i].r)
                    {
                        Destroy(fragments[i].r);
                    }

                    if (!nodes[i].root)
                    {
                        addRoot(nodes[i]);
                        nodes[i].parent.Deactivate();
                    }
                    if (activeItem[i])
                    {
                        activeItem[i] = false;
                        fragments[i].deactivateEffects();
                    }
                    // if the fragment is the root of a chunk that will break off than it should be parented to the destruction object manager and any higher-level parents should be deactivated
                }
                else if (parents[i] == i && !fragments[i].structural)
                {
                    fragments[i].chunk = true;
                    if (!nodes[i].root)
                    {
                        nodes[i].parent.Deactivate();
                        addRoot(nodes[i]);
                    }
                    if (!nodes[i].debris)
                    {
                        nodes[i].debris = true;
                        dbm.Register(nodes[i]);
                    }
                    if (activeItem[i])
                    {
                        activeItem[i] = false;
                        fragments[i].deactivateEffects();
                    }
                }

            }

            for (int i = 0; i < fragments.Length; i++)
            {
                if (!nodes[parents[i]].active)
                {
                    nodes[i].active = false;
                }
            }

            // go through the parents and if they aren't structural then give them a rigidbody and let them break off
            for (int i = 0; i < fragments.Length; i++)
            {
                if (parents[i] == i && !fragments[i].structural && nodes[i].active)
                {
                    Rigidbody rb = fragments[i].r;
                    if (rb)
                    {
                        rb.isKinematic = false;
                        rb.constraints = RigidbodyConstraints.None;
                    }
                    else
                    {
                        rb = fragments[i].gameObject.AddComponent<Rigidbody>();
                        fragments[i].r = rb;
                        rb.isKinematic = false;
                        rb.constraints = RigidbodyConstraints.None;
                    }
                    // also mark all the child objects as part of a chunk

                    mass = 1.0f;
                    for (int j = 0; j < fragments.Length; j++)
                    {
                        if (parents[j] == i && i != j && nodes[j].active)
                        {
                            fragments[j].chunk = true;
                            fragments[j].gameObject.SetActive(true);
                            if (nodes[j].root == false)
                            {
                                addRoot(nodes[j]);
                            }

                            mass += 1.0f;
                        }
                    }
                    rb.mass = mass;
                }
            }

            modified = false;


        }
        */
        public void removeEdges(Health h)
        {
            int index = h.index;
            int i;
            if (index < 0)
            {
                return;
            }


            for (i = 0; i < edges[index].Count; i++)
            {
                edges[edges[index][i]].Remove(index);
            }

            edges[index].Clear();
            parents[index] = index;
            modified = true;
        }

        int find(int i)
        {
            while (parents[i] != i)
            {
                i = parents[i];
            }
            return i;
        }

        // combine tree1 and tree2, if one of the parents is structural that one will be the parent of the new tree
        void union(int tree1, int tree2)
        {
            parent1 = find(tree1);
            parent2 = find(tree2);
            if (parent1 != parent2)
            {
                if ((fragments[parent1].structural && treeSize[parent1] < treeSize[parent2]) || (!fragments[parent2].structural && treeSize[parent1] < treeSize[parent2]) || (fragments[parent1].structural && !fragments[parent2].structural))
                {
                    parents[parent2] = parent1;
                    treeSize[parent1] += treeSize[parent2];
                    for (int i = 0; i < fragments.Length; i++)
                    {
                        if (parents[i] == parent2)
                        {
                            parents[i] = parent1;
                        }
                    }
                }
                else
                {
                    parents[parent1] = parent2;
                    treeSize[parent2] += treeSize[parent1];
                    for (int i = 0; i < fragments.Length; i++)
                    {
                        if (parents[i] == parent1)
                        {
                            parents[i] = parent2;
                        }
                    }
                }
            }



        }

        public void updateDestructionModel(Explosion exp, List<TreeNode> hits)
        {
            // check if the explosion damage radius overlaps any nodes
            for (int i = 0; i < hits.Count; i++)
            {
                hits[i].addedToHits = false;
                overlapCheck(exp, hits[i]);
            }
            // need to keep track of the roots of the destruction tree and update them when needed
            modified = true;
            if (!updateRoutineRunning)
            {
                StartCoroutine(UpdateDestruction());
            }
        }
        // need to add separate checks depending on type of object
        // move colliders to an array to avoid garbage collection on getcomponents;
        bool overlapCheck(Explosion exp, TreeNode node)
        {
            Health h = node.h;
            bool childWasDestroyed = false;
            bool hit = false;
            componentColliders = node.colliders; 
            for (int i = 0; i < componentColliders.Length; i++)
            {
                if (Vector3.Distance(exp.loc, componentColliders[i].ClosestPoint(exp.loc)) <= exp.damageRadius)
                {
                    hit = true;
                }
            }
            if (hit)
            {
                if (!h)
                {
                    // if this object got hit then check the children, if there are no children than apply damage
                    for (int i = 0; i < node.children.Count; i++)
                    {
                        node.children[i].gameObject.SetActive(true);
                        childWasDestroyed = overlapCheck(exp, node.children[i]) || childWasDestroyed;
                    }
                    if (childWasDestroyed)
                    {
                        node.dontActivate = true;
                        for (int i = 0; i < node.children.Count; i++)
                        {
                            node.children[i].Activate();
                        }
                        if (node.baseRoot && !node.forceDisable)
                        {
                            node.ShadowsOnly();
                        }
                        else
                        {
                            node.gameObject.SetActive(false);
                        }


                        if (node.root)
                        {
                           // roots.Remove(node);
                        }
                        return true;
                    }
                    else
                    {
                        /* // reparent and deactivate children
                         if (!node.root)
                         {
                             node.gameObject.transform.parent = this.gameObject.transform;
                             roots.Add(node);
                             node.root = true;
                         }*/
                        if (node.root == false)
                        {
                           node.gameObject.SetActive(false);
                        }

                        return false;
                    }

                }
                else
                {
                    // if it is not a parent than the object got hit and should be parented to the destroyable object and its parents disabled
                    if (!h.chunk)
                    {
                        node.gameObject.transform.parent = this.gameObject.transform;
                    }

                    h.damage(exp.d);
                    //addRoot(node);
                    if (node.h.r)
                    {
                        node.h.r.AddExplosionForce(exp.strength, exp.loc, exp.forceRadius, exp.upwards);
                    }
                    return true;
                }


            }
            else
            {
                if (node.root == false)
                {
                   node.gameObject.SetActive(false);
                }
                return false;
            }

        }

        public void disableTree(TreeNode node)
        {
            node.SetColliders();
            for (int i = 0; i < node.children.Count; i++)
            {
                disableTree(node.children[i]);
            }
            if (!node.root)
            {
                node.gameObject.SetActive(false);
            }
        }

        public void removeRoot(TreeNode node)
        {
            roots.Remove(node);
        }

        public void addRoot(TreeNode node)
        {
            if (node.root == false)
            {
                node.root = true;
                roots.Add(node);
            }

        }

        public void SetAsRoot(TreeNode node)
        {
            if(roots == null)
            {
                roots = new List<TreeNode>();
            }
            roots.Add(node);
        }

        private IEnumerator UpdateDestruction()
        {
            activeDestructionObjects++;
            updateRoutineRunning = true;
            modifiedVertices.Clear();
            while (modified)
            {
                modified = false;
                yield return 0;
                myBudget = frameBudget / activeDestructionObjects;
                for (int i = 0; i < edges.Length; i++)
                {
                    parents[i] = i;
                    visited[i] = false;
                    treeSize[i] = 1;
                }
                for (int i = 0; i < edges.Length; i++)
                {

                    for (int j = 0; j < edges[i].Count; j++)
                    {
                        if (fragments[parents[i]].structural)
                        {
                            break;
                        }
                        union(i, edges[i][j]);
                    }
                    myBudget--;
                    if (myBudget < 0)
                    {
                        yield return 0;
                        myBudget = frameBudget / activeDestructionObjects;
                    }
                }      

                for (int i = 0; i < fragments.Length; i++)
                {
                    // if the fragments parent is not structural then the fragment should be parented ingame 
                    if (parents[i] != i && !fragments[parents[i]].structural)
                    {
                        modifiedVertices.Add((short)nodes[i].treeIndex);
                        fragments[i].transform.SetParent(fragments[parents[i]].transform);
                        if (fragments[i].r)
                        {
                            Destroy(fragments[i].r);
                        }

                        if (!nodes[i].root)
                        {
                            nodes[i].parent.Deactivate();
                        }
                        if (activeItem[i])
                        {
                            activeItem[i] = false;
                            fragments[i].deactivateEffects();
                        }
                        // if the fragment is the root of a chunk that will break off than it should be parented to the destruction object manager and any higher-level parents should be deactivated
                    }
                    else if (parents[i] == i && !fragments[i].structural)
                    {
                        modifiedVertices.Add((short)nodes[i].treeIndex);
                        fragments[i].chunk = true;
                        if (!nodes[i].root)
                        {
                            nodes[i].parent.Deactivate();
                            addRoot(nodes[i]);
                        }
                        if (!nodes[i].debris)
                        {
                            nodes[i].debris = true;
                            DebrisManager.DBM.Register(nodes[i]);
                        }
                        if (activeItem[i])
                        {
                            activeItem[i] = false;
                            fragments[i].deactivateEffects();
                        }
                    }

                }

                for (int i = 0; i < fragments.Length; i++)
                {
                    if (!nodes[parents[i]].active)
                    {
                        nodes[i].active = false;
                    }
                }

                // go through the parents and if they aren't structural then give them a rigidbody and let them break off
                for (int i = 0; i < fragments.Length; i++)
                {
                    if (parents[i] == i && !fragments[i].structural && nodes[i].active)
                    {
                        Rigidbody rb = fragments[i].r;
                        if (rb)
                        {
                            rb.isKinematic = false;
                            rb.constraints = RigidbodyConstraints.None;
                        }
                        else
                        {
                            rb = fragments[i].gameObject.AddComponent<Rigidbody>();
                            fragments[i].r = rb;
                            rb.isKinematic = false;
                            rb.constraints = RigidbodyConstraints.None;
                        }
                        // also mark all the child objects as part of a chunk

                        mass = 1.0f;
                        for (int j = 0; j < fragments.Length; j++)
                        {
                            if (parents[j] == i && i != j && nodes[j].active)
                            {
                                fragments[j].chunk = true;
                                fragments[j].gameObject.SetActive(true);
                                if (nodes[j].root == false)
                                {
                                    addRoot(nodes[j]);
                                }

                                mass += 1.0f;
                            }
                        }
                        rb.mass = mass;
                    }
                }
            }
            if (controlledByPlayer)
            {
                SendStateChange();
            }
            activeDestructionObjects--;
            updateRoutineRunning = false;
        }

        private void SendStateChange()
        {
      /*      Array.Copy(BitConverter.GetBytes((short)(modifiedVertices.Count * 2 + 3)), 0, NetworkManager.messageBuffer, 0, 2);
            NetworkManager.messageBuffer[2] = MessageValues.DESTRUCTION_STATE;
            Array.Copy(BitConverter.GetBytes(NetworkManager.myNetID), 0, NetworkManager.messageBuffer, 3, 2);
            for(int i = 0; i < modifiedVertices.Count; i++)
            {
                Array.Copy(BitConverter.GetBytes(modifiedVertices[i]), 0, NetworkManager.messageBuffer, i * 2 + 5, 2);
            }
            NetworkManager.Net.SendNetworkMessage(NetworkManager.messageBuffer);*/
            modifiedVertices.Clear();
        }

        public void ApplyStateChange(short[] vertices)
        {
            foreach(short s in vertices)
            {
                removeEdges(allNodes[s].h);
            }
            if (!updateRoutineRunning)
            {
                StartCoroutine(UpdateDestruction());
            }
        }
    }

}

/*
       void Awake()
       {
           dbm = FindObjectOfType<DebrisManager>();
           fragments = GetComponentsInChildren<Health>();
           visited = new bool[fragments.Length];
           nodes = new TreeNode[fragments.Length];
           edges = new List<int>[fragments.Length];
           parents = new int[fragments.Length];
           activeItem = new bool[fragments.Length];
           treeSize = new int[fragments.Length];
           for (int i = 0; i < fragments.Length; i++)
           {
               visited[i] = false;
               fragments[i].index = i;
               nodes[i] = fragments[i].GetComponent<TreeNode>();
               edges[i] = new List<int>();
               activeItem[i] = true;
               parents[i] = i;
               fragments[i].gameObject.GetComponent<BoxCollider>().size *= 1.05F;
           }
           if(roots == null)
           {
               roots = new List<TreeNode>();
           }
           modified = true;
       }

       // Use this for initialization
       void Start()
       {

           for (int i = 0; i < fragments.Length; i++)
           {
               if (fragments[i].isDestroyable())
               {
                   Collider c = fragments[i].GetComponent<Collider>();
                   Collider[] hits = Physics.OverlapBox(c.bounds.center, c.bounds.extents);
                   if (hits != null)
                   {
                       for (int j = 0; j < hits.Length; j++)
                       {
                           Health h = hits[j].GetComponent<Health>();
                           if (h && h.getStructure() == this && i != h.index && !edges[i].Contains(h.index) && h.isDestroyable())
                           {
                               edges[i].Add(h.index);
                               edges[h.index].Add(i);
                           }else if (hits[j].GetComponent<TerrainCollider>() != null)
                           {
                              fragments[i].structural = true;
                           }
                       }
                   }


                   // fragments[i].gameObject.GetComponent<BoxCollider>().size *= 0.9F;
               }
           }
           for (int i = 0; i < fragments.Length; i++)
           {
               fragments[i].gameObject.GetComponent<BoxCollider>().size *= 0.85F;
           }

           for(int i = 0; i < roots.Count; i++)
           {
               disableTree(roots[i]);
           }

           if (useRandomHealth)
           {
               for (int i = 0; i < fragments.Length; i++)
               {
                   fragments[i].setHealth(Mathf.Ceil(Random.Range(healthMin, healthMax)));
               }
           }
       }
       */
