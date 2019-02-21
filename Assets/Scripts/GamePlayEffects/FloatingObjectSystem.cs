using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class FloatingObjectSystem : MonoBehaviour
    {
        [SerializeField]
        private float sinkSpeed, globalDepthOffset;
        private List<FloatingObjectData> floatingObjects;
        public waves waveSystem;

        private void Awake()
        {
            floatingObjects = new List<FloatingObjectData>();
        }

        public void Register(FloatingObjectData objectData)
        {
            if (objectData.shouldFloat)
            {
                objectData.floating = true;
            }
            else
            {
                objectData.sinking = true;
            }
            floatingObjects.Add(objectData);
        }

        public void Unregister(FloatingObjectData objectData)
        {
            floatingObjects.Remove(objectData);
        }

        private void FixedUpdate()  
        {
            for(int i = 0; i < floatingObjects.Count; i++){
                Vector3 newPos = floatingObjects[i].transform.position;
                if (floatingObjects[i].sinking)
                {
                    floatingObjects[i].floatOffset -= Time.fixedDeltaTime * sinkSpeed;
                }
                newPos.y = waveSystem.fineWaveHeight(floatingObjects[i].transform.position) + floatingObjects[i].floatOffset + globalDepthOffset;
                floatingObjects[i].transform.position = newPos;
            }
        }
    }

}
