using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame.Network
{
    public class GameMessage
    {
        public byte type;
        public GameMessage(byte t)
        {
            type = t;
        }
    }

    public class AgentMessage : GameMessage
    {
        public short id;
        public AgentMessage(byte t, short i) : base(t)
        {
            id = i;
        }
    }

    public class SyncMessage : GameMessage
    {
        public float serverNetTime;

        public SyncMessage(byte t, float time) : base(t)
        {
            serverNetTime = time;
        }
    }

    public class PositionMessage : AgentMessage
    {
        public Vector3 position;
        public Vector3 rotation;
        public float time;
        public PositionMessage(byte t, short i, Vector3 pos, Vector3 rot, float ti) : base(t, i)
        {
            position = pos;
            rotation = rot;
            time = ti;
        }
    }

    public class PositionFullMessage : PositionMessage
    {
        public Vector3 velocity;
        public Vector3 angularVelocity;
        public PositionFullMessage(byte t, short i, Vector3 p, Vector3 r, Vector3 v, Vector3 av, float ti) : base(t, i, p, r, ti)
        {
            velocity = v;
            angularVelocity = av;
        }
    }

    public class VelocityMessage : AgentMessage
    {
        public Vector3 velocity;
        public Vector3 angularVelocity;
        public float time;
        public VelocityMessage(byte t, short i, Vector3 v, Vector3 av, float ti) : base(t, i)
        {
            velocity = v;
            angularVelocity = av;
            time = ti;
        }
    }
    public class StatMessage : AgentMessage
    {
        public short statID;
        public float statValue;

        public StatMessage(byte t, short i, short statid, float value) : base(t, i)
        {
            statID = statid;
            statValue = value;
        }
    }

    public class DestructionStateMessage : AgentMessage
    {
        public short[] vertices;
        public DestructionStateMessage(byte t, short id, short[] verts) : base(t,id)
        {
            vertices = verts;
        }
    }

}
