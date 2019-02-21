using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class FloatingObjectData : MonoBehaviour
    {
        private FloatingObjectSystem floatSystem;
        public bool floating, sinking, shouldFloat;
        public float floatOffset;
        public Collider collider;

        public void StartFloating(FloatingObjectSystem f)
        {
            floatSystem = f;
            Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<Collider>().enabled = false;
            floatOffset = transform.position.y - floatSystem.transform.position.y; 
            floatSystem.Register(this);
        }

        public void StopFloating()
        {
            gameObject.GetComponent<Collider>().enabled = true;
            floatSystem.Unregister(this);
        }
    }
}

