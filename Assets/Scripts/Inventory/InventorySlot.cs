using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace ShipGame.UI
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField]
        private InventoryItem currentItem;
        public bool open;
        [SerializeField]
        private Image backGround;
        [SerializeField]
        private Color highlight;
        [SerializeField]
        private Color normal;
        public ItemLocation location;
        public RectTransform rectTransform;
        public Vector2 iconOffset;
        public string key;
        public short id;
        private void Start()
        {
            backGround.color = normal;
        }

        public bool AddItem(InventoryItem item)
        {
            if (open)
            {
                currentItem = item;
                currentItem.SetLocation(this);
                open = false;
                return true;
            }
            return false;
        }

        public InventoryItem RemoveItem()
        {
            open = true;
            return currentItem;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ItemManager.Inventory.SetActiveSlot(this, true);
            backGround.color = highlight;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ItemManager.Inventory.SetActiveSlot(this, false);
            backGround.color = normal;
        }
    }
}

