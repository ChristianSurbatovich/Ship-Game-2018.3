using System.Collections.Generic;
using UnityEngine;
using System;
namespace ShipGame.Network
{
    // this is only a representation of the gamestate for online purposes, it is not directly related to the gameworld in unity
    public class NetworkGameState
    {
        private Dictionary<short, NetGameAgent> agentList;
        public float tolerance;
        // Use this for initialization


        public void ApplyUpdates(List<AgentMessage> updates)
        {
            foreach(AgentMessage message in updates)
            {
                switch (message.type)
                {
                    case MessageValues.ADD_AGENT:
                      //  agentList[]
                        break;
                    case MessageValues.POSITION:
                        agentList[message.id].position.UpdateTarget((PositionMessage)message);
                        break;
                    case MessageValues.POSITION_FULL:
                        agentList[message.id].position.UpdateTarget((PositionFullMessage)message);
                            break;
                    case MessageValues.VELOCITY:
                        agentList[message.id].position.UpdateTarget((VelocityMessage)message);
                        break;
                    case MessageValues.AGENT_SYNC:
                        break;
                    case MessageValues.STAT:
                        agentList[message.id].state.SetStat((StatMessage)message);
                        break;
                    case MessageValues.DESTRUCTION_STATE:
                        agentList[message.id].networkController.Action(message);
                        break;
                    case MessageValues.DESTRUCTION_STATE_RESET:
                        agentList[message.id].networkController.Action(message);
                        break;
                    case MessageValues.OPEN:
                        agentList[message.id].networkController.Action(message);
                        break;
                }

            }
        }

        public void Interpolate()
        {
            foreach(NetGameAgent agent in agentList.Values)
            {
                if (!agent.frozen && agent.interpolate && agent.ID != NetworkManager.myNetID)
                {
                    Vector3 velocityCorrection, rotationCorrection;
                    if(agent.position.targetTime > NetworkManager.netTime)
                    {
                        // get position/rotation at target time based on current parameters
                        float timeDifference = agent.position.targetTime - NetworkManager.netTime;
                        velocityCorrection = agent.position.targetPosition - (agent.transform.position + agent.position.velocity * timeDifference);
                        rotationCorrection = agent.position.targetRotation.eulerAngles - (agent.transform.rotation.eulerAngles + agent.position.angularVelocity * timeDifference);
                        velocityCorrection *= velocityCorrection.magnitude / tolerance;
                        rotationCorrection *= rotationCorrection.magnitude / tolerance;
                        velocityCorrection /= timeDifference;
                        rotationCorrection /= timeDifference;
                    }
                    else
                    {
                        // get expected rotation/position based on time past target time and current velocities
                        float timeDifference = NetworkManager.netTime - agent.position.targetTime;
                        velocityCorrection = (agent.position.targetPosition + agent.position.velocity * timeDifference) - agent.transform.position;
                        rotationCorrection = (agent.position.targetRotation.eulerAngles + agent.position.angularVelocity * timeDifference) - agent.transform.rotation.eulerAngles;
                        velocityCorrection *= velocityCorrection.magnitude / tolerance;
                        rotationCorrection *= rotationCorrection.magnitude / tolerance;
                        velocityCorrection /= timeDifference;
                        rotationCorrection /= timeDifference;
                    }
                    agent.transform.Translate(Time.smoothDeltaTime * (agent.position.velocity + velocityCorrection),Space.World);
                    agent.transform.Rotate(Time.smoothDeltaTime * (agent.position.angularVelocity + rotationCorrection), Space.World);
                }
            }
        }

        public void InterpolateClientOnly()
        {

        }
        
        public float GetStat(short agentID, short statID)
        {
            if(agentList.ContainsKey(agentID) && agentList[agentID].state.stats.ContainsKey(statID))
            {
                return agentList[agentID].state.stats[statID];
            }
            return 0;
        }

        public Vector3 AverageVectorArray(Vector3[] vectorArray)
        {
            float x, y, z;
            x = y = z = 0.0f;
            foreach(Vector3 v in vectorArray)
            {
                x += v.x;
                y += v.y;
                z += v.z;
            }

            return new Vector3(x / vectorArray.Length, y / vectorArray.Length, z / vectorArray.Length);
        }

        public Quaternion AverageQuaternionArray(Quaternion[] quaternionArray)
        {
            float x, y, z, w;
            x = y = z = w = 0.0f;
            foreach (Quaternion q in quaternionArray)
            {
                x += q.x;
                y += q.y;
                z += q.z;
                w += q.w;
            }
            float normalizeFactor = 1.0f / Mathf.Sqrt(x * x + y * y + z * z + w * w);
            return new Quaternion(x * normalizeFactor, y * normalizeFactor, z * normalizeFactor, w * normalizeFactor);
        }
    }

}
