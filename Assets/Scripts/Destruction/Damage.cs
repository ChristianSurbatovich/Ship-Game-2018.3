using UnityEngine;
using System.Collections;

namespace ShipGame.Destruction
{
    public class Damage
    {
        private float effective;
        public float amount;
        public string typeOfDamage;
        public bool killingBlow;
        // Use this for initialization
        public Damage()
        {
            amount = 0;
            typeOfDamage = "";
            effective = 0;
            killingBlow = false;
        }

        public Damage(float a, string t)
        {
            amount = a;
            typeOfDamage = t;
            effective = 0;
            killingBlow = false;
        }

        public Damage(Damage d)
        {
            amount = d.amount;
            typeOfDamage = d.typeOfDamage;
            effective = d.effective;
            killingBlow = d.killingBlow;
        }

        public float calculate(float mod)
        {
            effective = amount * mod;
            return effective;
        }

        public string getType()
        {
            return typeOfDamage;
        }

        public float getAmount()
        {
            return amount;
        }

        public void set(float a, string t)
        {
            amount = a;
            typeOfDamage = t;
        }

        public void applyMod(float m)
        {
            amount = amount * m;
        }
    }

}
