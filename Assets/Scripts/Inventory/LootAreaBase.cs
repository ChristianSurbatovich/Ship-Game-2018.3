using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    [CreateAssetMenu()]
    public class LootAreaBase : ScriptableObject
    {
        public float lootRange;
        public short tier;
        public short ID;
    }
}

