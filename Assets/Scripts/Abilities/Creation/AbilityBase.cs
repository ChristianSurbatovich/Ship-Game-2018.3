using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public abstract class AbilityBase : ScriptableObject
    {
        public string abilityName;
        public short id;
        public float cooldown;
        public abstract void Use(AbilityContext context);
    }
}

