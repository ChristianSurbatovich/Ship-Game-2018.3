using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ShipGame.Network;
using System;

namespace ShipGame.UI
{
    public enum PointerState { exit, enter, hover, off }
    public class ElementState
    {
        public PointerState state = PointerState.exit;
        public bool usedThisFrame;
        public void SetState(bool mouseState)
        {
            usedThisFrame = true;
            switch (state)
            {
                case PointerState.exit:
                    if (mouseState)
                    {
                        state = PointerState.enter;
                    }
                    break;
                case PointerState.enter:
                    if (mouseState)
                    {
                        state = PointerState.hover;
                        break;
                    }
                    else
                    {
                        state = PointerState.exit;
                    }
                    break;
                case PointerState.hover:
                    if (!mouseState)
                    {
                        state = PointerState.exit;
                    }
                    break;
            }
        }
    }

    public class ItemManager : MonoBehaviour
    {

        private static ItemManager instance;


        public static ItemManager Inventory
        {
            get
            {
                return instance;
            }
        }
        public InventorySlot activeInventorySlot;
        [SerializeField]
        private GraphicRaycaster backGroundRaycaster, foreGroundRaycaster;
        [SerializeField]
        private GameObject inventoryFront, inventoryBack, lootWindowObject;
        public LootWindow lootWindow;
        public bool open;
        private PointerEventData eventData;
        private List<RaycastResult> results;
        private Dictionary<InventorySlot, ElementState> elementStates;
        private List<InventorySlot> inactive;
        [SerializeField]
        private EventSystem eventSystem;
        [SerializeField]
        private List<InventorySlot> slotList;
        [SerializeField]
        private int rows, columns;
        public int capacity;
        public bool openSpaces;
        [SerializeField]
        private NetworkManager net;
        public short lootAreaID;
        public LootableArea activeLootArea;
        private byte[] messageBuffer = new byte[MessageLengths.MAX_SEND_LENGTH];
        private Dictionary<short, InventoryItem> equippedItems;
        private Dictionary<short, InventoryItem> inventoryItems;
        private Dictionary<short, InventorySlot> allLocations;
        private Dictionary<short, short> itemLocations;
        private Dictionary<InventorySlot,InventoryItem> QueuedChanges;


        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            elementStates = new Dictionary<InventorySlot, ElementState>();
            inactive = new List<InventorySlot>();
            results = new List<RaycastResult>();

        }

        private void Update()
        {
            if (Input.GetButtonDown("Inventory"))
            {
                if (open)
                {
                    CloseInventory();
                }
                else
                {
                    OpenInventory();
                }
            }
        }

        IEnumerator InventoryOpen()
        {
            while (open)
            {
                foreach(ElementState ele in elementStates.Values)
                {
                    ele.usedThisFrame = false;
                }
                eventData = new PointerEventData(eventSystem)
                {
                    position = Input.mousePosition
                };
                results.Clear();
                backGroundRaycaster.Raycast(eventData, results);
                foreach(RaycastResult hit in results)
                {
                    InventorySlot temp = hit.gameObject.GetComponent<InventorySlot>();
                    if (temp)
                    {
                        if (!elementStates.ContainsKey(temp))
                        {
                            elementStates[temp] = new ElementState();
                        }
                        elementStates[temp].SetState(true);
                    }
                }
                foreach(KeyValuePair<InventorySlot,ElementState> kvp in elementStates)
                {
                    if (!kvp.Value.usedThisFrame)
                    {
                        kvp.Value.SetState(false);
                    }
                    switch (kvp.Value.state)
                    {
                        case PointerState.enter:
                            kvp.Key.OnPointerEnter(eventData);
                            break;
                        case PointerState.exit:
                            kvp.Key.OnPointerExit(eventData);
                            inactive.Add(kvp.Key);
                            break;
                        case PointerState.hover:
                            break;
                    }
                }
                foreach(InventorySlot isl in inactive)
                {
                    elementStates.Remove(isl);
                }
                inactive.Clear();
                yield return 0;
            }
        }

