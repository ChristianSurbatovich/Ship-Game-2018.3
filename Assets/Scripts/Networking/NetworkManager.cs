using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using ShipGame.UI;
using ShipGame.Destruction;
namespace ShipGame.Network
{
    // network manager keeps all the networking components running 
    public class NetworkManager : MonoBehaviour
    {
        private static NetworkManager instance;


        public static NetworkManager Net{
            get
            {
                return instance;
            }
        }

        public static float netTime;
        public static float netTimeOffset;
        public static float ping;
        public static GameObject player;
        public static short myNetID;
        public string serverName;
        public int serverPort;
        private float syncRequestStart;
        private  NetworkCommunication net;
        private NetToGameConverter converter;
        private NetworkGameState gameState; 
        private CameraControl playerCam;
        private string messageToSend;
        public float clientUpdateFrequency;
        public float serverUpdateFrequency = 0.05f;
        private float timeSinceUpdate;
        private int state, availableFrames;
        private MessageBuffer serverMessage;
        private Queue<byte[]> clientMessages;
        public GameObject[] spawnTypes;
        private GameObject spawnedObject;
        private Vector3 spawnLocation;
        private ObjectPool pooler;
        private PlayerInput playerController;
        private UIController uiController;
        private bool playerSpawned;
        private Explosion damage;
        private Vector3 location;
        public GameSettings settings;
        [SerializeField]
        private ItemDB itemDB;
        [SerializeField]
        private ItemManager inventory;
        public static DebrisManager dbm;
        private Vector3 previousPosition;
        private float previousTime;
        private float lastSyncTime = 0;
        public float syncInterval;
        private bool connected = false;
        public float tolerance;
        private void Awake()
        {
            instance = this;
            net = new NetworkCommunication();
            converter = new NetToGameConverter();
            gameState = new NetworkGameState();
            clientMessages = new Queue<byte[]>();
            ShipGame.Game.SetGameState(gameState);
        }
        private void Start()
        {
            pooler = FindObjectOfType<ObjectPool>();
            uiController = FindObjectOfType<UIController>();
            playerCam = FindObjectOfType<CameraControl>();
            playerController = FindObjectOfType<PlayerInput>();
            dbm = FindObjectOfType<DebrisManager>();
        }
        private void Update()
        {
            if (connected)
            {
                if(Time.time - lastSyncTime > syncInterval)
                {
                //    messageBuffer[0] = 0x2;
               //     messageBuffer[1] = 0x0;
               //     messageBuffer[2] = 0x1A;
                //    messageBuffer[3] = 0x0;
                    syncRequestStart = Time.time;
                    lastSyncTime = Time.time;
                //    net.SendData(messageBuffer);

                }


            }
            availableFrames = net.AvailableDataFrames();
            for (int i = 0; i < availableFrames; i++)
            {
                serverMessage = net.GetDataFrame();
              //  stateChanges = converter.GenerateList(serverMessage);
              //  gameState.QueueChanges(stateChanges);
                if (state == 1)
                {
                    state = 2;
                    //gameState.Initialize(myNetID, serverUpdateFrequency, tolerance);
                    uiController.ShowNameMenu();
                }
            }

            switch (state)
            {
                case 0:
                    state = -1;
                    ConnectToServer(serverName,serverPort);
                    break;
                case 1:
                    break;
                case 2:
                 //   gameState.Interpolate(Time.deltaTime);
                    RegularUpdate();
                    break;
                case 3:
                   // gameState.Interpolate(Time.deltaTime);
                    break;

            }
            if (clientMessages.Count > 0)
            {
                for (int i = 0; i < clientMessages.Count; i++)
                {
                    HandleAction(clientMessages.Dequeue());
                }
            }

        }


        public void AddMessage(byte[] a)
        {
            clientMessages.Enqueue(a);
        }
        public void ConnectToServer(string hostName, int port)
        {
            net.ConnectToServer(hostName,port);
            connected = true;
        }

        private void RegularUpdate()
        {
            if (timeSinceUpdate > 1 / clientUpdateFrequency && playerSpawned)
            {
                Vector3 playerVelocity = (player.transform.position - previousPosition) / (Time.time - previousTime);
                previousTime = Time.time;
                previousPosition = player.transform.position;
                // or just send values directly to stream with no copy
      /*          Array.Copy(BitConverter.GetBytes(MessageLengths.POSITION), 0, messageBuffer, 0, 2);
                messageBuffer[2] = MessageValues.POSITION;
                Array.Copy(BitConverter.GetBytes(myNetID), 0, messageBuffer, 3, 2);
                Array.Copy(BitConverter.GetBytes(player.transform.position.x), 0, messageBuffer, 5, 4);
                Array.Copy(BitConverter.GetBytes(player.transform.position.y), 0, messageBuffer, 9, 4);
                Array.Copy(BitConverter.GetBytes(player.transform.position.z), 0, messageBuffer, 13, 4);
                Array.Copy(BitConverter.GetBytes(player.transform.eulerAngles.x), 0, messageBuffer, 17, 4);
                Array.Copy(BitConverter.GetBytes(player.transform.eulerAngles.y), 0, messageBuffer, 21, 4);
                Array.Copy(BitConverter.GetBytes(player.transform.eulerAngles.z), 0, messageBuffer, 25, 4);
                Array.Copy(BitConverter.GetBytes(playerVelocity.x), 0, messageBuffer, 29, 4);
                Array.Copy(BitConverter.GetBytes(playerVelocity.y), 0, messageBuffer, 33, 4);
                Array.Copy(BitConverter.GetBytes(playerVelocity.z), 0, messageBuffer, 37, 4);
                Array.Copy(BitConverter.GetBytes(Time.time), 0, messageBuffer, 41, 4);
                net.SendData(messageBuffer);*/
                timeSinceUpdate = 0;
            }
            else
            {
                timeSinceUpdate += Time.deltaTime;
            }

        }
        
