using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class floatingObject : MonoBehaviour
    {
        public float speed, turnSpeed, rollSpeed, returnSpeed, rollSnap, currentRoll, currentPitch, currentYaw, rollLimit;
        private Vector3 velocity, nextPosition, positionChange;
        private float seaHeightFront, seaHeightBack, seaHeightLeft, seaHeightRight, seaHeightRightLeft, seaHeightFrontBack;
        private float rollFromSea, pitchFromSea, pitchWidth, rollWidth;
        private Vector3 sampleFrontLeft, SampleFrontRight, sampleBackLeft, sampleBackRight, sampleBack, sampleFront, sampleRight, sampleLeft;
        public float rollBase = 5, pitchBase = 5, samplingPeriod = 0.2f;
        private float waveOffset;
        private waves ocean;
        private int averageIndex = 0;
        private float[] backAverage, frontAverage, rightAverage, leftAverage;

        public float windDrag;
        private Wind windSettings;
        // Use this for initialization
        void Start()
        {
            ocean = FindObjectOfType<waves>();
            windSettings = FindObjectOfType<Wind>();
            waveOffset = 0;
            currentYaw = transform.eulerAngles.y;
            backAverage = new float[Mathf.FloorToInt(samplingPeriod / Time.fixedDeltaTime)];
            frontAverage = new float[Mathf.FloorToInt(samplingPeriod / Time.fixedDeltaTime)];
            rightAverage = new float[Mathf.FloorToInt(samplingPeriod / Time.fixedDeltaTime)];
            leftAverage = new float[Mathf.FloorToInt(samplingPeriod / Time.fixedDeltaTime)];

        }
        void FixedUpdate()
        {

            nextPosition = transform.position;
            nextPosition += Vector3.ProjectOnPlane(windSettings.wind, Vector3.up) * windDrag;

            positionChange = nextPosition - transform.position;


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

            backAverage[averageIndex] = seaHeightBack;
            frontAverage[averageIndex] = seaHeightFront;
            leftAverage[averageIndex] = seaHeightLeft;
            rightAverage[averageIndex] = seaHeightRight;
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
            }

            seaHeightBack /= backAverage.Length + 1;
            seaHeightFront /= frontAverage.Length + 1;
            seaHeightLeft /= leftAverage.Length + 1;
            seaHeightRight /= rightAverage.Length + 1;

            // pitch and roll from sea

            seaHeightRightLeft = seaHeightRight - seaHeightLeft;
            seaHeightFrontBack = seaHeightFront - seaHeightBack;

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



            transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * -pitchFromSea, transform.right) * Quaternion.AngleAxis(Mathf.Rad2Deg * rollFromSea + currentRoll, transform.forward) * Quaternion.AngleAxis(currentYaw, Vector3.up);
            //transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * rollFromSea, transform.forward);
            //transform.rotation = Quaternion.AngleAxis(moveHorizontal * -turnSpeed * Time.fixedDeltaTime, Vector3.up);
            Debug.DrawRay(transform.position, -Vector3.ProjectOnPlane(transform.right, Vector3.up) * (rollBase / 2), Color.green, 0);
            Debug.DrawRay(transform.position, Vector3.ProjectOnPlane(transform.right, Vector3.up) * (rollBase / 2), Color.green, 0);
            Debug.DrawRay(transform.position, -Vector3.ProjectOnPlane(transform.forward, Vector3.up) * (pitchBase / 2), Color.green, 0);
            Debug.DrawRay(transform.position, Vector3.ProjectOnPlane(transform.forward, Vector3.up) * (pitchBase / 2), Color.green, 0);
            nextPosition.y = ((seaHeightBack + seaHeightFront + seaHeightLeft + seaHeightRight) / 4);
            transform.position = nextPosition;

        }
    }
}


