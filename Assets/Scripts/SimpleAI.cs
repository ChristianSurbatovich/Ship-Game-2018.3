using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class SimpleAI : MonoBehaviour
    {
        public ShipControl npcShip;
        public FireControlGroup playerCannons;
        public Vector3 aimPoint;
        public GameObject player;
        private float moveHorizontal, moveForward, cooldown, angleToTarget;
        public float shotCooldown, degreeRange, trackingMax, trackingMin, yBias;
        // Use this for initialization
        void Start()
        {
            npcShip.OpenGunports();
        }

        // Update is called once per frame
        void Update()
        {
            moveForward = 1;
            aimPoint = player.transform.position;
            aimPoint.y += yBias;
            angleToTarget = Vector3.Angle(transform.forward, aimPoint - transform.position);
            if (Vector3.Cross(transform.forward, aimPoint - transform.position).y > 0)
            {
                if (angleToTarget > trackingMax)
                {
                    moveHorizontal = 1;
                }
                else if (angleToTarget < trackingMin)
                {
                    moveHorizontal = -1;
                }
                else
                {
                    moveHorizontal = 0;
                }

            }
            else
            {
                if (angleToTarget > trackingMax)
                {
                    moveHorizontal = -1;
                }
                else if (angleToTarget < trackingMin)
                {
                    moveHorizontal = 1;
                }
                else
                {
                    moveHorizontal = 0;
                }
            }
            npcShip.SetMovement(moveHorizontal, moveForward);
            if (cooldown <= 0 && (Vector3.Angle(transform.right, aimPoint - transform.position) < degreeRange || Vector3.Angle(-transform.right, aimPoint - transform.position) < degreeRange))
            {
                playerCannons.Fire(aimPoint, 0);
                cooldown = shotCooldown;
            }
            else
            {
                playerCannons.AimAt(aimPoint);
            }
            cooldown -= Time.deltaTime;
        }
    }

}


