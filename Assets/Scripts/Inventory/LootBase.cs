using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShipGame.UI;
namespace ShipGame
{
    [CreateAssetMenu()]
    public abstract class LootBase : ScriptableObject
    {
        public InventorySlot location;
        public short ID;
        public string itemName;
        public string description;
        public short[] statIDs;
        public float[] statValues;
        public Sprite icon;
        public bool ability;

        public abstract void OnLootWindow();
        public abstract void OnPickup();

        public abstract void OnEquip();

        public abstract void OnUnequip();
        public abstract void OnItemDestroyed();
    }
}

