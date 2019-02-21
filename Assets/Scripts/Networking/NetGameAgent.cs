using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame.Network
{
    public class NetGameAgent
    {
        public Transform transform;
        public short ID;
        public INetworkController networkController;
        public NetAgentState state;
        public NetAgentPosition position;
        public bool frozen, interpolate;
        public NetGameAgent(short id, Transform t, INetworkController i)
        {
            ID = id;
            transform = t;
            networkController = i;
        }

    }
}

