using UnityEngine;
using System;
using ShipGame.Network;
using ShipGame.Destruction;
namespace ShipGame
{
    public class ProjectileOld : MonoBehaviour
    {

        public float RocketPower;
        private ParticleSystem airburst;
        private ParticleSystem trail;
        public string trailName, airburstName, explosionName;
        public float maxTimeOut;
        private float flightTime = 0;
        private Rigidbody rBody;
        public float gForce; // amount of gravity projectile will experience, gravity is not active during the burn time
        private bool launched; // flight phase of the projectile
        private Vector3 orientationNew;
        private Vector3 orientationPrev;
        private Vector3 targetOrientation;
        private float countDown = 0;
        private bool exploded = false;
        private Explosion damage;
        private Wind windSettings;
        public float windDrag;
        public float explosionDamage;
        public float explosionForce;
        public float explosionRadius;
        public float explosionUp;
        public string typeOfDamage;
        private ObjectPool pooler;
        private SoundPlayer sound;
        public short owner, myNetID;
        private bool hit = false;
 //       private byte[] hitMessage = new byte[MessageLengths.HIT + 2];
        public bool hideOnHit = true;
        public short weaponTypeID;
        // Use this for initialization
        void Awake()
        {
            //print("running projectile start");
            rBody = GetComponent<Rigidbody>();
            rBody.isKinematic = true;
            orientationNew = transform.forward;
            orientationPrev = transform.position;
            launched = false;
            GetComponent<Collider>().enabled = false;
            pooler = FindObjectOfType<ObjectPool>();
            sound = FindObjectOfType<SoundPlayer>();
            // windSettings = FindObjectOfType<Wind>();
        }
        private void OnEnable()
        {
            launched = false;
            GetComponentInChildren<MeshRenderer>().enabled = true;
            exploded = false;
        }

