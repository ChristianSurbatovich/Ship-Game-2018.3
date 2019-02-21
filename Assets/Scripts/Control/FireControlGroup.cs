using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class FireControlGroup : MonoBehaviour
    {
        public LauncherController[] launcherGroups;
        // these need to add up to 360;
        public Vector2[] launcherRanges;
        public float degreeRange, verticalRange;
        public float G, power;
        public float coolDown;
        private float cooldownRemaining;
        public float accuracyGrouping, accuracyRange;
        public bool active, ready;
        // Use this for initialization
        void Start()
        {
            active = true;
            ready = true;
        }

        private IEnumerator CooldownRoutine()
        {
            while(cooldownRemaining > 0)
            {
                cooldownRemaining -= Time.deltaTime;
                yield return 0;
            }
            ready = true;
        }

        public void SetStat(short stat, float value)
        {
            switch (stat)
            {
                case Stats.WEAPON_COOLDOWN:
                    coolDown = value;
                    break;
                case Stats.WEAPON_DAMAGE:
                    break;
                case Stats.WEAPON_POWER:
                    break;
                case Stats.WEAPON_VERTICAL:
                    break;
                case Stats.WEAPON_HORIZONTAL:
                    break;
                case Stats.WEAPON_RANGE:
                    break;
                case Stats.WEAPON_SPREAD:
                    break;
                case Stats.WEAPON_GROUPING:
                    break;
                case Stats.WEAPON_ACTIVE:
                    active = false;
                    break;
            }
        }

        // Update is called once per frame
        public void AimAt(Vector3 aimPoint)
        {
            float angle = Vector3.SignedAngle(transform.forward, aimPoint - transform.position, Vector3.up);
            for (int i = 0; i < launcherGroups.Length; i++)
            {
                if (launcherRanges[i].x <= angle && angle < launcherRanges[i].y)
                {
                    launcherGroups[i].AimAtPoint(aimPoint);
                }
            }
        }
        public Vector3[] Fire(Vector3 aimPoint, short id)
        {
            if(active & ready)
            {
                float angle = Vector3.SignedAngle(transform.forward, aimPoint - transform.position, Vector3.up);

                for (int i = 0; i < launcherGroups.Length; i++)
                {
                    if (launcherRanges[i].x <= angle && angle < launcherRanges[i].y)
                    {
                        cooldownRemaining = coolDown;
                        StartCoroutine(CooldownRoutine());
                        ready = false;
                        return launcherGroups[i].Fire(aimPoint, id);
                    }
                }
            }

            return null;
        }

        public void Fire(Vector3[] aimPoints, short id)
        {
            if(active && ready)
            {
                float angle = Vector3.SignedAngle(transform.forward, aimPoints[0] - transform.position, Vector3.up);

                for (int i = 0; i < launcherGroups.Length; i++)
                {
                    if (launcherRanges[i].x <= angle && angle < launcherRanges[i].y)
                    {
                        cooldownRemaining = coolDown;
                        StartCoroutine(CooldownRoutine());
                        launcherGroups[i].Fire(aimPoints, id);
                        ready = false;
                        break;
                    }
                }
            }

        }
    }
}

