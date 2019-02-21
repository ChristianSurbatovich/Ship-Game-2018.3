using UnityEngine;
using System.Collections;

namespace ShipGame
{
    public class RocketBarrageController : MonoBehaviour
    {
        public int numLaunchers; //number of projectile launchers
        public GameObject[] rocketLaunchers;
        public float fireInterval;   // period of time between different launchers fireing
        private float timeSinceLastShot = 100;
        private bool status = false; // state of the launchers true = firing, false = not firing
        public int numVolleys; // number of shots fired by each projectile launcher
        private int firedVolleys = 0; // 
        int i = 0;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // If fire input is pressed or the launcher is currently firing continue to fire
            if (Input.GetButtonDown("Fire2") || status)
            {
                status = true;
                // keep firing until all launchers have fired
                if (i < numLaunchers)
                {
                    // but wait unti a suitable time has passed since the last launcher fired
                    if (timeSinceLastShot >= fireInterval)
                    {
                        // fire a rocket, reset the timer and increment the launcher to fire
                        rocketLaunchers[i].GetComponentInChildren<RocketLauncherControl>().fire();
                        timeSinceLastShot = 0;
                        print("Fireing Rocket" + i);
                        i++;
                    }
                    else
                    {
                        // otherwise wait to fire
                        timeSinceLastShot += Time.deltaTime;
                    }
                }
                else
                {
                    // if not enough volleys have been fired repeat the fireing proccess but keep track of which volley we are on
                    firedVolleys++;
                    if (firedVolleys < numVolleys)
                    {
                        i = 0;
                        print("Fired Volley: " + firedVolleys + " out of: " + numVolleys);

                    }
                    else
                    {
                        // otherwise we are done so turn off the launcher status and reset everything for later
                        status = false;
                        i = 0;
                        firedVolleys = 0;
                    }
                }
            }

        }
    }

}
