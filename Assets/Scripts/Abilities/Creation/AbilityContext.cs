using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShipGame.Network;

namespace ShipGame
{
    public struct AbilityContext
    {
        public IShipControl playerShip;
        public INetworkController player;
        public byte[] messageBuffer;
    }
}

