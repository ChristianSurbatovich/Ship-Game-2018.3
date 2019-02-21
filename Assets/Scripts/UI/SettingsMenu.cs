using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame.UI
{
    public class SettingsMenu : MonoBehaviour, IUIObject
    {
        private bool focus, panelOpen;
        public GameObject settingsMenu;
        private IInputManager input;

        private void Awake()
        {
            input = GameObject.FindGameObjectWithTag("Input").GetComponent(typeof(IInputManager)) as IInputManager;
        }

        private void Start()
        {
            input.Register(this, InputFocus.SHARED);
        }
        private void Update()
        {
            if (focus)
            {
                if(Input.GetButtonDown("Cancel") && focus){
                    if (panelOpen)
                    {
                        Close();
                    }
                }
            }
        }

        public void Close()
        {
            panelOpen = false;
            settingsMenu.SetActive(false);
        }

        public void Open()
        {
            panelOpen = true;
            settingsMenu.SetActive(true);
        }

        public void SetFocus(bool newFocus)
        {
            focus = newFocus;
        }
    }

}
