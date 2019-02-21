using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShipGame.UI
{
    public class LootWindow : MonoBehaviour
    {
        private Dictionary<short, InventoryItem> itemList;
        [SerializeField]
        private List<InventorySlot> itemSpaces;
        [SerializeField]
        private GameObject window;
        [SerializeField]
        private int maxRows, maxColumns, offsetX, offsetY, currentRow, currentCol, anchorX, anchorY, widthPerCol, heightPerRow, numColumns, numRows, baseWidth, baseHeight;
        public bool open;
        [SerializeField]
        private RectTransform backGround;
        private short itemAmount;

        private void Awake()
        {
            itemList = new Dictionary<short, InventoryItem>();
        }

        public void Open(short numItems)
        {
            itemAmount = numItems;
            open = true;
            gameObject.SetActive(true);
        }

        public void Close()
        {
            open = false;
            gameObject.SetActive(false);
            currentCol = 1; 
            currentRow = 0;
            numColumns = 1;
            numRows = 0;
            foreach (InventorySlot space in itemSpaces)
            {
                space.open = true; 
            }

            foreach (KeyValuePair<short, InventoryItem> kvp in itemList)
            {
                Destroy(kvp.Value.gameObject);
            }

            itemList.Clear();
        }

        public void AddItemToWindow(InventoryItem item, short localID)
        {
            item.localID = localID;
            itemList[localID] = item;
            foreach (InventorySlot space in itemSpaces)
            {
                if (space.open)
                {
                    space.gameObject.SetActive(true);
                    space.AddItem(item);
                    break;
                }
            }

            if(currentRow < maxRows - 1)
            {
                currentRow++;
                numRows += numRows < maxRows ? 1 : 0;
                backGround.sizeDelta = new Vector2(baseWidth + widthPerCol * numColumns, baseHeight + heightPerRow * numRows);
            }
            else
            {
                // new column
                numColumns++;
                currentRow = 0;
                currentCol++;
                backGround.sizeDelta = new Vector2(baseWidth + widthPerCol * numColumns, baseHeight + heightPerRow * numRows);
            }
        }

        public InventoryItem RemoveItemFromWindow(short id)
        {
            if (itemList.ContainsKey(id))
            {
                itemAmount--;
                InventoryItem temp = itemList[id];
                itemList.Remove(id);
                temp.itemOwner.gameObject.SetActive(false);
                if(itemAmount == 0)
                {
                    Close();
                }
                return temp;
            }
            return null;
        }
    }
}

