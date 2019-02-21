using System;
using UnityEngine;
using System.Collections.Generic;

namespace ShipGame.Network
{
    // interperets data from the server and converts it into game events
    public class NetToGameConverter
    {
        /* Data format for server -> client communication
         * 4 bytes for message length
         * the number of positions n in the state
         * the number of game actions o
         * the number of client actions p
         * followed by n positions in the form ID x y z rotationx rotationy rotationz
         * followed by o game actions in the form 
         * and then p client actions with the form
         */

        public static List<AgentMessage> ProcessServerFrame(MessageBuffer buffer)
        {
            List<AgentMessage> messageList = new List<AgentMessage>();
            while (buffer.HasUnreadData())
            {
                switch (buffer.ReadByte())
                {
                    case MessageValues.POSITION:
                        messageList.Add(new PositionMessage(
                            MessageValues.POSITION,
                            buffer.ReadInt16(), buffer.ReadVector3(),
                            buffer.ReadVector3(),
                            buffer.ReadFloat32()
                        ));
                        break;
                    case MessageValues.POSITION_FULL:
                        messageList.Add(new PositionFullMessage(
                            MessageValues.POSITION_FULL,
                            buffer.ReadInt16(),
                            buffer.ReadVector3(),
                            buffer.ReadVector3(),
                            buffer.ReadVector3(),
                            buffer.ReadVector3(),
                            buffer.ReadFloat32()
                        ));
                        break;
                    case MessageValues.VELOCITY:
                        messageList.Add(new VelocityMessage(
                            MessageValues.VELOCITY,
                            buffer.ReadInt16(),
                            buffer.ReadVector3(),
                            buffer.ReadVector3(),
                            buffer.ReadFloat32()
                        ));
                        break;
                    case MessageValues.SYNC:
                        NetworkManager.Net.HandleMessage(new SyncMessage(
                            MessageValues.SYNC,
                            buffer.ReadFloat32()
                            ));
                        break;
                    case MessageValues.DESTRUCTION_STATE:
                        short id = buffer.ReadInt16();
                        short[] vertices = new short[buffer.ReadInt16()];
                        for(int i = 0; i < vertices.Length; i++)
                        {
                            vertices[i] = buffer.ReadInt16();
                        }
                        messageList.Add(new DestructionStateMessage(
                            MessageValues.DESTRUCTION_STATE,
                            id,
                            vertices
                            ));
                        break;
                    case MessageValues.DESTRUCTION_STATE_RESET:
                        messageList.Add(new AgentMessage(MessageValues.DESTRUCTION_STATE_RESET, buffer.ReadInt16()));
                        break;
                    // loot_item areaID itemID inventory slotID 
           /*         case MessageValues.LOOT_ITEM:
                        bytesToCopy = 5;
                        destination = 1;
                        break;
                    // add numItems item1ID, item2ID ...
                    case MessageValues.ADD_LOOT_ITEM:
                        bytesToCopy = BitConverter.ToInt16(data, offset + 1) * 4 + 3;
                        destination = 1;
                        break;
                    // ABILITY playerID abilityID
                    case MessageValues.ABILITY:
                        bytesToCopy = 5;
                        destination = 0;
                        break;
                    // STAT playerID statID value
                    // 1    2        2      4
                    case MessageValues.STAT:
                        bytesToCopy = 9;
                        destination = 1;
                        break;
                    // fire
                    // s id weaponID numshots x y z ...
                    // 1 2  2        4 4 4....
                    case MessageValues.FIRE:
                        bytesToCopy = BitConverter.ToInt16(data, offset + 5) * 12 + 7;
                        destination = 0;
                        break;
                    // open
                    // o id
                    // 1 2
                    case MessageValues.OPEN:
                        messageList.Add(new AgentMessage(MessageValues.OPEN, buffer.ReadInt16()));
                        break;
                    // sink
                    // i id x y z x y z
                    // 1 2  4 4 4 4 4 4
                    case MessageValues.SINK:
                        bytesToCopy = 27;
                        destination = 0;
                        break;
                    /******************************************/
                    /******************************************/
                    /******************************************/
                    // HEALTH ID Newhealth
                    // h id newhealth
                    // 1 2  4
                 /*   case MessageValues.HEALTH:
                        bytesToCopy = 7;
                        destination = 1;
                        break;
                    // NAME ID Name
                    // n id length name
                    // 1 2  2      x
                    case MessageValues.NAME:
                        bytesToCopy = 5 + BitConverter.ToInt16(data, offset + 3);
                        destination = 1;
                        break;
                    // spawn command "SPAWN X Y Z rX rY rZ ID TYPE 
                    // then the state True/False health name
                    // w x y z rx ry rz id type bool healmax healthcurrent L name
                    // 1 4 4 4 4  4  4  2  2    1    4       4             2 x 
                    case MessageValues.SPAWN:
                        bytesToCopy = 40 + BitConverter.ToInt16(data, offset + 38);
                        destination = 1;
                        break;
                    // REGISTER id tickrate
                    // r id tickrate
                    // 1 2  2
                    case MessageValues.REGISTER:
                        bytesToCopy = 5;
                        destination = 1;
                        break;
                    // HIT TRUE ID X Y Z
                    // 1   1    2  4 4 4
                    case MessageValues.HIT:
                        bytesToCopy = 18;
                        destination = 1;
                        break;
                    // remove
                    // m id
                    // 1 2
                    case MessageValues.REMOVE:
                        bytesToCopy = 3;
                        destination = 1;
                        break;
                    // despawn
                    // d id
                    // 1 2
                    case MessageValues.DESPAWN:
                        bytesToCopy = 3;
                        destination = 1;
                        break;
                    // respawn timer
                    // p time
                    // 1 4
                    case MessageValues.RESPAWN:
                        bytesToCopy = 5;
                        destination = 1;
                        break;
                    // e x y z
                    // 1 4 4 4
                    case MessageValues.EXPLODE:
                        bytesToCopy = 13;
                        destination = 1;
                        break;
                    // Chat name length ms m2 m3...
                    // c L1 L2 name message
                    // 1 2  2  x1   x2
                    case MessageValues.CHAT:
                        bytesToCopy = 5 + BitConverter.ToInt16(data, offset + 1) + BitConverter.ToInt16(data, offset + 3);
                        destination = 1;
                        break;
                    // feed id1 id2
                    // 1 2 2
                    case MessageValues.FEED:
                        bytesToCopy = 5;
                        destination = 1;
                        break;
                    // add shipID, ability id localID
                    // 1    2       2           2
                    case MessageValues.ADD_ABILITY:
                        bytesToCopy = 7;
                        destination = 0;
                        break;
                    // equip shipid localID
                    // 1     2         2
                    case MessageValues.EQUIP_ABILITY:
                        bytesToCopy = 5;
                        destination = 0;
                        break;
                    case MessageValues.UNEQUIP_ABILITY:
                        bytesToCopy = 5;
                        destination = 0;
                        break;
                    case MessageValues.REMOVE_ABILITY:
                        bytesToCopy = 7;
                        destination = 0;
                        break;
                    // equip item slot 1, item slot 2
                    // 1     2            2
                    case MessageValues.EQUIP_ITEM:
                        bytesToCopy = 5;
                        destination = 1;
                        break;
                    case MessageValues.UNEQUIP_ITEM:
                        bytesToCopy = 5;
                        destination = 1;
                        break;
                    default:
                        Debug.Log("Can't find a match for: " + data[offset]);
                        Debug.Log(data);
                        offset++;
                        valuesToCopy--;
                        destination = -1;
                        break;*/
                }
            }
            return messageList;
        }
    }

}
