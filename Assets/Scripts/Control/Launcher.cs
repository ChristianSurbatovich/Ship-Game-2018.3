using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class Launcher : MonoBehaviour
    {
        public GameObject carriage, barrel;
        public bool useAmmoSettings;
        public float speed, drop, expDamage, expRadius, expUp, expForce, expFalloff, healthDamage, healthRadius, healthFalloff, startTime, travelTime, pathTime;
        public float degreeRange, maxRange;
        private MouseControl mouse;
        private Vector3 aimPoint;
        // Use this for initialization
        private void Awake()
        {
            mouse = GameObject.FindGameObjectWithTag("MouseControl").GetComponent<MouseControl>();
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            aimPoint = mouse.getAimPoint().point;
            aimAtPoint(aimPoint);

        }

        // point the cannon in the direction being aimed at
        private void aimAtPoint(Vector3 aimPoint)
        {

        }
    }
}

