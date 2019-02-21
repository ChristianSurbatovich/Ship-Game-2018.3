using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class LevelSegment : MonoBehaviour
    {
        public float viewDistance;
        public Vector3 location;
        public bool active = true;

        private void Awake()
        {
            location = transform.position;
        }
        public void Enable()
        {
            gameObject.SetActive(true);
            active = true;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            active = false;
        }
    }

}
