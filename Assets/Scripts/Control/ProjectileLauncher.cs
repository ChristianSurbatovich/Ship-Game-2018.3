using UnityEngine;
using System.Collections;

namespace ShipGame
{
    public class ProjectileLauncher : MonoBehaviour
    {
        public GameObject hPivot, vPivot, attachPoint, effectAttachPoint;
        private GameObject projectile;
        private Vector3 aimPoint;
        private Vector3 launchVector, horizontalOnly;
        public string fireEffectName;
        private float maxRange;
        private float timesincefiring = 0;
        public bool ready;
        public float firingDelay;
        public int numRockets;
        private ParticleSystem fireEffect;
        public string projectileName;
        public GameObject typeOfProjectile;
        public float degreeRange;
        public float nearClip, verticalLimit;
        public float G, power;
        public float recovery;
        public float accuracyGrouping;
        public float accuracyRange;
        public string expsound;
        private float pitch, roll, localHorizontalRotation, localVerticalRotation, worldVerticalRotation, worldVerticalRotationLarge, worldHorizontalRotation;
        private float worldX, worldY, worldZ;
        private Vector3 worldRotation, projectionVector;
        [SerializeField]
        private ObjectPool pooler;
        [SerializeField]
        private SoundPlayer sound;
        public bool active = true;
        private void Awake()
        {
            sound = FindObjectOfType<SoundPlayer>();
            pooler = FindObjectOfType<ObjectPool>();
        }

        // Use this for initialization
        void Start()
        {
            maxRange = (power * power) / G;
            LoadProjectile();
            ready = true;

        }


        // Update is called once per frame

        /* fires the projectile if it is available */
        public void Fire(short id, Vector3 target)
        {
            if (ready && active)
            {
                AimAtPoint(target);
                projectile.GetComponent<ProjectileOld>().Launch(id);
                sound.PlaySound(expsound, effectAttachPoint.transform.position);
                fireEffect = pooler.getParticleSystem(fireEffectName, 15);
                fireEffect.transform.SetParent(effectAttachPoint.transform);
                fireEffect.transform.position = effectAttachPoint.transform.position;
                fireEffect.transform.rotation = effectAttachPoint.transform.rotation;
                fireEffect.Play();
            }
        }



        bool IsReady()
        {
            return ready && active;
        }

        /* restricts rotation of the child object to maxAngle in either direction */
        void ClampToAngle(float maxAngle, GameObject child)
        {
            // Quaternion childRotation = Quaternion.FromToRotation(child.transform.forward, transform.forward);
            float childAngle;
            Vector3 cross;
            childAngle = Vector3.Angle(transform.forward, child.transform.forward);
            cross = Vector3.Cross(transform.forward, child.transform.forward);
            if (cross.y > 0)
            {
                childAngle = -childAngle;
            }
            if (childAngle > maxAngle)
            {
                child.transform.localRotation = Quaternion.AngleAxis(-maxAngle, transform.InverseTransformVector(transform.up));
            }
            else if (childAngle < -maxAngle)
            {
                child.transform.localRotation = Quaternion.AngleAxis(maxAngle, transform.InverseTransformVector(transform.up));
            }
        }

        /* create an object of projectileType at the transform location */
        public void LoadProjectile()
        {
            if (active)
            {
                projectile = pooler.getObject(projectileName);
                projectile.transform.parent = attachPoint.transform;
                projectile.transform.localPosition = typeOfProjectile.transform.localPosition;
                projectile.transform.localRotation = typeOfProjectile.transform.localRotation;
                projectile.GetComponent<ProjectileOld>().gForce = G;
                projectile.GetComponent<ProjectileOld>().RocketPower = power;
            }

        }

        /* aim at the given point */
        public void AimAtPoint(Vector3 aimpoint)
        {
            if (!active)
            {
                return;
            }
            aimPoint = aimpoint;
            float distance2, distance3, hMin, hMax;
            Vector3 aimVector, aimVector2D;
            aimVector = aimPoint - attachPoint.transform.position;
            aimVector2D = Vector3.ProjectOnPlane(aimVector, Vector3.up);
            distance3 = aimVector.magnitude;
            distance2 = aimVector2D.magnitude;

            if (distance3 > nearClip)
            {
                projectionVector.Set(hPivot.transform.forward.x, 0.0f, hPivot.transform.forward.z);
                projectionVector.Normalize();
                roll = Vector3.SignedAngle(Vector3.up, Vector3.ProjectOnPlane(hPivot.transform.up, projectionVector), hPivot.transform.forward);
                roll = (roll > 90) ? (90 - roll) * Mathf.Deg2Rad : roll * Mathf.Deg2Rad;
                projectionVector.Set(hPivot.transform.right.x, 0.0f, hPivot.transform.right.z);
                projectionVector.Normalize();
                pitch = -Vector3.SignedAngle(Vector3.up, Vector3.ProjectOnPlane(hPivot.transform.up, projectionVector), hPivot.transform.right);
                pitch = (pitch > 90) ? (90 - pitch) * Mathf.Deg2Rad : pitch * Mathf.Deg2Rad;

                // the total vertical rotation needed, some of this might need to be put into the horizontal rotation depending on the roll/pitch of the ship
                if (distance3 < maxRange)
                {
                    worldVerticalRotation = -Mathf.Atan2(((power * power) - Mathf.Sqrt(Mathf.Pow(power, 4) - G * (G * distance2 * distance2 + 2 * aimVector.y * power * power))), (G * distance2));
                    worldVerticalRotationLarge = -Mathf.Atan2(((power * power) + Mathf.Sqrt(Mathf.Pow(power, 4) - G * (G * distance2 * distance2 + 2 * aimVector.y * power * power))), (G * distance2));
                }
                else
                {
                    // if the aimpoint is out of range then the vertical angle should be 45 degrees
                    worldVerticalRotation = Mathf.PI / 4;
                    worldVerticalRotationLarge = worldVerticalRotation;
                    //print("out of range");
                }
                worldHorizontalRotation = Vector3.Angle(Vector3.ProjectOnPlane(transform.forward, Vector3.up), aimVector2D) * Mathf.Deg2Rad;
                Vector3 cross = Vector3.Cross(Vector3.ProjectOnPlane(transform.forward, Vector3.up), aimVector2D);
                if (cross.y < 0)
                {
                    worldHorizontalRotation = -worldHorizontalRotation;
                }

                worldVerticalRotation += pitch;
                localVerticalRotation = Mathf.Cos(roll) * worldVerticalRotation * Mathf.Rad2Deg;

                localHorizontalRotation = Mathf.Cos(roll) * worldHorizontalRotation * Mathf.Rad2Deg;

                localHorizontalRotation = localHorizontalRotation > degreeRange ? degreeRange : (localHorizontalRotation < -degreeRange ? -degreeRange : localHorizontalRotation);
                localVerticalRotation = localVerticalRotation > verticalLimit ? verticalLimit : (localVerticalRotation < -verticalLimit ? -verticalLimit : localVerticalRotation);
                // rotate the horizontal pivot locally
                hPivot.transform.localRotation = Quaternion.AngleAxis(localHorizontalRotation, transform.InverseTransformVector(transform.up));
                // rotate the vertical pivot locally
                vPivot.transform.localRotation = Quaternion.AngleAxis(localVerticalRotation, vPivot.transform.InverseTransformVector(vPivot.transform.right));
            }
        }
    }
}
