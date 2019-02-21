using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShipGame.Network;

namespace ShipGame
{
    public class ShipGame : MonoBehaviour
    {
        private static ShipGame instance;
        private NetworkGameState gameState;
        [SerializeField]
        private NetworkManager net;

        public static ShipGame Game
        {
            get
            {
                return instance;
            }
        }

        public void SetGameState(NetworkGameState state)
        {
            gameState = state;
        }

        private void Awake()
        {
            instance = this;
        }

        public float GetStatValue(short playerID, short statID)
        {
            return gameState.GetStat(playerID, statID);
        }

        public void SetPlayerWatch(short playerID)
        {

        }
    }
}

