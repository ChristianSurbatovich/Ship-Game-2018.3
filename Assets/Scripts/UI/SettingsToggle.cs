using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShipGame.UI
{
    public class SettingsToggle : MonoBehaviour, IMenuOption{
        [SerializeField]
        private GameSettings gameSettings;
        [SerializeField]
        private Settings setting;
        [SerializeField]
        private Toggle toggle;

        public void ValueChanged()
        {
            gameSettings.SetValue(setting, toggle.isOn);
        }

    }

}
