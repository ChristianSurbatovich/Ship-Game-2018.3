using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShipGame.UI
{
    public enum ItemLocation { INVENTORY,EQUIPPED,LOOTWINDOW,ABILITYBAR}
    public class InventoryItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public ItemLocation itemLocation;
        public InventorySlot itemOwner;
        public LootBase itemData;
        [SerializeField]
        private Image icon;
        public short localID;
        public RectTransform rectTransform;
        public bool equipped = false;

        private bool drag;
        [SerializeField]
        private GameObject toolTip;
        [SerializeField]
        private Text tooltipText;
        public Transform iconLayer;

        public void SetData(LootBase data)
        {
            itemData = data;
            icon.sprite = data.icon;
            tooltipText.text = data.description;
        }

        private IEnumerator DragRoutine()
        {
            transform.SetParent(iconLayer);
            toolTip.SetActive(false);
            while (drag)
            {
                transform.position = Input.mousePosition - new Vector3(rectTransform.sizeDelta.x / 2, rectTransform.sizeDelta.y / -2, 0);
                yield return 0;
            }
            ItemManager.Inventory.RequestChange(this);
        }

        

        public void SetLocation(InventorySlot space)
        {
            toolTip.SetActive(false);
            if (space)
            {
                itemLocation = space.location;
                itemOwner = space;
                transform.SetParent(itemOwner.transform.parent);
                rectTransform.anchoredPosition = itemOwner.rectTransform.anchoredPosition + itemOwner.iconOffset;
                rectTransform.localScale = itemOwner.rectTransform.localScale;

            }else
            {
                if (equipped)
                {
                    //unequip
                }
            }

  
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (itemLocation == ItemLocation.INVENTORY || itemLocation == ItemLocation.EQUIPPED)
            {
                transform.position = Input.mousePosition - new Vector3(rectTransform.sizeDelta.x / 2, rectTransform.sizeDelta.y / -2, 0);
            }

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (itemLocation == ItemLocation.INVENTORY || itemLocation == ItemLocation.EQUIPPED)
            {
                ItemManager.Inventory.RequestChange(this);
            }

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.SetParent(iconLayer);
            toolTip.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (itemLocation)
            {
                case ItemLocation.LOOTWINDOW:
                    ItemManager.Inventory.RequestPickup(this);
                    break;
                default:
                    drag = !drag;
                    StartCoroutine(DragRoutine());
                    break;
            }
        }

        public void Equip()
        {

        }

        public void UnEquip()
        {

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            toolTip.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            toolTip.SetActive(false);
        }
    }
}

