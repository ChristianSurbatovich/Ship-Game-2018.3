using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class Projectile : MonoBehaviour
    {
        private float speed, drop, expDamage, expRadius, expUp, expForce, expFalloff, healthDamage, healthRadius, healthFalloff, startTime, travelTime, pathTime;
        private Launcher parent;
        private string damageType;
        private bool launched;
        private Vector3 flightDirection;
        // Use this for initialization
        void Start()
        {
            // get values from launcher
        }

        private void OnEnable()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (launched)
            {
                travelTime += Time.deltaTime;
                pathTime -= Time.deltaTime;
                flightDirection = transform.forward * Time.deltaTime * speed;
                flightDirection.y = Time.deltaTime * pathTime / startTime * speed;
                transform.Translate(flightDirection);
            }
        }

        public void launch()
        {
            launched = true;
        }
    }
}
