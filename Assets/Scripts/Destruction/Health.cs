using UnityEngine;
using System;

namespace ShipGame.Destruction
{
    [Serializable]
    public class Health : MonoBehaviour, ObjectHealth
    {
        private float strength;
        private float effectiveDamage;
        public float health;
        [SerializeField]
        private bool alive;
        public bool destroyable = true;
        public bool player;
        public bool debris;
        public float startingHealth;
        public int thickness;
        [SerializeField]
        private Collider shipCollider;
        public Rigidbody r;
        public bool keepParent = false;
        [SerializeField]
        private DestroyableObject structure;
        public bool structural;
        public bool immune;
        public int index;
        public bool chunk = false;
        public GameObject[] deactivateObjects;
        //public actionStop[] deactivateScripts;

        public bool startStructural, startImmune;



        public void ResetToStartingValues()
        {
            immune = startImmune;
            structural = startStructural;
            alive = true;
            debris = false;
            health = startingHealth;
            if (r)
            {
                Destroy(r);
            }
            foreach(GameObject go in deactivateObjects)
            {
                go.SetActive(true);
            }
        }

        public void SetStructure(DestroyableObject root)
        {
            structure = root;
            alive = true;
            debris = false;
            shipCollider = GetComponent<Collider>();
        }

        public void SetUp(DestroyableObject root, float startHealth)
        {
            structure = root;
            startingHealth = startHealth;
        }

        public bool isDestroyable()
        {
            return destroyable;
        }

        public float currentHealth()
        {
            return health;
        }

        public Damage damage(Damage d)
        {
            Damage k = new Damage(d);
            if (alive && !immune)
            {


                strength = 1;


                health = health - k.calculate(strength);
                if (health <= 0)
                {
                    health = 0;
                    alive = false;
                    die();
                }

            }

            return k;
        }

        public Damage damage(float d, string t)
        {
            Damage k = new Damage(d, t);
            if (alive && !immune)
            {

                strength = 1;


                health = health - k.calculate(strength);
                if (health <= 0)
                {
                    health = 0;
                    alive = false;
                    die();
                }

            }


            return k;
        }


        public void die()
        {
            if (CompareTag("Deactivate on Death"))
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
                debris = true;
                if (!r)
                {
                    r = gameObject.AddComponent<Rigidbody>();
                }


                r.mass = 1.0f;
            }
            if (structure)
            {
                structural = false;
                structure.removeEdges(this);
            }

            deactivateEffects();

        }

        public bool isAlive()
        {
            return alive;
        }
        public DestroyableObject getStructure()
        {
            return structure;
        }

        public void deactivateEffects()
        {
            for (int i = 0; i < deactivateObjects.Length; i++)
            {
                deactivateObjects[i].SetActive(false);
            }

            /*
            for(int i = 0; i < deactivateScripts.Length; i++)
            {
                deactivateScripts[i].stop();
            }
            */
        }

        public void setHealth(float h)
        {
            health = h;
        }
    }

}
