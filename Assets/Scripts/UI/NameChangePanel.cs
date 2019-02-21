using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ShipGame.Network;

namespace ShipGame.UI
{
    public class NameChangePanel : MonoBehaviour, INameChangePanel
    {
        public GameObject uiPanel;
        private bool focus, panelOpen;
        private IInputManager input;
      //  private byte[] nameMessage = new byte[MessageLengths.MAX_SEND_LENGTH];

        private void Awake()
        {
            input = GameObject.FindGameObjectWithTag("Input").GetComponent(typeof(IInputManager)) as IInputManager;
        }

        private void Update()
        {
            if (focus && Input.GetButtonDown("Cancel"))
            {
                if (panelOpen) {

                    Close();
                }
            }
        }

        public void Submit()
        {
            string name = uiPanel.GetComponentInChildren<InputField>().text;
            if (name != null || name != "")
            {
                byte[] nameBytes = System.Text.Encoding.UTF8.GetBytes(name);
            //    Array.Copy(BitConverter.GetBytes((short)(nameBytes.Length + 5)), 0, nameMessage, 0, 2);
            //    nameMessage[2] = MessageValues.NAME;
           //     Array.Copy(BitConverter.GetBytes(NetworkManager.Net.NetID()), 0, nameMessage, 3, 2);
             //   Array.Copy(BitConverter.GetBytes((short)nameBytes.Length), 0, nameMessage, 5, 2);
            //    Array.Copy(nameBytes, 0, nameMessage, 7, nameBytes.Length);

            //    NetworkManager.Net.SendNetworkMessage(nameMessage,nameBytes.Length + 7);
            }
            Close();
        }

        public void SetFocus(bool newFocus)
        {
            focus = newFocus;
        }

        public void Open()
        {
            input.Register(this, InputFocus.EXCLUSIVE);
            uiPanel.SetActive(true);
            panelOpen = true;
        }

        public void Close()
        {
            panelOpen = false;
            uiPanel.SetActive(false);
            input.Unregister(this, InputFocus.EXCLUSIVE);
        }
    }
}