        public void HandleMessage(GameMessage message)
        {
            switch (message.type)
            {
                case MessageValues.SPAWN:
                    break;
                case MessageValues.RESPAWN:
                    break;
                case MessageValues.DESPAWN:
                    break;
                case MessageValues.SYNC:
                    ping = (Time.time - syncRequestStart) / 2;
                    netTimeOffset = ((SyncMessage)message).serverNetTime;
                    netTime = Time.time + netTimeOffset;
                    print("ping: " + ping);
                    print("netTimeOffset: " + netTimeOffset);
                    print("netTime: " + netTime);
                    break;
            }
        }


        private void HandleAction(byte[] action)
        {
            switch (action[0])
            {
                case MessageValues.LOOT_ITEM:
                    inventory.PickUpItem(BitConverter.ToInt16(action, 1),BitConverter.ToInt16(action,3));
                    break;
                case MessageValues.ADD_LOOT_ITEM:
                    short numItems = BitConverter.ToInt16(action, 1);
                    LootWindow window = inventory.OpenLootWindow(numItems);
                    for(int i = 0; i < numItems; i++)
                    {
                        window.AddItemToWindow(itemDB.GetItemByID(BitConverter.ToInt16(action, 3 + i * 4)),BitConverter.ToInt16(action, 5 + i * 4 ));
                    }
                    break;
                // stat playerID statID value
                case MessageValues.STAT:
                    switch (BitConverter.ToInt16(action, 3))
                    {
                        case Stats.SPEED:
                          //  gameState.StateOf(BitConverter.ToInt16(action, 1)).shipControl.SetStat(Stats.SPEED, BitConverter.ToSingle(action, 5));
                            break;
                        case Stats.CURRENT_SPEED:
                          //  gameState.StateOf(BitConverter.ToInt16(action, 1)).shipControl.SetStat(Stats.CURRENT_SPEED, BitConverter.ToSingle(action, 5));
                            break;
                        case Stats.CURRENT_HEALTH:
                         //   gameState.StateOf(BitConverter.ToInt16(action, 1)).health.SetHealth(BitConverter.ToSingle(action, 5));
                            break;
                        case Stats.MAX_HEALTH:
                          //  gameState.StateOf(BitConverter.ToInt16(action, 1)).health.SetMaxHealth(BitConverter.ToSingle(action, 5));
                            break;
                        default:
                          //  gameState.StateOf(BitConverter.ToInt16(action, 1)).shipControl.SetStat(action);
                            break;
                    }

                    break;
                case MessageValues.FEED:
                  //  uiController.ShowInFeed(gameState.StateOf(BitConverter.ToInt16(action,1)).name.text + " Sank " + gameState.StateOf(BitConverter.ToInt16(action, 3)).name.text);
                    break;
                // Chat name length ms m2 m3..
                case MessageValues.CHAT:
                    string message;
                    short nameLength, chatLength;
                    nameLength = BitConverter.ToInt16(action, 1);
                    chatLength = BitConverter.ToInt16(action, 3);
                    message = Encoding.UTF8.GetString(action, 5, nameLength) + ": " + Encoding.UTF8.GetString(action, 5 + nameLength, chatLength);
                    uiController.AddChatMessage(message);
                    break;
                case MessageValues.RESPAWN:
                    uiController.StartSpectating(BitConverter.ToSingle(action,1));
               //     playerController.RegisterSpectateTarget(gameState.SetAgentIterator(myNetID));
                    break;
                // despawn id 
                case MessageValues.DESPAWN:
               //     gameState.StateOf(BitConverter.ToInt16(action,1)).ship.SetActive(false);
                    break;
                // spawn command "SPAWN X Y Z rX rY rZ ID TYPE bool cur max     L   name
                //                1     4 4 4 4  4  4  2  2     1   4   4       2   x
                //                0     1 5 9 13 17 21 25 27    29  30  34      38  40
                // then the state True/False health max health name
                case MessageValues.SPAWN:
                    short newID = BitConverter.ToInt16(action, 25);
                    spawnLocation.Set(BitConverter.ToSingle(action, 1), BitConverter.ToSingle(action, 5), BitConverter.ToSingle(action, 9));
                    if (newID == myNetID && !playerSpawned)
                    {
                        spawnedObject = Instantiate(spawnTypes[1], spawnLocation, Quaternion.Euler(BitConverter.ToSingle(action,13), BitConverter.ToSingle(action, 17), BitConverter.ToSingle(action, 21)));
                //        gameState.AddAgent(new NetGameAgent(newID, spawnedObject.transform, playerController));
                        settings.SetValue(Settings.playerShip, spawnedObject);
                        playerController.RegisterPlayer(spawnedObject);
                        player = spawnedObject;
                        playerSpawned = true;
                        uiController.StopSpectating();
                    }   
                    else
                    {
                        spawnedObject = Instantiate(spawnTypes[BitConverter.ToInt16(action,27)], spawnLocation, Quaternion.Euler(BitConverter.ToSingle(action, 13), BitConverter.ToSingle(action, 17), BitConverter.ToSingle(action, 21)));
                  //      gameState.AddAgent(new NetGameAgent(newID, spawnedObject.transform, spawnedObject.GetComponent(typeof(INetworkController)) as INetworkController));
                    }
                    print(action);
                    PlayerHealth newObjectHealth = spawnedObject.GetComponentInChildren<PlayerHealth>();
                    Text newObjectName = spawnedObject.GetComponentInChildren<Text>();
                    newObjectHealth.Initialize(BitConverter.ToSingle(action,30), BitConverter.ToSingle(action,34));
                    newObjectName.text = Encoding.UTF8.GetString(action, 40, BitConverter.ToInt16(action, 38));

                    if (BitConverter.ToBoolean(action,29))
                    {
                        byte[] m = { MessageValues.OPEN };
                       // (spawnedObject.GetComponent(typeof(INetworkController)) as INetworkController).Action(m);
                    }
              //      gameState.AddAgentState(newID, new NetAgentState(BitConverter.ToBoolean(action, 29), newObjectName, newObjectHealth, spawnedObject));
                    break;
                // REGISTER ID tickrate 
                // 1        2  2
                // 0        1   
                case MessageValues.REGISTER:
                    myNetID = BitConverter.ToInt16(action, 1);
                    state = 1;
                    serverUpdateFrequency = 1.0f / BitConverter.ToInt16(action, 3);
                    print("Setting Server update frequency to: " + serverUpdateFrequency);
                    break;
                // hit true id weaponID x  y  z
                // 1   1    2  2        4  4  4
                // 0   1    2  4        6  10 14
                case MessageValues.HIT:
                    if (BitConverter.ToBoolean(action,1))
                    {
                       // location = new Vector3(BitConverter.ToSingle(action,6), BitConverter.ToSingle(action, 10), BitConverter.ToSingle(action, 14)) + gameState.StateOf(BitConverter.ToInt16(action,2)).ship.transform.position;
                    }
                    else
                    {
                        location = new Vector3(BitConverter.ToSingle(action, 6), BitConverter.ToSingle(action, 10), BitConverter.ToSingle(action, 14));
                    }

                    damage = pooler.getObject<Explosion>("Explosion", 15f);
                    damage.transform.SetParent(transform);
                    damage.transform.position = location;
                    damage.explode(location);
                    break;
                // HEALTH ID health
                // 1      2  4
                // 0      1  3
                case MessageValues.HEALTH:
             //       gameState.StateOf(BitConverter.ToInt16(action,1)).health.SetHealth(BitConverter.ToSingle(action,3));
                    break;
                // NAME ID L name
                // 1    2  2 x
                // 0    1  3 5
                case MessageValues.NAME:
              //      gameState.StateOf(BitConverter.ToInt16(action,1)).name.text = Encoding.UTF8.GetString(action,5,BitConverter.ToInt16(action,3));
                    break;
                // remove id
                // 1      2
                // 0      1
                case MessageValues.REMOVE:
                    short id = BitConverter.ToInt16(action, 1);
              //      gameState.RemoveAgent(id);
              //      gameState.RemoveAgentState(id);
                    if (id == myNetID)
                    {
                        playerSpawned = false;
                    }
                    break;
                // explode x y z
                // 1       4 4 4
                // 0       1 5 9
                case MessageValues.EXPLODE:
                    location = new Vector3(BitConverter.ToSingle(action,1), BitConverter.ToSingle(action, 5), BitConverter.ToSingle(action, 9));
                    damage = pooler.getObject<Explosion>("BigExplosion", 15f);
                    damage.transform.SetParent(transform);
                    damage.transform.position = location;
                    damage.explode(location);
                    break;
                case MessageValues.SYNC:

                    break;
                default:
                    break;
            }
        }

        private void OnApplicationQuit()
        {
            net.Quit();
        }

        public short NetID()
        {
            return myNetID;
        }
    }

}
