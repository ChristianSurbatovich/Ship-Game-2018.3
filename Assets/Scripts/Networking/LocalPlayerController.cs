using UnityEngine;
using System;
namespace ShipGame.Network
{
    public class LocalPlayerController : MonoBehaviour, INetworkController
    {
        public short id;
        private IShipControl playerShip;
        // Use this for initialization
        void Start()
        {
            playerShip = GetComponent(typeof(IShipControl)) as IShipControl;
        }

        public void Action(byte[] action)
        {
            print(action[0]);
            switch (action[0])
            {
                case MessageValues.SINK:
                    playerShip.Sink(new Vector3(BitConverter.ToSingle(action, 3), BitConverter.ToSingle(action, 7), BitConverter.ToSingle(action, 11)),
                        BitConverter.ToSingle(action, 15), BitConverter.ToSingle(action, 19), BitConverter.ToSingle(action, 23));
                    break;
                default:
                    break;
            }
        }
        public void SetID(short i)
        {
            id = i;
        }


        public short GetID()
        {
            return id;
        }

        public void SetName(string n)
        {
            return;
        }

        public void AddAbility(short id)
        {
            throw new NotImplementedException();
        }

        public void RemoveAbility(short id)
        {
            throw new NotImplementedException();
        }

        public void Action(GameMessage message)
        {
            throw new NotImplementedException();
        }
    }
}

