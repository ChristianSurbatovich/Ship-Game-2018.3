using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame.Network
{
    public static class MessageValues
    {
        public const byte HIT = 0x01;
        public const byte HEALTH = 0x02;
        public const byte SINK = 0x03;
        public const byte FIRE = 0x04;
        public const byte OPEN = 0x05;
        public const byte NAME = 0x06;  
        public const byte SPAWN = 0x07;
        public const byte REGISTER = 0x08;
        public const byte RESPAWN = 0x09;
        public const byte REMOVE = 0x0A;
        public const byte DESPAWN = 0x0B;
        public const byte CHAT = 0x0C;
        public const byte FEED = 0x0D;
        public const byte EXPLODE = 0x0E;
        public const byte POSITION = 0x0F;
        public const byte STAT = 0x10;
        public const byte ABILITY = 0x11;
        public const byte ADD_ACTION = 0x12;    
        public const byte EQUIP_ITEM = 0x13; 
        public const byte ITEM = 0x14;
        public const byte LOOT_AREA = 0x15;
        public const byte LOOT_ITEM = 0x16;
        public const byte ADD_LOOT_ITEM = 0x17;
        public const byte PICKUP_ITEM = 0x18;
        public const byte UNEQUIP_ITEM = 0x19;
        public const byte SYNC = 0x1A;
        public const byte DESTRUCTION_STATE_RESET = 0x1B;
        public const byte DESTRUCTION_STATE = 0x1C;
        public const byte ADD_ABILITY = 0x1D;
        public const byte EQUIP_ABILITY = 0x1E;
        public const byte REMOVE_ABILITY = 0x1F;
        public const byte UNEQUIP_ABILITY = 0x20;
        public const byte POSITION_FULL = 0x21;
        public const byte VELOCITY = 0x22;
        public const byte VELOCITY_FULL = 0x23;
        public const byte AGENT_SYNC = 0x24;
        public const byte ADD_AGENT = 0x25;
        public const byte TEST = 0xFF;
    }

}

