using UnityEngine;
using System.Collections.Generic;

namespace ShipGame.Destruction
{
    public class Explosion : MonoBehaviour
    {
        private ParticleSystem effect;
        public string effectName;
        public float targetDamage;
        public string damageType;
        public float strength;
        public float damageRadius;
        public float forceRadius;
        public float upwards;
        public Damage d;
        public string expSound;
        public Vector3 loc;
        ObjectPool pooler;
        SoundPlayer sound;
        private Dictionary<DestroyableObject, List<TreeNode>> hits;

        void Awake()
        {
            pooler = FindObjectOfType<ObjectPool>();
            d = new Damage(targetDamage, damageType);
            sound = FindObjectOfType<SoundPlayer>();
            hits = new Dictionary<DestroyableObject, List<TreeNode>>();
        }

        public Damage[] explode(Vector3 location)
        {
            int i;
            TreeNode t;
            loc = location;
            Collider[] colliders = Physics.OverlapSphere(loc, damageRadius);
            Damage[] results = new Damage[colliders.Length];
            for (i = 0; i < colliders.Length; i++)
            {

                t = colliders[i].GetComponent<TreeNode>();
                if (t)
                {
                    if (hits.ContainsKey(t.manager))
                    {
                        // if object has multiple colliders it should only be added once
                        if (!t.addedToHits)
                        {
                            hits[t.manager].Add(t);
                            t.addedToHits = true;
                        }

                    }
                    else
                    {
                        hits[t.manager] = new List<TreeNode>();
                        hits[t.manager].Add(t);
                        t.addedToHits = true;
                    }
                }
            }
            foreach (KeyValuePair<DestroyableObject,List<TreeNode>> kvp in hits)
            {
                kvp.Key.updateDestructionModel(this, kvp.Value);
            }
            if (effectName != null)
            {
                sound.PlaySound(expSound, loc);
                //explosionEffect = pooler.getObject(effectName);
                effect = pooler.getParticleSystem(effectName, 20);
                effect.transform.SetParent(transform.parent);
                effect.transform.position = transform.position;
                effect.transform.rotation = transform.rotation;
                effect.Play();
                //explosionEffect.transform.SetParent(transform);
                //explosionEffect.GetComponent<ParticleSystem>().Play();
            }
            return results;
        }
        private void OnDisable()
        {
            hits.Clear();
        }
    }

}
