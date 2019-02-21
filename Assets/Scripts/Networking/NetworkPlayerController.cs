using UnityEngine;
using System;
using System.Collections.Generic;
using ShipGame.Destruction;
namespace ShipGame.Network
{
    public class NetworkPlayerController : MonoBehaviour, INetworkController
    {
        private IShipControl netPlayerShip;
        public DestroyableObject shipDestruction;
        private Vector3[] aimPoints;
        private short id;
        public Dictionary<short, Ability> abilities;
        // Use this for initialization
        void Awake()
        {
            netPlayerShip = GetComponent(typeof(IShipControl)) as IShipControl;
        }
        public void SetID(short i)
        {
            id = i;
        }


        public short GetID()
        {
            return id;
        }

        public void Action(GameMessage message)
        {
            print("Received action: " + message);
            switch (message.type)
            {
                case MessageValues.DESTRUCTION_STATE:
                    shipDestruction.ApplyStateChange(((DestructionStateMessage)message).vertices);
                    break;
                case MessageValues.DESTRUCTION_STATE_RESET:
                    shipDestruction.FullDestructionReset();
                    break;
                case MessageValues.OPEN:
                    netPlayerShip.OpenGunports();
                    break;
            }
        }
        public void SetName(string n)
        {
            return;
        }

        public void AddAbility(short id)
        {

        }

        public void RemoveAbility(short id)
        {

        }
    }
}

