using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ShipGame
{
    public class ShipControl : MonoBehaviour, IShipControl
    {
        [SerializeField]
        private float seaHeightOffset;
        public float speed, currentSpeed, maxSpeed, slowDownSpeed, turnSpeed, rollSpeed, returnSpeed, rollSnap, currentRoll, currentPitch, currentYaw, rollLimit;
        private Vector3 velocity, nextPosition, positionChange, collisionVelocity;
        private float seaHeightFront, seaHeightBack, seaHeightLeft, seaHeightRight, seaHeightRightLeft, seaHeightFrontBack, seaHeightMiddle;
        private float rollFromSea, pitchFromSea, pitchWidth, rollWidth;
        private Vector3 sampleFrontLeft, SampleFrontRight, sampleBackLeft, sampleBackRight, sampleBack, sampleFront, sampleRight, sampleLeft;
        public float rollBase, pitchBase, samplingPeriod, reverseMaxSpeed, reverseAcceleration, drag;
        private Rigidbody rb;
        private float reverse = 1;
        private ParticleSystem wake;
        public ParticleSystem wakeEffect;
        private bool moving;
        public float wakeAmount, wakeValue;
        public float waveOffset;
        public float waveForce, rudderRange, rudderSpeed, rudderReturnRate;
        private waves ocean;
        public Transform front, back, left, right, middle;
        public GameObject rudder;
        private float rudderRotation;
        private int averageIndex = 0;
        private float[] backAverage, frontAverage, rightAverage, leftAverage, middleAverage;
        public float bounceScalar;
        private GameObject otherShip;
        private ShipControl otherShipControl;
        private bool inCollision = false;
        public float windDrag, friction;
        private Wind windSettings;
        private Vector3 impactVector;
        private float impactAngle, collisionPitchTotal, collisionPitchCurrent, collisionRollTotal, collisionRollCurrent, collisionYawTotal, collisionYawCurrent, pitchBack, rollBack;
        private float collisionPitchSpeed, collisionRollSpeed, collisionYawSpeed;
        public Vector3 collisionForceVector;
        private bool collisionPitching, collisionRolling, pitchDone, rollDone, collisionYaw;

        private float moveHorizontal;
        private float moveForward;

        private bool doorOpen;
        private float doorPositionLeft;
        public GameObject[] doorsLeft;
        public GameObject[] doorsRight;
        private float rotationAmount;
        public float openSpeed;
        public float maxOpenAngle;
        private bool doorMotion;
        private Vector3 currentRotation;
        public float extendRange;
        public float extendSpeed;
        private float currentExtension;
        private float extendAmount;
        public GameObject gunBaseRight, gunBaseLeft;
        private bool gunMotion;
        private bool gunExtended;
        public bool sinking;
        private float sinkOffset = 0.0f;
        public float sinkDrag = 1.0f;
        public float sinkDragAmount;
        public FireControlGroup[] weapons;

        private Dictionary<string, GameObject> attachPoints;

        void Start()
        {
            doorMotion = false;
            doorOpen = false;
            gunMotion = false;
            gunExtended = false;
            doorPositionLeft = 0;
            rb = GetComponent<Rigidbody>();
            wake = Instantiate(wakeEffect, transform.position, transform.rotation) as ParticleSystem;
            wake.transform.SetParent(transform);
            wake.transform.localPosition = wakeEffect.transform.position;
            ocean = FindObjectOfType<waves>();
            windSettings = FindObjectOfType<Wind>();
           // waveOffset = transform.position.y - ocean.transform.position.y;
            wakeValue = wake.emission.rateOverTime.constant;
            currentYaw = 0;
            rudderRotation = 0;
            backAverage = new float[Mathf.CeilToInt(samplingPeriod / Time.deltaTime)];
            frontAverage = new float[Mathf.CeilToInt(samplingPeriod / Time.deltaTime)];
            rightAverage = new float[Mathf.CeilToInt(samplingPeriod / Time.deltaTime)];
            leftAverage = new float[Mathf.CeilToInt(samplingPeriod / Time.deltaTime)];
            middleAverage = new float[Mathf.CeilToInt(samplingPeriod / Time.deltaTime)];
            sinking = false;
            for (int i = averageIndex; i < backAverage.Length; i++)
            {
                backAverage[i] = 0;
                frontAverage[i] = 0;
                leftAverage[i] = 0;
                rightAverage[i] = 0;
                middleAverage[i] = 0;
            }
        }



        void Update()
        {


            if (currentSpeed < 0)
            {
                reverse = -1;
            }
            else
            {
                reverse = 1;
            }

            if (moveForward > 0)
            {
                currentSpeed = currentSpeed + speed * moveForward * Time.deltaTime;
            }
            else if (moveForward < 0)
            {
                currentSpeed = currentSpeed + reverseAcceleration * moveForward * Time.deltaTime;
            }
            else
            {
                if (currentSpeed > (slowDownSpeed * Time.deltaTime * sinkDrag))
                {
                    currentSpeed = currentSpeed - slowDownSpeed * Time.deltaTime * sinkDrag;
                }
                else if (currentSpeed < (-slowDownSpeed * Time.deltaTime * sinkDrag))
                {
                    currentSpeed = currentSpeed + slowDownSpeed * Time.deltaTime * sinkDrag;
                }

            }

            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
            else if (currentSpeed < -reverseMaxSpeed)
            {
                currentSpeed = -reverseMaxSpeed;
            }



            nextPosition = transform.position;
            // nextPosition = nextPosition + collisionForceVector * Time.deltaTime;
            // nextPosition = nextPosition + Vector3.ProjectOnPlane(windSettings.wind, Vector3.up) * windDrag;
            nextPosition = nextPosition + transform.forward * currentSpeed * Time.smoothDeltaTime;
            positionChange = nextPosition - transform.position;


            if (collisionForceVector.x > 0)
            {
                collisionForceVector.x -= drag * Time.deltaTime;
            }
            else if (collisionForceVector.x < 0)
            {
                collisionForceVector.x += drag * Time.deltaTime;
            }

            if (collisionForceVector.y > 0)
            {
                collisionForceVector.y -= drag * Time.deltaTime;
            }
            else if (collisionForceVector.y < 0)
            {
                collisionForceVector.y += drag * Time.deltaTime;
            }
            if (collisionForceVector.z > 0)
            {
                collisionForceVector.z -= drag * Time.deltaTime;
            }
            else if (collisionForceVector.z < 0)
            {
                collisionForceVector.z += drag * Time.deltaTime;
            }

            SetSeaHeight();


            rollFromSea = Mathf.Atan2(seaHeightRightLeft, rollBase);
            pitchFromSea = Mathf.Atan2(seaHeightFrontBack, pitchBase);


            // print(nextPosition.y);

            /* print("Roll from sea: " + rollFromSea * Mathf.Rad2Deg);
             print("Difference in sea heigh from left to right: " + seaHeightRightLeft);
             print("Roll baselength: " + rollBase);
             print("Sea height on left: " + seaHeightLeft);
             print("Sea height on right: " + seaHeightRight);
             */




            // print("Pitch from sea: " + pitchFromSea * Mathf.Rad2Deg);

            if (collisionPitching)
            {
                if (collisionPitchCurrent > collisionPitchTotal || collisionPitchCurrent < -collisionPitchTotal)
                {
                    pitchBack = -pitchBack;
                    pitchDone = true;
                }
                if (Mathf.Sign(collisionPitchCurrent) != Mathf.Sign(collisionPitchTotal) && pitchDone)
                {
                    collisionPitching = false;
                    collisionPitchCurrent = 0;
                }
                else
                {
                    collisionPitchCurrent += Time.deltaTime * pitchBack * collisionPitchSpeed;
                }

            }

            if (collisionRolling)
            {
                if (collisionRollCurrent > collisionRollTotal || collisionRollCurrent < -collisionRollTotal)
                {
                    rollBack = -rollBack;
                    rollDone = true;
                }
                if (Mathf.Sign(collisionRollCurrent) != Mathf.Sign(collisionRollTotal) && rollDone)
                {
                    collisionRolling = false;
                    collisionRollCurrent = 0;
                }
                else
                {
                    collisionRollCurrent += Time.deltaTime * rollBack * collisionRollSpeed;
                }
            }

            if (collisionYaw)
            {
                if (collisionYawCurrent > collisionYawTotal || collisionYawCurrent < -collisionYawTotal)
                {
                    collisionYaw = false;
                }
                else
                {
                    currentYaw += Time.deltaTime * collisionYawSpeed;
                    collisionYawCurrent += Time.deltaTime * collisionYawSpeed;
                }
            }

            currentYaw += moveHorizontal * turnSpeed * Time.deltaTime;

            currentRoll += moveHorizontal * rollSpeed * Time.deltaTime;

            rudderRotation += -moveHorizontal * rudderSpeed * Time.deltaTime;

            currentRoll = currentRoll > rollLimit ? rollLimit : (currentRoll < -rollLimit ? -rollLimit : currentRoll);

            rudderRotation = rudderRotation > rudderRange ? rudderRange : (rudderRotation < -rudderRange ? -rudderRange : rudderRotation);

            rudderRotation += moveHorizontal == 0 ? (rudderRotation > rollSnap ? -rudderReturnRate : (rudderRotation < -rollSnap ? rudderReturnRate : 0)) : 0;

            currentRoll += moveHorizontal == 0 ? (currentRoll > rollSnap ? -returnSpeed : (currentRoll < -rollSnap ? returnSpeed : 0)) : 0;


            if (moveHorizontal == 0)
            {
                if (rudderRotation < -rollSnap)
                {
                    rudderRotation += rudderReturnRate;
                }
                else if (rudderRotation > rollSnap)
                {
                    rudderRotation -= rudderReturnRate;
                }
            }

            rudder.transform.localRotation = Quaternion.AngleAxis(rudderRotation * reverse, transform.InverseTransformVector(transform.up));


            transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * -pitchFromSea + collisionPitchCurrent, transform.right) *
                                 Quaternion.AngleAxis(Mathf.Rad2Deg * rollFromSea + currentRoll + collisionRollCurrent, transform.forward) *
                                 Quaternion.AngleAxis(currentYaw, Vector3.up);
            nextPosition.y = (seaHeightBack + seaHeightFront + seaHeightLeft + seaHeightRight + seaHeightMiddle * 3) / 7 + waveOffset + sinkOffset;
            velocity = (nextPosition - transform.position) / Time.deltaTime;
            collisionVelocity = velocity;
            transform.position = nextPosition;
        }

        public void SetMovement(float x, float y)
        {
            if (sinking)
            {
                return;
            }
            moveHorizontal = x;
            moveForward = y;
        }
        public void OpenGunports()
        {
            if (!doorMotion && !doorOpen)
            {
                doorMotion = true;
                StartCoroutine(OpenDoor());
            }
            if (!doorMotion && doorOpen)
            {
                doorMotion = true;
                StartCoroutine(CloseDoor());
            }
        }

        private Vector3 targetVector;
        private float impactSpeed, collisionOffset;
        public void CollideWith(Collision other)
        {
            Debug.DrawRay(other.contacts[0].point, other.contacts[0].normal * 5, Color.green, 15);
            impactVector = transform.position - other.contacts[0].point;
            impactVector.Normalize();
            Debug.DrawRay(other.contacts[0].point, impactVector * 5, Color.red, 15);
            impactAngle = Vector3.SignedAngle(Vector3.ProjectOnPlane(other.contacts[0].normal, Vector3.up), Vector3.ProjectOnPlane(impactVector, Vector3.up), Vector3.up);
            inCollision = true;
            impactSpeed = currentSpeed;
            collisionForceVector = Vector3.ProjectOnPlane(other.contacts[0].normal, Vector3.up) * bounceScalar * Mathf.Cos(impactAngle * Mathf.Deg2Rad) * currentSpeed;
            currentSpeed = currentSpeed * Mathf.Abs(Mathf.Sin(impactAngle * Mathf.Deg2Rad)) * friction;


            if (Vector3.Cross(Vector3.ProjectOnPlane(other.contacts[0].normal, Vector3.up), Vector3.ProjectOnPlane(impactVector, Vector3.up)).y > 0)
            {
                collisionYawTotal = (90 - impactAngle) * Mathf.Abs(Mathf.Sin(impactAngle * Mathf.Deg2Rad));
                collisionYawSpeed = Mathf.Pow(Mathf.Sin(impactAngle * Mathf.Deg2Rad) * Mathf.Cos(impactAngle), 2) * impactSpeed;
                collisionRollTotal = collisionYawTotal / 2.0f;
                collisionRollSpeed = Mathf.Pow(Mathf.Sin(impactAngle * Mathf.Deg2Rad) * Mathf.Cos(impactAngle), 2) * impactSpeed;
            }
            else
            {
                collisionYawTotal = (90 - impactAngle) * Mathf.Abs(Mathf.Sin(impactAngle * Mathf.Deg2Rad));
                collisionYawSpeed = -Mathf.Pow(Mathf.Sin(impactAngle * Mathf.Deg2Rad) * Mathf.Cos(impactAngle), 2) * impactSpeed;
                collisionRollTotal = collisionYawTotal / 2.0f;
                collisionRollSpeed = -Mathf.Pow(Mathf.Sin(impactAngle * Mathf.Deg2Rad) * Mathf.Cos(impactAngle), 2) * impactSpeed;
            }

            collisionPitchTotal = Mathf.Cos(impactAngle * Mathf.Deg2Rad) * impactSpeed / 4 * (transform.position.y - other.contacts[0].point.y);
            collisionPitchSpeed = Mathf.Cos(impactAngle * Mathf.Deg2Rad) * impactSpeed / 4;

            collisionPitchCurrent = 0;
            collisionYawCurrent = 0;
            collisionRollCurrent = 0;
            collisionYaw = true;
            collisionPitching = true;
            collisionRolling = true;
            rollBack = 1;
            pitchBack = 1;
            pitchDone = false;
            rollDone = false;

            collisionOffset = Mathf.Abs(other.contacts[0].separation);
            transform.position += Vector3.ProjectOnPlane(other.contacts[0].normal * collisionOffset, Vector3.up);

            // roll away from collision based on velocity


            // pitch away from collision based on velocity

            // yaw away from collision based on velocity

            // lower speed based on collision angle and velocity
        }

        public void StopCollision(Collision other)
        {
            inCollision = false;
        }

        public void InCollision(Collision other)
        {
            collisionOffset = Mathf.Abs(other.contacts[0].separation);
            transform.position += Vector3.ProjectOnPlane(other.contacts[0].normal * collisionOffset, Vector3.up);
        }
        IEnumerator OpenDoor()
        {
            Debug.Log("Opening doors");
            while (doorPositionLeft < maxOpenAngle)
            {
                rotationAmount = openSpeed * Time.deltaTime;
                doorPositionLeft += rotationAmount;
                for (int i = 0; i < doorsLeft.Length; i++)
                {
                    /*currentRotation = doorsLeft[i].transform.localEulerAngles;
                    currentRotation.z += rotationAmount;
                    doorsLeft[i].transform.localEulerAngles = currentRotation;
                    currentRotation = doorsRight[i].transform.localEulerAngles;
                    currentRotation.z -= rotationAmount;
                    doorsRight[i].transform.localEulerAngles = currentRotation;
                    */
                    // doorsLeft[i].transform.Rotate(0, -rotationAmount, 0);
                    //  doorsRight[i].transform.Rotate(0, rotationAmount, 0);
                    doorsLeft[i].transform.localRotation = Quaternion.AngleAxis(-doorPositionLeft, doorsLeft[i].transform.InverseTransformVector(doorsLeft[i].transform.forward));
                    doorsRight[i].transform.localRotation = Quaternion.AngleAxis(doorPositionLeft, doorsRight[i].transform.InverseTransformVector(doorsRight[i].transform.forward));
                }

                yield return 0;
            }

            doorMotion = false;
            doorOpen = true;
            if (!gunExtended && !gunMotion)
            {
                StartCoroutine(ExtendGun());
            }
        }

        IEnumerator CloseDoor()
        {
            Debug.Log("Closing doors");
            if (!gunMotion && gunExtended)
            {
                StartCoroutine(RetractGun());
            }
            while (doorPositionLeft > 0)
            {
                rotationAmount = openSpeed * Time.deltaTime;
                doorPositionLeft -= rotationAmount;
                for (int i = 0; i < doorsLeft.Length; i++)
                {
                    /*currentRotation = doorsLeft[i].transform.localEulerAngles;
                    currentRotation.z += rotationAmount;
                    doorsLeft[i].transform.localEulerAngles = currentRotation;
                    currentRotation = doorsRight[i].transform.localEulerAngles;
                    currentRotation.z -= rotationAmount;
                    doorsRight[i].transform.localEulerAngles = currentRotation;
                    */
                    doorsLeft[i].transform.localRotation = Quaternion.AngleAxis(-doorPositionLeft, doorsLeft[i].transform.InverseTransformVector(doorsLeft[i].transform.forward));
                    doorsRight[i].transform.localRotation = Quaternion.AngleAxis(doorPositionLeft, doorsRight[i].transform.InverseTransformVector(doorsRight[i].transform.forward));
                }
                    
                yield return 0;
            }

            doorMotion = false;
            doorOpen = false;
        }

        IEnumerator ExtendGun()
        {
            Debug.Log("extending guns");
            while (currentExtension < extendRange)
            {
                extendAmount = extendSpeed * Time.deltaTime;
                currentExtension += extendAmount;
                gunBaseRight.transform.Translate(new Vector3(extendAmount, 0, 0));
                gunBaseLeft.transform.Translate(new Vector3(extendAmount, 0, 0));
                yield return 0;
            }

            gunMotion = false;
            gunExtended = true;
        }
        IEnumerator RetractGun()
        {
            Debug.Log("retracting guns");
            while (currentExtension > 0)
            {
                extendAmount = -extendSpeed * Time.deltaTime;
                currentExtension += extendAmount;
                gunBaseRight.transform.Translate(new Vector3(extendAmount, 0, 0));
                gunBaseLeft.transform.Translate(new Vector3(extendAmount, 0, 0));
                yield return 0;
            }

            gunMotion = false;
            gunExtended = false;
        }

        private void SetSeaHeight()
        {
            /*
            sampleBackLeft = transform.position - Vector3.ProjectOnPlane(transform.forward, Vector3.up) * (pitchBase / 2) - Vector3.ProjectOnPlane(transform.right, Vector3.up) * (rollBase / 2);
            sampleBackRight = transform.position - Vector3.ProjectOnPlane(transform.forward, Vector3.up) * (pitchBase / 2) + Vector3.ProjectOnPlane(transform.right, Vector3.up) * (rollBase / 2);
            sampleFrontLeft = transform.position + Vector3.ProjectOnPlane(transform.forward, Vector3.up) * (pitchBase / 2) - Vector3.ProjectOnPlane(transform.right, Vector3.up) * (rollBase / 2);
            SampleFrontRight = transform.position + Vector3.ProjectOnPlane(transform.forward, Vector3.up) * (pitchBase / 2) + Vector3.ProjectOnPlane(transform.right, Vector3.up) * (rollBase / 2);
            sampleFront = transform.position + Vector3.ProjectOnPlane(transform.forward, Vector3.up) * (pitchBase / 2);
            sampleBack = transform.position - Vector3.ProjectOnPlane(transform.forward, Vector3.up) * (pitchBase / 2);
            sampleLeft = transform.position - Vector3.ProjectOnPlane(transform.right, Vector3.up) * (rollBase / 2);
            sampleRight = transform.position + Vector3.ProjectOnPlane(transform.right, Vector3.up) * (rollBase / 2);


            seaHeightBack = (ocean.waveHeight(sampleBackLeft) + ocean.waveHeight(sampleBack) + ocean.waveHeight(sampleBackRight)) / 3.0f;
            seaHeightFront = (ocean.waveHeight(sampleFrontLeft) + ocean.waveHeight(sampleFront) + ocean.waveHeight(SampleFrontRight)) / 3.0f;
            seaHeightLeft = (ocean.waveHeight(sampleBackLeft) + ocean.waveHeight(sampleLeft) + ocean.waveHeight(sampleFrontLeft)) / 3.0f;
            seaHeightRight = (ocean.waveHeight(sampleBackRight) + ocean.waveHeight(sampleRight) + ocean.waveHeight(SampleFrontRight)) / 3.0f;
            seaHeightMiddle = ocean.waveHeight(transform.position);
            */

            seaHeightBack = ocean.waveHeight(back.position);
            seaHeightFront = ocean.waveHeight(front.position);
            seaHeightLeft = ocean.waveHeight(left.position);
            seaHeightRight = ocean.waveHeight(right.position);
            seaHeightMiddle = ocean.waveHeight(middle.position);
            backAverage[averageIndex] = seaHeightBack;
            frontAverage[averageIndex] = seaHeightFront;
            leftAverage[averageIndex] = seaHeightLeft;
            rightAverage[averageIndex] = seaHeightRight;
            middleAverage[averageIndex] = seaHeightMiddle;
            averageIndex++;
            if (averageIndex > (backAverage.Length - 1))
            {
                averageIndex = 0;
            }
            for (int i = 0; i < backAverage.Length; i++)
            {
                seaHeightBack += backAverage[i];
                seaHeightFront += frontAverage[i];
                seaHeightLeft += leftAverage[i];
                seaHeightRight += rightAverage[i];
                seaHeightMiddle += middleAverage[i];
            }

            seaHeightBack /= backAverage.Length + 1;
            seaHeightFront /= frontAverage.Length + 1;
            seaHeightLeft /= leftAverage.Length + 1;
            seaHeightRight /= rightAverage.Length + 1;
            seaHeightMiddle /= middleAverage.Length + 1;

            // pitch and roll from sea

            seaHeightRightLeft = seaHeightRight - seaHeightLeft;
            seaHeightFrontBack = seaHeightFront - seaHeightBack;
        }

        public void Sink(Vector3 direction, float sinkTime, float flipSpeed, float sinkSpeed)
        {
            if (sinking)
            {
                return;
            }
            wakeValue = 0;
            sinkDrag = sinkDragAmount;
            sinking = true;
            StartCoroutine(SinkRoutine(direction, sinkTime, flipSpeed, sinkSpeed));
        }

        IEnumerator SinkRoutine(Vector3 direction, float sinkTime, float flipSpeed, float sinkSpeed)
        {
            float elapsedTime = 0;
            float angle = 0;
            while (elapsedTime < sinkTime)
            {
                angle = angle >= 90 ? 90 : 90 * elapsedTime / flipSpeed;
                elapsedTime += Time.deltaTime;
                //transform.Rotate(direction, angle, Space.World);
                transform.rotation = transform.rotation * Quaternion.AngleAxis(angle, direction);
                sinkOffset += -sinkSpeed * Time.deltaTime * elapsedTime;
                yield return 0;
            }

        }

        public Vector3[] FireWeapon(Vector3 aimPoint, short id, short weaponID)
        {
            return weapons[weaponID].Fire(aimPoint, id);
        }
        public void AimAtPoint(Vector3 aimPoint)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].AimAt(aimPoint);
            }
        }

        public void FireWeapon(Vector3[] aimPoints, short id, short weaponID)
        {
            weapons[weaponID].Fire(aimPoints, id);
        }

        public void SetStat(short statID, float value)
        {
            switch (statID){
                case Stats.SPEED:
                    maxSpeed = value;
                    break;
                case Stats.CURRENT_SPEED:
                    currentSpeed = value;
                    break;
                case Stats.ACCELERATION:
                    speed = value;
                    break;
                   
            }
        }
        public void SetStat(short statID, int value)
        {
            switch (statID)
            {
                default:
                    break;
            }
        }

        public float GetStat(short StatID)
        {
            switch (StatID)
            {
                case Stats.SPEED:
                    return speed;
                case Stats.CURRENT_SPEED:
                    return currentSpeed;
                default:
                    return 0;
            }
        }

        public float GetAbilityCooldown(short id)
        {
            return weapons[id].coolDown;
        }

        public void SetStat(byte[] action)
        {
            throw new System.NotImplementedException();
        }

        public Vector3 Position()
        {
            return transform.position;
        }

        public Vector3 LocalRotation()
        {
            return transform.localEulerAngles;
        }

        public GameObject GetAttachPoint(string name)
        {
            return attachPoints[name];
        }

        public void SeaHeight(ref float[] outArray)
        {
            outArray[0] = seaHeightFront;
            outArray[1] = seaHeightRight;
            outArray[2] = seaHeightBack;
            outArray[3] = seaHeightLeft;
            outArray[4] = seaHeightMiddle;
        }
    }
}
