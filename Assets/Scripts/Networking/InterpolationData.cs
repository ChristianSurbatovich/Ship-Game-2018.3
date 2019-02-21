using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame.Network
{
    public class InterpolationData
    {
        public Vector3 velocity;
        public Quaternion oldRotation, newRotation;
        public float interpolationTime, totalInterpolationTime;

        public InterpolationData(Vector3 v, Quaternion rotation1, Quaternion rotation2, float time1, float time2)
        {
            velocity = v;
            oldRotation = rotation1;
            newRotation = rotation2;
            interpolationTime = time1;
            totalInterpolationTime = time2;
        }

    }

}
