using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class LargeFloatingObject : MonoBehaviour
    {

        public float speed, currentSpeed, maxSpeed, slowDownSpeed, turnSpeed, rollSpeed, returnSpeed, rollSnap, currentRoll, currentPitch, currentYaw, rollLimit;
        private Vector3 velocity, nextPosition, positionChange, collisionVelocity;
        private float seaHeightFront, seaHeightBack, seaHeightLeft, seaHeightRight, seaHeightRightLeft, seaHeightFrontBack, seaHeightMiddle;
        private float rollFromSea, pitchFromSea, pitchWidth, rollWidth;
        private Vector3 sampleFrontLeft, SampleFrontRight, sampleBackLeft, sampleBackRight, sampleBack, sampleFront, sampleRight, sampleLeft;
        public float rollBase, pitchBase, samplingPeriod, reverseMaxSpeed, reverseAcceleration, drag;
        private float waveOffset;
        public float waveForce, rudderRange, rudderSpeed, rudderReturnRate;
        private waves ocean;
        private int averageIndex = 0;
        private float[] backAverage, frontAverage, rightAverage, leftAverage, middleAverage;
        public float windDrag, friction;
        private Wind windSettings;
        public bool sinking = false;


        void Start()
        {
            ocean = FindObjectOfType<waves>();
            windSettings = FindObjectOfType<Wind>();
            waveOffset = transform.position.y - ocean.transform.position.y;
            currentYaw = transform.eulerAngles.y;
            backAverage = new float[Mathf.CeilToInt(samplingPeriod / Time.deltaTime)];
            frontAverage = new float[Mathf.CeilToInt(samplingPeriod / Time.deltaTime)];
            rightAverage = new float[Mathf.CeilToInt(samplingPeriod / Time.deltaTime)];
            leftAverage = new float[Mathf.CeilToInt(samplingPeriod / Time.deltaTime)];
            middleAverage = new float[Mathf.CeilToInt(samplingPeriod / Time.deltaTime)];

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

            SetSeaHeight();
            nextPosition = transform.position;

            rollFromSea = Mathf.Atan2(seaHeightRightLeft, rollBase);
            pitchFromSea = Mathf.Atan2(seaHeightFrontBack, pitchBase);


            transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * -pitchFromSea, transform.right) *
                                 Quaternion.AngleAxis(Mathf.Rad2Deg * rollFromSea + currentRoll, transform.forward) *
                                 Quaternion.AngleAxis(currentYaw, Vector3.up);
            nextPosition.y = (seaHeightBack + seaHeightFront + seaHeightLeft + seaHeightRight + seaHeightMiddle) / 5 + waveOffset;
            transform.position = nextPosition;
        }




      

        private void SetSeaHeight()
        {
            sampleBackLeft = transform.position - Vector3.ProjectOnPlane(transform.forward, Vector3.up) * (pitchBase / 2) - Vector3.ProjectOnPlane(transform.right, Vector3.up) * (rollBase / 2);
            sampleBackRight = transform.position - Vector3.ProjectOnPlane(transform.forward, Vector3.up) * (pitchBase / 2) + Vector3.ProjectOnPlane(transform.right, Vector3.up) * (rollBase / 2);
            sampleFrontLeft = transform.position + Vector3.ProjectOnPlane(transform.forward, Vector3.up) * (pitchBase / 2) - Vector3.ProjectOnPlane(transform.right, Vector3.up) * (rollBase / 2);
            SampleFrontRight = transform.position + Vector3.ProjectOnPlane(transform.forward, Vector3.up) * (pitchBase / 2) + Vector3.ProjectOnPlane(transform.right, Vector3.up) * (rollBase / 2);
            sampleFront = transform.position + Vector3.ProjectOnPlane(transform.forward, Vector3.up) * (pitchBase / 2);
            sampleBack = transform.position - Vector3.ProjectOnPlane(transform.forward, Vector3.up) * (pitchBase / 2);
            sampleLeft = transform.position - Vector3.ProjectOnPlane(transform.right, Vector3.up) * (rollBase / 2);
            sampleRight = transform.position + Vector3.ProjectOnPlane(transform.right, Vector3.up) * (rollBase / 2);

            seaHeightMiddle = ocean.waveHeight(transform.position);
            seaHeightBack = (ocean.waveHeight(sampleBackLeft) + ocean.waveHeight(sampleBack) + ocean.waveHeight(sampleBackRight)) / 3.0f;
            seaHeightFront = (ocean.waveHeight(sampleFrontLeft) + ocean.waveHeight(sampleFront) + ocean.waveHeight(SampleFrontRight)) / 3.0f;
            seaHeightLeft = (ocean.waveHeight(sampleBackLeft) + ocean.waveHeight(sampleLeft) + ocean.waveHeight(sampleFrontLeft)) / 3.0f;
            seaHeightRight = (ocean.waveHeight(sampleBackRight) + ocean.waveHeight(sampleRight) + ocean.waveHeight(SampleFrontRight)) / 3.0f;

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
            sinking = true;
            StartCoroutine(SinkRoutine(direction, sinkTime, flipSpeed, sinkSpeed));
        }

        IEnumerator SinkRoutine(Vector3 direction, float sinkTime, float flipSpeed, float sinkSpeed)
        {
            float elapsedTime = 0;
            float angle = 0;
            while (elapsedTime > 0)
            {
                angle = angle > 180 ? 180 : 180 * elapsedTime / flipSpeed;
                elapsedTime += Time.deltaTime;
                transform.rotation = Quaternion.AngleAxis(angle, direction);
                transform.Translate(Vector3.up * sinkSpeed * Time.deltaTime, Space.World);
                yield return 0;
            }

        }

    }

    
}

