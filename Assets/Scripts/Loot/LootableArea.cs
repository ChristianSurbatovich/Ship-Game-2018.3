using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ShipGame.UI;
using ShipGame.Network;
namespace ShipGame
{
    public class LootableArea : MonoBehaviour
    {

        [SerializeField]
        private float pickupRange;
        [SerializeField]
        private Text description;
        [SerializeField]
        private Text lootPrompt;
        [SerializeField]
        private Int16 id;
        private Camera cameraObject;
        [SerializeField]
        private float fadeStart, fadeLength, promptFadeStart, promptFadeLength;
        private GameSettings settings;
        private Coroutine routine;
        [SerializeField]
        private Color descriptionColor, promptColor;
        private bool looting;
        [SerializeField]
        private NetworkManager net;
        [SerializeField]
        private ItemManager inventory;
       // static private byte[] messageBuffer = new byte[MessageLengths.LOOT_AREA_LENGTH + 2];
        private float distance;

        private void Start()
        {
            cameraObject = Camera.main;
            descriptionColor = description.color;
            promptColor = lootPrompt.color;
        }

        public void StartLoot()
        {
            inventory.lootAreaID = id;
            looting = true;
            description.gameObject.SetActive(false);
            lootPrompt.gameObject.SetActive(false);
           // Array.Copy(BitConverter.GetBytes((short)MessageLengths.LOOT_AREA_LENGTH), 0, messageBuffer, 0, 2);
         //   messageBuffer[2] = MessageValues.LOOT_AREA;
          //  Array.Copy(BitConverter.GetBytes(id), 0, messageBuffer, 3, 2);
           // net.SendNetworkMessage(messageBuffer, MessageLengths.LOOT_AREA_LENGTH + 2);
        }

        public void StopLoot()
        {
            looting = false;
            description.gameObject.SetActive(true);
            lootPrompt.gameObject.SetActive(true);
        }

        private IEnumerator PlayerCloseRoutine()
        {
            while (true)
            {
                distance = Vector3.Distance(transform.position, NetworkManager.player.transform.position);
                float fadeAmount = distance - fadeStart;
                fadeAmount = 1 - (fadeAmount / fadeLength);
                fadeAmount = fadeAmount < 0 ? 0 : (fadeAmount > 1 ? 1 : fadeAmount);
                descriptionColor.a = fadeAmount;
                description.color = descriptionColor;

                fadeAmount = distance - promptFadeStart;
                fadeAmount = 1 - (fadeAmount / promptFadeLength);
                fadeAmount = fadeAmount < 0 ? 0 : (fadeAmount > 1 ? 1 : fadeAmount);
                promptColor.a = fadeAmount;
                lootPrompt.color = promptColor;

                transform.LookAt(cameraObject.transform);
                yield return 0;
            }

        }

        public void TryLoot()
        {
            if(distance <= pickupRange)
            {
                if (!inventory.lootWindow.open)
                {
                    StartLoot();
                }
                else if (looting)
                {
                    inventory.CloseLootWindow();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                inventory.activeLootArea = this;
                description.gameObject.SetActive(true);
                lootPrompt.gameObject.SetActive(true);
                routine = StartCoroutine(PlayerCloseRoutine());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                description.gameObject.SetActive(false);
                lootPrompt.gameObject.SetActive(false);
                if (looting)
                {
                    inventory.CloseLootWindow();
                    looting = false;
                }
                StopCoroutine(routine);

            }
        }
    }
}

