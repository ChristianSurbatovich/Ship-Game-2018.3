using UnityEngine;
using System.Collections;

namespace ShipGame
{
    public class ObjectHit : MonoBehaviour
    {
        private ParticleSystem hitEffect;
        public string effectName;
        private ObjectPool pooler;
        private void Awake()
        {
            pooler = FindObjectOfType<ObjectPool>();
        }
        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Projectile"))
            {

                ContactPoint contact = other.contacts[0];
                hitEffect = pooler.getParticleSystem(effectName, 15);
                hitEffect.transform.SetParent(transform);
                hitEffect.transform.position = contact.point;
                hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.up);
            }
        }
    }
}

