using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    [CreateAssetMenu()]
    public class AbilityList : ScriptableObject
    {

        public List<short> ids;
        public AbilityBase[] abilities;

        public AbilityBase GetAbilityByID(short id)
        {
            return abilities[ids.IndexOf(id)];
        }
    }
}


