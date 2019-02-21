using UnityEngine;
using System.Collections;

namespace ShipGame
{
    public class RocketControl : MonoBehaviour
    {
        public float RocketPower;
        public float burnTime; // time when engine is producing thrust, set to 0 for a gun type projectile
        private float engineOnTime = 0;
        public GameObject engine;
        public ParticleSystem airburst;
        public ParticleSystem waterHit;
        public ParticleSystem groundHit;
        public ParticleSystem shipHit;
        private ParticleSystem engineEffect;
        public float maxTimeOut;
        private float flightTime = 0;
        private Rigidbody rBody;
        public float gForce; // amount of gravity projectile will experience, gravity is not active during the burn time
        private int flightStage; // flight phase of the projectile
        private Vector3 orientationNew;
        private Vector3 orientationPrev;
        private Vector3 targetOrientation;
        public float launchDelay;  // amount of time after launch that engine begins applying force
        private float countDown = 0;
        private bool exploded = false;
        private float vInitial;
        private bool i1 = true;


        // Use this for initialization
        void Start()
        {
            rBody = GetComponent<Rigidbody>();
            engine = Instantiate(engine, transform.position, transform.rotation) as GameObject;
            engine.transform.parent = transform;
            engineEffect = engine.GetComponentInChildren<ParticleSystem>();
            orientationNew = transform.position;
            orientationPrev = orientationNew;

        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            if (flightStage == 1)
            {

                rBody.AddForce(Vector3.Normalize(transform.forward) * RocketPower);
                rBody.AddForce(Vector3.Normalize(transform.up) * -gForce * Time.deltaTime);


                print("Vy: " + rBody.velocity.y);
                flightStage = 2;


            }
            if (flightStage == 2)
            {
                //rBody.AddForce(transform.forward * RocketPower * Time.deltaTime);
                rBody.AddForce(Vector3.Normalize(transform.up) * -gForce * Time.deltaTime);
                if (i1 && rBody.velocity.y > 0)
                {
                    i1 = false;
                    vInitial = rBody.velocity.y;
                    print("vInitial assigned: " + rBody.velocity.y);
                }

                print("Vy: " + rBody.velocity.y);
                engineOnTime += Time.deltaTime;
                if (engineOnTime >= burnTime)
                {
                    flightStage = 3;
                }
            }
            if (flightStage == 3)
            {
                // if (rBody.velocity.y > -vInitial)
                // {
                rBody.AddForce(Vector3.Normalize(transform.up) * -gForce * Time.deltaTime);

                // }



                print("Vy: " + rBody.velocity.y);
                orientationPrev = orientationNew;
                orientationNew = transform.position;
                targetOrientation = orientationNew + (orientationNew - orientationPrev);
                transform.LookAt(targetOrientation);
                if (flightTime > maxTimeOut && !exploded)
                {
                    exploded = true;
                    Kill(airburst, transform.position, Quaternion.identity);
                }
                else
                {
                    flightTime += Time.deltaTime;
                }
            }
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Water"))
            {

                ContactPoint contact = other.contacts[0];
                Vector3 pos = contact.point;
                Kill(waterHit, pos, Quaternion.FromToRotation(Vector3.forward, Vector3.up));
            }
            else
            {
                ContactPoint contact = other.contacts[0];
                Vector3 pos = contact.point;
                Kill(airburst, pos, Quaternion.FromToRotation(Vector3.forward, Vector3.up));
            }
        }

        void Kill(ParticleSystem explosion, Vector3 orientation, Quaternion rotation)
        {
            Instantiate(explosion, orientation, rotation);
            engineEffect.Stop();
            engine.transform.parent = null;
            Destroy(gameObject);
        }

        public void Launch()
        {
            transform.SetParent(null);
            engineEffect.Play();
            flightStage = 1;
            rBody.isKinematic = false;

            //tag = "Player";

        }
    }

}
