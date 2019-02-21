using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    [CreateAssetMenu()]
    public abstract class Ability : LootBase
    {

        public abstract void OnAdd();

        public abstract void OnRemove();

        public abstract void Use();
        public abstract void UseRemote();
        public abstract void OnButtonDown();
        public abstract void OnButtonUp();

        public override void OnEquip()
        {
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

    }
}

