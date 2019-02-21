using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class AbilityItem : LootBase
    {
        GameObject cannons;
        public override void OnEquip()
        {
            // add ability to input
            // add ability to ship
        }
                
        public override void OnItemDestroyed()
        {
            throw new System.NotImplementedException();
        }

        public override void OnLootWindow()
        {
            throw new System.NotImplementedException();
        }

        public override void OnPickup()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUnequip()
        {
            throw new System.NotImplementedException();
        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

