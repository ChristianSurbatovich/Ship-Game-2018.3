using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class Stats
    {
        public const short CURRENT_HEALTH = 1101;
        public const short MAX_HEALTH = 101;
        public const short SPEED = 102;
        public const short CURRENT_SPEED = 1102;
        public const short MOBILITY = 103;
        public const short COOLDOWN = 104;
        public const short DAMAGE_REDUCTION = 105;
        public const short VISION_RANGE = 106;
        public const short CANNON_DAMAGE = 107;
        public const short HARPOON_DAMAGE = 108;
        public const short LEVEL = 109;
        public const short EXP = 110;
        public const short CANNON_COOLDOWN = 111;
        public const short HARPOON_COOLDWON = 112;
        public const short DAMAGE_INCREASE = 113;
        public const short ACCELERATION = 114;
        public const short TURN_SPEED = 115;
        public const short SLOW_SPEED = 116;
        public const short WIND_DRAG = 117;
        public const short WEAPON_COOLDOWN = 118;
        public const short WEAPON_POWER = 119;
        public const short WEAPON_DAMAGE = 120;
        public const short WEAPON_VERTICAL = 121;
        public const short WEAPON_HORIZONTAL = 122;
        public const short WEAPON_RANGE = 123;
        public const short WEAPON_GROUPING = 124;
        public const short WEAPON_SPREAD = 125;
        public const short WEAPON_ACTIVE = 126;
    }
    public class PlayerStats
    {
        public float currentHealth;
        public float maxHealth;
        public float mobility;
        public float damageReduction;
        public float visionRange;
        public float speed;
        public float cooldown;
        public float cannonDamage;
        public float harpoonDamage;
        public short level;
        public short experience;
        public float cannonCooldown;
        public float harpoonCooldown;
        public float damageIncrease;
        public float acceleration;
        public float turnSpeed;
        public float slowSpeed;
        public float windDrag;

    }

}
