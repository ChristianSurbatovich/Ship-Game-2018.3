using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame.Network
{
    public class NetAgentPosition
    {
        public short agentID;
        public Vector3 currentPosition, targetPosition;
        public Vector3 velocity;
        public Quaternion currentRotation, targetRotation;
        public Vector3 angularVelocity;
        public float locationTime, targetTime, interpolationTime;
        NetAgentPosition(short id, Vector3 pos, Vector3 vel, Vector3 rot, Vector3 angVel, float t)
        {
            agentID = id;
            currentPosition = targetPosition = pos;
            currentRotation = targetRotation = Quaternion.Euler(rot);
            velocity = vel;
            angularVelocity = angVel;
            locationTime = targetTime = t;
        }
        public void UpdateTarget(PositionMessage message)
        {
            targetPosition = message.position;
            targetRotation = Quaternion.Euler(message.rotation);
            targetTime = message.time;
        }

        public void UpdateTarget(VelocityMessage message)
        {
            velocity = message.velocity;
            angularVelocity = message.angularVelocity;
        }

        public void UpdateTarget(PositionFullMessage message)
        {
            targetPosition = message.position;
            targetRotation = Quaternion.Euler(message.rotation);
            velocity = message.velocity;
            angularVelocity = message.angularVelocity;
            targetTime = message.time;
        }
    }
}
