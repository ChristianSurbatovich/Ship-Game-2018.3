using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class SoundPlayer : MonoBehaviour
    {
        public AudioClip[] sounds;
        public string[] keys;
        public string sourceName;
        private Dictionary<string, List<AudioClip>> soundPool;
        private ObjectPool pooler;
        private AudioSource source;
        // Use this for initialization
        void Start()
        {
            pooler = FindObjectOfType<ObjectPool>();
            soundPool = new Dictionary<string, List<AudioClip>>();
            // map the 
            for (int i = 0; i < sounds.Length; i++)
            {
                if (!soundPool.ContainsKey(keys[i]))
                {
                    soundPool[keys[i]] = new List<AudioClip>();
                }
                soundPool[keys[i]].Add(sounds[i]);
            }

        }

        public void PlaySound(string soundToPlay, Vector3 soundLocation, float time, Transform t = null)
        {
            int clip = Mathf.FloorToInt(Random.Range(0, soundPool[soundToPlay].Count));
            source = pooler.getObject<AudioSource>(sourceName, time);
            source.transform.SetParent(t);
            source.transform.position = soundLocation;
            source.PlayOneShot(soundPool[soundToPlay][clip]);
        }

        public void PlaySound(string soundToPlay, Vector3 soundLocation, bool loop, Transform t = null)
        {
            int clip = Mathf.FloorToInt(Random.Range(0, soundPool[soundToPlay].Count));
            source = pooler.getObject<AudioSource>(sourceName);
            source.transform.SetParent(t);
            source.transform.position = soundLocation;
            source.PlayOneShot(soundPool[soundToPlay][clip]);
        }

        public void PlaySound(string soundToPlay, Vector3 soundLocation, Transform t = null)
        {
            int clip = Mathf.FloorToInt(Random.Range(0, soundPool[soundToPlay].Count));
            source = pooler.getObject<AudioSource>(sourceName, 15);
            source.transform.SetParent(t);
            source.transform.position = soundLocation;
            source.PlayOneShot(soundPool[soundToPlay][clip]);
        }
    }

}
