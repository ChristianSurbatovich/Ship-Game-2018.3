using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShipGame.UI
{
    public class NameToggle : MonoBehaviour, IMenuOption
    {
        public GameSettings gameSettings;
        private Toggle playerName;
        private void Start()
        {
            playerName = GetComponent<Toggle>();   
        }
        public void ValueChanged()
        {
            gameSettings.SetValue(Settings.showOwnName, playerName.isOn);
        }
    }
}

