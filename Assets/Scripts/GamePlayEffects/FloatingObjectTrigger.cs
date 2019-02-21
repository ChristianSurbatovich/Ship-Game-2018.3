using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShipGame.Destruction;

namespace ShipGame
{
    public class FloatingObjectTrigger : MonoBehaviour
    {
        [SerializeField]
        private FloatingObjectSystem floatSystem;
        private void OnCollisionEnter(Collision collision)
        {
            FloatingObjectData objectData = collision.gameObject.GetComponent<FloatingObjectData>();
            if (objectData && objectData.floating == false && objectData.sinking == false)
            {
                objectData.StartFloating(floatSystem);
            }
        }
    }
}

