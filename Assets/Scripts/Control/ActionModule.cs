using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class ActionModule : MonoBehaviour
    {
        public static int MAX_ABILITIES = 10;
        private short[] localMapping;
        private Dictionary<short, IGameAction> actionMap;

        private void Start()
        {
            localMapping = new short[MAX_ABILITIES];
        }

        public void UseAbility(short id)
        {

        }

        public void AddAbility(short id)
        {
            
        }

        public void RemoveAbility(short id)
        {

        }
    }
}

