using UnityEngine;
using System.Collections;
namespace ShipGame
{
    public abstract class ProjectileHitHandler : MonoBehaviour
    {
        public abstract void onHit(int damage);
    }
}

