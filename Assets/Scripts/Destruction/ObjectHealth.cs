using UnityEngine;
using System.Collections;

namespace ShipGame.Destruction
{
    public interface ObjectHealth
    {

        bool isAlive();

        Damage damage(Damage d);

        Damage damage(float d, string t);

        float currentHealth();

        void die();

    }
}

