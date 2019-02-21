using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShipGame
{
    public class LevelBlockSpawner : MonoBehaviour
    {

        [SerializeField]
        private GameObject levelBlock, blockToSpawn;
        [SerializeField]
        private LevelSegmentManager segManager;

        private void Awake()
        {
            levelBlock = Instantiate(blockToSpawn);
        }
    }
}