        public void OpenInventory()
        {
            open = true;
            inventoryBack.SetActive(true);
            StartCoroutine(InventoryOpen());
        }


        public void CloseInventory()
        {
            inventoryBack.SetActive(false);
            StopCoroutine(InventoryOpen());
            open = false;
        }

        public void SetActiveSlot(InventorySlot slot, bool isActive)
        {
            activeInventorySlot = isActive ? slot : null;
        }

        public void Register(InventorySlot s)
        {
            allLocations[s.id] = s;
        }

        public void SwapItems(short loc1, short loc2)
        {
            if (allLocations[loc1].open)
            {

            }
        }

        public void MoveItems(short loc1, short loc2)
        {
            if (allLocations[loc2].open)
            {

            }
        }
        

        public void RequestChange(InventoryItem item)
        {
            if(activeInventorySlot == null || activeInventorySlot.location == ItemLocation.LOOTWINDOW)
            {
                item.SetLocation(item.itemOwner);
            }
            else
            {

            }
            /*if (activeInventorySlot.open)
            {
                if (item.itemOwner)
                {
                    item.itemOwner.RemoveItem();
                }
                activeInventorySlot.AddItem(item);
            }
            else
            {
                InventoryItem tempItem = activeInventorySlot.RemoveItem();
                InventorySlot tempLoc = item.itemOwner;
                activeInventorySlot.AddItem(item);
                tempLoc.RemoveItem();   
                tempLoc.AddItem(tempItem);
            }
            */
        }

        public LootWindow OpenLootWindow(short num_items)
        {
            lootWindow.Open(num_items);
            return lootWindow;
        }

        public void CloseLootWindow()
        {
            activeLootArea.StopLoot();
            lootWindow.Close();
        }

        public void PickUpItem(short localID, short slotID)
        {
            InventoryItem temp = lootWindow.RemoveItemFromWindow(localID);
            allLocations[slotID].AddItem(temp);
        }

        public void RequestPickup(InventoryItem item)
        {


            for (int i = 0; i < slotList.Count; i++)
            {
                if (slotList[i].open)
                {
                    Array.Copy(BitConverter.GetBytes((short)5), 0, messageBuffer, 0, 2);
                    messageBuffer[2] = MessageValues.PICKUP_ITEM;
                    Array.Copy(BitConverter.GetBytes(item.localID), 0, messageBuffer, 3, 2);
                    Array.Copy(BitConverter.GetBytes(lootAreaID), 0, messageBuffer, 5, 2);
                    net.SendNetworkMessage(messageBuffer);
                    return;
                }
            }
            // no open spaces for the item

        }

        public void RequestEquip(InventoryItem item, InventorySlot slot)
        {
            item.equipped = true;
            Array.Copy(BitConverter.GetBytes((short)3), 0, messageBuffer, 0, 2);
            messageBuffer[2] = MessageValues.EQUIP_ITEM;
            Array.Copy(BitConverter.GetBytes(item.localID), 0, messageBuffer, 3, 2);
            net.SendNetworkMessage(messageBuffer);
        }

        public void RequestUnequip(InventoryItem item, InventorySlot slot)
        {
            item.equipped = false;
            Array.Copy(BitConverter.GetBytes((short)3), 0, messageBuffer, 0, 2);
            messageBuffer[2] = MessageValues.UNEQUIP_ITEM;
            Array.Copy(BitConverter.GetBytes(item.localID), 0, messageBuffer, 3, 2);
            net.SendNetworkMessage(messageBuffer);
        }

        public void QueueEquipChange(InventorySlot slot, InventoryItem item)
        {
            QueuedChanges[slot] = item;
        }

        public void QueueUnequipChange()
        {

        }

        public void FinishEquip()
        {

        }

        public void FinishUnequip()
        {

        }

    }
}

