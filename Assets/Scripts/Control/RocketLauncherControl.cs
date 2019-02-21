using UnityEngine;
using System.Collections;

namespace ShipGame
{
    public class RocketLauncherControl : MonoBehaviour
    {

        public GameObject projectile;
        private GameObject myRocket;
        public float recoveryTime;     // time needed between shots
        private float timeSinceFiring = 0;
        private bool fired = false;     // true = launcher recently fired and is not ready yet
        public bool isPartOfUnit = true; // set to false if the launcher will be directly activated from the controller instead of going through some other object
        public int numRockets; // number of rockets in barrage
        public float firingDelay; // period between shots in barrage

        // Use this for initialization
        void Start()
        {
            // on first start up setup a projectile and move it to the launcher
            myRocket = Instantiate(projectile) as GameObject;
            myRocket.transform.parent = transform;
            myRocket.transform.localPosition = projectile.transform.position;
            myRocket.transform.localRotation = projectile.transform.rotation;

        }


        // Update is called once per frame
        void Update()
        {
            // if the launcher recently fired wait for the recovery time
            if (fired == true)
            {
                timeSinceFiring += Time.deltaTime;
                if (timeSinceFiring >= recoveryTime)
                {
                    // after recovery time set launcher as ready to use, create a new projectile and it to the launcher
                    fired = false;
                    myRocket = Instantiate(projectile) as GameObject;
                    myRocket.transform.parent = transform;
                    myRocket.transform.localPosition = projectile.transform.position;
                    myRocket.transform.localRotation = projectile.transform.rotation;
                    timeSinceFiring = 0F;
                }
            }
            if (Input.GetButtonDown("Fire2") && fired == false && isPartOfUnit == false)
            {

                fire();
            }

        }

        public void fire()
        {
            // get the projectile controller and launch it
            myRocket.GetComponent<RocketControl>().Launch();
            float timeSinceLaunch = 0;
            int i = 1;
            while (i < numRockets)
            {
                if (timeSinceLaunch > firingDelay)
                {
                    myRocket = Instantiate(projectile) as GameObject;
                    myRocket.transform.parent = transform;
                    myRocket.transform.localPosition = projectile.transform.position;
                    myRocket.transform.localRotation = projectile.transform.rotation;
                    myRocket.GetComponent<RocketControl>().Launch();
                    i++;
                }
                timeSinceLaunch += Time.deltaTime;
            }
            fired = true;
        }
    }
}

