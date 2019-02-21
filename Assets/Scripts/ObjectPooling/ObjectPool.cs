using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class ObjectPool : MonoBehaviour
    {
        //public GameObject[] objectTypes;
        public int[] poolAmounts;
        public GameObject[] objTypes;
        public int[] particleAmounts;
        public ParticleSystem[] particleSystemTypes;
        public string[] keys, particleKeys;
        private Dictionary<string, GameObject> objectTypes;
        private Dictionary<string, List<GameObject>> pool;
        private Dictionary<string, ParticleSystem> particleTypes;
        private Dictionary<string, List<ParticleSystem>> particlePool;

        private void Awake()
        {
            int j = 0;
            GameObject newObject;
            ParticleSystem newSystem;
            objectTypes = new Dictionary<string, GameObject>();
            particleTypes = new Dictionary<string, ParticleSystem>();
            for (int i = 0; i < objTypes.Length; i++)
            {
                objectTypes[keys[i]] = objTypes[i];
            }
            pool = new Dictionary<string, List<GameObject>>();
            foreach (var entry in objectTypes)
            {
                pool[entry.Key] = new List<GameObject>();
                for (int i = 0; i < poolAmounts[j]; i++)
                {
                    newObject = Instantiate(entry.Value);
                    newObject.transform.SetParent(transform);
                    newObject.SetActive(false);
                    pool[entry.Key].Add(newObject);
                }
                j++;
            }

            for (int i = 0; i < particleSystemTypes.Length; i++)
            {
                particleTypes[particleKeys[i]] = particleSystemTypes[i];
            }

            particlePool = new Dictionary<string, List<ParticleSystem>>();
            j = 0;
            foreach (var entry in particleTypes)
            {
                particlePool[entry.Key] = new List<ParticleSystem>();
                for (int i = 0; i < particleAmounts[j]; i++)
                {
                    newSystem = Instantiate<ParticleSystem>(entry.Value);
                    newSystem.Stop();
                    newSystem.Clear();
                    newSystem.transform.SetParent(transform);
                    newSystem.gameObject.SetActive(false);
                    particlePool[entry.Key].Add(newSystem);
                }
                j++;
            }
        }

        // Use this for initialization
        void Start()
        {

        }


        public GameObject getObject(string key)
        {
            GameObject objectToReturn = null;
            if (pool.ContainsKey(key))
            {
                for (int i = 0; i < pool[key].Count; i++)
                {
                    if (pool[key][i].activeInHierarchy == false)
                    {
                        objectToReturn = pool[key][i];
                    }
                }
                // if no free objects found then add one
                if (objectToReturn == null)
                {
                    objectToReturn = Instantiate<GameObject>(objectTypes[key]);
                    pool[key].Add(objectToReturn);
                }
                objectToReturn.SetActive(true);
                return objectToReturn;
            }
            // there was no gameobject that matches the string passed
            return null;
        }

        public T getObject<T>(string key)
        {
            GameObject objectToReturn = null;
            if (pool.ContainsKey(key))
            {
                for (int i = 0; i < pool[key].Count; i++)
                {
                    if (pool[key][i].activeInHierarchy == false)
                    {
                        objectToReturn = pool[key][i];
                    }
                }
                // if no free objects found then add one
                if (objectToReturn == null)
                {
                    objectToReturn = Instantiate<GameObject>(objectTypes[key]);
                    pool[key].Add(objectToReturn);
                }
                objectToReturn.SetActive(true);
                return objectToReturn.GetComponent<T>();
            }
            // there was no gameobject that matches the string passed
            return default(T);
        }

        // get a gameobject for a period of time
        public GameObject getObject(string key, float time)
        {
            GameObject objectToReturn = null;
            if (pool.ContainsKey(key))
            {
                for (int i = 0; i < pool[key].Count; i++)
                {
                    if (pool[key][i].activeInHierarchy == false)
                    {
                        objectToReturn = pool[key][i];
                    }
                }
                // if no free objects found then add one
                if (objectToReturn == null)
                {
                    objectToReturn = Instantiate<GameObject>(objectTypes[key]);
                    pool[key].Add(objectToReturn);
                }
                objectToReturn.SetActive(true);
                StartCoroutine(gameTimer.DeactivateAndReturn(time, objectToReturn, transform));
                return objectToReturn;
            }
            Debug.Log("Key: " + key + " not found");
            // there was no gameobject that matches the string passed
            return null;
        }

        // get a copy of the component for a period of time
        public T getObject<T>(string key, float time)
        {
            GameObject objectToReturn = null;
            if (pool.ContainsKey(key))
            {
                for (int i = 0; i < pool[key].Count; i++)
                {
                    if (pool[key][i].activeInHierarchy == false)
                    {
                        objectToReturn = pool[key][i];
                    }
                }
                // if no free objects found then add one
                if (objectToReturn == null)
                {
                    objectToReturn = Instantiate<GameObject>(objectTypes[key]);
                    pool[key].Add(objectToReturn);
                }
                objectToReturn.SetActive(true);
                StartCoroutine(gameTimer.DeactivateAndReturn(time, objectToReturn, transform));
                return objectToReturn.GetComponent<T>();
            }
            // there was no gameobject that matches the string passed
            return default(T);
        }

        public ParticleSystem getParticleSystem(string key)
        {
            ParticleSystem SystemToReturn = null;
            if (particlePool.ContainsKey(key))
            {
                for (int i = 0; i < particlePool[key].Count; i++)
                {
                    if (particlePool[key][i].gameObject.activeInHierarchy == false)
                    {
                        SystemToReturn = particlePool[key][i];
                    }
                }
                // if no free objects found then add one
                if (SystemToReturn == null)
                {
                    SystemToReturn = Instantiate<ParticleSystem>(particleTypes[key]);
                    particlePool[key].Add(SystemToReturn);
                }
                SystemToReturn.gameObject.SetActive(true);
                return SystemToReturn;
            }
            // there was no gameobject that matches the string passed
            return null;
        }

        public ParticleSystem getParticleSystem(string key, float time)
        {
            ParticleSystem SystemToReturn = null;
            if (particlePool.ContainsKey(key))
            {
                for (int i = 0; i < particlePool[key].Count; i++)
                {
                    if (particlePool[key][i].gameObject.activeInHierarchy == false)
                    {
                        SystemToReturn = particlePool[key][i];
                    }
                }
                // if no free objects found then add one
                if (SystemToReturn == null)
                {
                    SystemToReturn = Instantiate<ParticleSystem>(particleTypes[key]);
                    particlePool[key].Add(SystemToReturn);
                }
                SystemToReturn.gameObject.SetActive(true);
                StartCoroutine(gameTimer.DeactivateAndReturn(time, SystemToReturn.gameObject, transform));
                return SystemToReturn;
            }
            // there was no gameobject that matches the string passed
            return null;
        }
    }
}

