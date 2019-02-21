using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class GameSettings : MonoBehaviour
    {
        public delegate void OnValueChange(object value);
        public DefaultSettings defaultSettings;
        private Dictionary<Settings, object> settingsValues;
        private Dictionary<Settings,List<OnValueChange>> subscribedFunctions;

        public void Subscribe(OnValueChange func, Settings setting)
        {
            if (!subscribedFunctions.ContainsKey(setting))
            {
                subscribedFunctions[setting] = new List<OnValueChange>();
            }
            if (subscribedFunctions[setting].Contains(func))
            {
                return;
            }
            subscribedFunctions[setting].Add(func);
        }

        private void Alert(Settings setting)
        {
            foreach (OnValueChange func in subscribedFunctions[setting])
            {
                func(settingsValues[setting]);
            }
        }

        // load settings
        private void Awake()
        {
            settingsValues = new Dictionary<Settings, object>();
            subscribedFunctions = new Dictionary<Settings, List<OnValueChange>>();
            settingsValues[Settings.showOwnHealthbar] = DefaultSettings.showOwnHealthbar;
            settingsValues[Settings.showOwnName] = DefaultSettings.showOwnName;
            settingsValues[Settings.showPlayerHealthbars] = DefaultSettings.showPlayerHealthbars;
            settingsValues[Settings.showPlayerNames] = DefaultSettings.showPlayerNames;
            settingsValues[Settings.playerShip] = DefaultSettings.playerShip;
            settingsValues[Settings.showLootPrompts] = DefaultSettings.showLootPrompts;
        }

        // save them to file
        private void OnApplicationQuit()
        {

        }

        public void SetValue(Settings setting, object value)
        {
            if (!subscribedFunctions.ContainsKey(setting))
            {
                subscribedFunctions[setting] = new List<OnValueChange>();
            }
            settingsValues[setting] = value;
            Alert(setting);
        }

        public object GetValue(Settings setting)
        {
            return settingsValues[setting];
        }
    }
}