        void FixedUpdate()
        {
            if (launched)
            {
                // rBody.AddForce(windSettings.wind * windDrag);
                rBody.AddForce(Vector3.up * -gForce, ForceMode.Acceleration);
                //orientationPrev = orientationNew;
                //  orientationNew = transform.position;
                if (flightTime > 0.05)
                {


                    //     targetOrientation = orientationNew + (orientationNew - orientationPrev);
                    //      transform.LookAt(targetOrientation);
                    GetComponent<Collider>().enabled = true;
                }
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
            if (hit)
            {
                return;
            }
            hit = true;
            // if collided with a destroyable object type it should be passed the resulting explosion for calculating destruction
            if (launched && !other.gameObject.CompareTag("Projectile") && !other.gameObject.CompareTag("Water"))
            {
                if (owner != NetworkManager.Net.NetID())
                {
                    if (trail)
                    {
                        trail.Stop();
                        trail.transform.SetParent(pooler.transform);
                        gameTimer.DeactivateAfter(15, trail.gameObject);
                    }
                    launched = false;
                    exploded = true;
                    GetComponent<Collider>().enabled = false;
                    if (hideOnHit)
                    {
                        GetComponentInChildren<MeshRenderer>().enabled = false;
                    }
                    rBody.isKinematic = true;
                    transform.SetParent(other.gameObject.transform);
                    StartCoroutine(gameTimer.DeactivateAndReturn(15, gameObject, pooler.transform));
                    return;
                }
                INetworkController otherNetObject = other.gameObject.GetComponentInParent(typeof(INetworkController)) as INetworkController;
               // Array.Copy(BitConverter.GetBytes(MessageLengths.HIT), 0, hitMessage, 0, 2);
      //          Array.Copy(BitConverter.GetBytes(MessageValues.HIT), 0, hitMessage, 2, 1);
                if (otherNetObject != null)
                {
                    Vector3 offset = transform.position - other.gameObject.transform.root.position;
          //          Array.Copy(BitConverter.GetBytes(true), 0, hitMessage, 3, 1);
              //      Array.Copy(BitConverter.GetBytes(otherNetObject.GetID()), 0, hitMessage, 4, 2);
                 //   Array.Copy(BitConverter.GetBytes(weaponTypeID), 0, hitMessage, 6, 2);
                 //   Array.Copy(BitConverter.GetBytes(offset.x), 0, hitMessage, 8, 4);
                 //   Array.Copy(BitConverter.GetBytes(offset.y), 0, hitMessage, 12, 4);
                 //   Array.Copy(BitConverter.GetBytes(offset.z), 0, hitMessage, 16, 4);
                  //  NetworkManager.Net.SendNetworkMessage(hitMessage);
                }
                else
                {
                 //   Array.Copy(BitConverter.GetBytes(false), 0, hitMessage, 3, 1);
                 //   Array.Copy(BitConverter.GetBytes((short)0), 0, hitMessage, 4, 2);
                  //  Array.Copy(BitConverter.GetBytes(weaponTypeID), 0, hitMessage, 6, 2);
                 //   Array.Copy(BitConverter.GetBytes(transform.position.x), 0, hitMessage, 8, 4);
                 //   Array.Copy(BitConverter.GetBytes(transform.position.y), 0, hitMessage, 12, 4);
                 //   Array.Copy(BitConverter.GetBytes(transform.position.z), 0, hitMessage, 16, 4);
                   // NetworkManager.Net.SendNetworkMessage(hitMessage);
                }
                if (trail)
                {
                    trail.Stop();
                    trail.transform.SetParent(pooler.transform);
                    gameTimer.DeactivateAfter(15, trail.gameObject);
                }
                launched = false;
                exploded = true;
                GetComponentInChildren<Collider>().enabled = false;
                if (hideOnHit)
                {
                    GetComponentInChildren<MeshRenderer>().enabled = false;
                }

                rBody.isKinematic = true;
                transform.SetParent(other.gameObject.transform);
                StartCoroutine(gameTimer.DeactivateAndReturn(15, gameObject, pooler.transform));
                return;
                damage = pooler.getObject<Explosion>(explosionName, 15f);
                damage.transform.SetParent(other.transform);
                damage.transform.position = transform.position;
                damage.transform.rotation = transform.rotation;
                damage.explode(other.contacts[0].point);
            }
            else if (other.gameObject.CompareTag("Water"))
            {
                if (trail)
                {
                    trail.Stop();
                    trail.transform.SetParent(pooler.transform);
                    gameTimer.DeactivateAfter(15, trail.gameObject);
                }
                launched = false;
                exploded = true;

                GetComponentInChildren<MeshRenderer>().enabled = false;

                GetComponent<Collider>().enabled = false;

                rBody.isKinematic = true;
                transform.SetParent(other.gameObject.transform);
                StartCoroutine(gameTimer.DeactivateAndReturn(15, gameObject, pooler.transform));
            }
        }

        void Kill(ParticleSystem explosion, Vector3 orientation, Quaternion rotation)
        {
            //Instantiate(explosion, orientation, rotation);
            damage.explode(transform.position);
            trail.Stop();
            trail.transform.SetParent(pooler.transform);
            gameTimer.DeactivateAfter(15, trail.gameObject);
            GetComponent<SphereCollider>().enabled = false;
            GetComponentInChildren<MeshRenderer>().enabled = false;
            rBody.isKinematic = true;
            StartCoroutine(gameTimer.DeactivateAndReturn(15, gameObject, pooler.transform));
            //Destroy(gameObject);
        }

        public void Launch(short id)
        {
            owner = id;
            if (!launched)
            {
                if (rBody == null)
                {
                    rBody = gameObject.AddComponent<Rigidbody>();
                    rBody.useGravity = false;
                }
                trail = pooler.getParticleSystem(trailName);
                trail.transform.SetParent(transform);
                trail.transform.position = transform.position;
                trail.Play();
                flightTime = 0;
                transform.parent = null;
                rBody.isKinematic = false;
                rBody.AddForce(transform.forward * RocketPower, ForceMode.Impulse);
                launched = true;
                hit = false;
                //tag = "Player";
            }
        }

    }
}
