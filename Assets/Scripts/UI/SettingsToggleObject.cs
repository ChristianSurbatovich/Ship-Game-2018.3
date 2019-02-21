using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame.UI
{
    public class SettingsToggleObject : MonoBehaviour
    {
        [SerializeField]
        private GameSettings settings;
        [SerializeField]
        private Settings setting;
        // Use this for initialization
        void Start()
        {
            if (!settings)
            {
                settings = FindObjectOfType<GameSettings>();
            }
            settings.Subscribe(OnValueChanged, setting);
            gameObject.SetActive((bool)settings.GetValue(setting));
        }

        public void OnValueChanged(object value)
        {
            gameObject.SetActive((bool)value);
        }
    }
}

