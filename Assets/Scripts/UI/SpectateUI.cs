using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ShipGame.Network;
using System;
namespace ShipGame.UI
{
    public class SpectateUI : MonoBehaviour, ISpectateUI
    {
        private UIController ui;
        public Button spawnButton;
        public Text spawnButtonText;
        public Text spectateText;
        public GameObject specateUI;
        private bool focus;
      //  private byte[] spawnMessage = new byte[MessageLengths.SPAWN + 2];
        // Use this for initialization
        private void Awake()
        {
            ui = FindObjectOfType<UIController>();
        }

        public void SpectateNext()
        {
            ui.SpectateNext();
        }
        public void SpectatePrevious()
        {
            ui.SpectatePrevious();
        }
        public void Respawn()
        {
          //  Array.Copy(BitConverter.GetBytes(MessageLengths.SPAWN), 0, spawnMessage, 0, 2);
           // spawnMessage[2] = MessageValues.SPAWN;
           // Array.Copy(BitConverter.GetBytes(NetworkManager.Net.NetID()), 0, spawnMessage, 3, 2);
           // NetworkManager.Net.SendNetworkMessage(spawnMessage);
            
        }


        public void StartSpawnCounter(float t)
        {
            spawnButton.interactable = false;
            StartCoroutine(CountDown(t));
        }

        public void Spectate(string s)
        {
            spectateText.text = "Spectating " + s;
        }

        IEnumerator CountDown(float t)
        {
            while (t > 0)
            {
                spawnButtonText.text = Mathf.CeilToInt(t).ToString();
                t -= Time.deltaTime;
                yield return 0;
            }
            spawnButton.interactable = true;
            spawnButtonText.text = "Respawn";
        }

        public void Open()
        {
            specateUI.SetActive(true);
        }

        public void Close()
        {
            specateUI.SetActive(false);
        }

        public void SetFocus(bool newFocus)
        {
            focus = newFocus;
        }
    }

}
