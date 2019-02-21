using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShipGame.UI;

namespace ShipGame
{
    public class ItemDB : MonoBehaviour
    {
        [SerializeField]
        private LootBase[] lootData;
        [SerializeField]
        private List<short> lootIDs;
        [SerializeField]
        private InventoryItem itemObject;
        [SerializeField]
        private Transform iconLayer;
        public InventoryItem GetItemByID(short id)
        {
            InventoryItem temp = Instantiate(itemObject);
            temp.SetData(lootData[lootIDs.IndexOf(id)]);
            temp.iconLayer = iconLayer;
            return temp;
        }
    }
}

