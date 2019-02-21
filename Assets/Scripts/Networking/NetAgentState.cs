using UnityEngine;
using UnityEngine.UI;
using ShipGame.Destruction;
using System.Collections.Generic;

namespace ShipGame.Network
{
    public class NetAgentState
    {
        public bool doorsOpen;
        public Text name;
        public PlayerHealth health;
        public DestroyableObject shipDestruction;
        public GameObject ship;
        public IShipControl shipControl;
        public Dictionary<short, float> stats;
        public Dictionary<short, short> items;
        public NetAgentState(bool doors, Text nameBar, PlayerHealth healthBar, GameObject agent)
        {
            doorsOpen = doors;
            name = nameBar;
            health = healthBar;
            ship = agent;
            shipControl = ship.GetComponent(typeof(IShipControl)) as IShipControl;
        }

        public void SetStat(StatMessage message)
        {
            stats[message.statID] = message.statValue;
        }
    }

}
