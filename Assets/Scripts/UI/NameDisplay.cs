using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame.UI
{
    public class NameDisplay : MonoBehaviour
    {
        private Camera cameraObject;
        public float fadeStart, fadeEnd;
        private GameSettings settings;
        [SerializeField]
        private Settings playerSetting;
        // Use this for initialization
        void Start()
        {
            cameraObject = Camera.main;
            settings = FindObjectOfType<GameSettings>();
            settings.Subscribe(DisplayChanged, playerSetting);
            gameObject.SetActive((bool)settings.GetValue(playerSetting));
        }

        private void DisplayChanged(object showName)
        {
            gameObject.SetActive((bool)showName);
        }
        // Update is called once per frame
        void Update()
        {
            transform.LookAt(cameraObject.transform);
        }
    }
}

