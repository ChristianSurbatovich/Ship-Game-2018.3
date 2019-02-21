using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using ShipGame.Network;
using ShipGame.UI;
using ShipGame.Destruction;
namespace ShipGame
{
    public class PlayerInput : MonoBehaviour, INetworkController, IKeyHandler
    {
        public static PlayerInput Player { get; private set; }
        public delegate void AbilityCallBack(short index);
        public IShipControl playerShip;
        public GameObject playerShipObject;
        public MouseControl mc;
        public static Vector3 aimPoint;
        private float moveHorizontal, moveForward;
        private short id;
        private NameDisplay shipName;
        public UIController uiController;
        private CameraControl playerCam;
        private Vector3[] targets;
        private ControlMode mode;
        private bool focus;
        private IInputManager inputManager;
        enum ControlMode { ShipControl, Spectate };
        private Dictionary<short, Ability> addedAbilities;
        private Dictionary<string, Ability> equippedAbilities;
        private Dictionary<int, short> actionMapping;
        [SerializeField]
        private ItemManager inventory;
        [SerializeField]
        private GameObject characterSheet;
        private DestroyableObject playerDestruction;
        // Use this for initialization

        private void Awake()
        {
            Player = this;
        }
        void Start()
        {

            playerCam = FindObjectOfType<CameraControl>();
            mode = ControlMode.Spectate;  
            inputManager = GameObject.FindGameObjectWithTag("Input").GetComponent(typeof(IInputManager)) as IInputManager;
            inputManager.Register(this, InputFocus.SHARED);
            focus = true;
            equippedAbilities = new Dictionary<string, Ability>();
            actionMapping = new Dictionary<int, short>();
            addedAbilities = new Dictionary<short, Ability>();
            actionMapping[0] = 0;
            actionMapping[1] = 0;
            actionMapping[2] = 1;
            actionMapping[3] = 2;
        }


        // Update is called once per frame
        void Update()
        {

            moveHorizontal = Input.GetAxis("Horizontal");
            moveForward = Input.GetAxis("Vertical");
            aimPoint = mc.getAimPoint().point;
            switch (mode)
            {
                case ControlMode.ShipControl:
                    {
                        if(focus == true)
                        {
                            playerShip.SetMovement(moveHorizontal, moveForward);
                        }

                    }

                    foreach(KeyValuePair<string,Ability> ability in equippedAbilities)
                    {
                        if (Input.GetButtonDown(ability.Key))
                        {
                            ability.Value.OnButtonDown();
                        }else if (Input.GetButtonUp(ability.Key))
                        {
                            ability.Value.OnButtonUp();
                        }
                    }

                    if (Input.GetButtonDown("Action1") && focus)
                    {
                        playerShip.OpenGunports();
                    }
                    if (Input.GetButtonDown("Loot") && inventory.activeLootArea != null)
                    {
                        inventory.activeLootArea.TryLoot();
                    }
                    if (Input.GetButtonDown("Character"))
                    {
                        characterSheet.SetActive(!characterSheet.activeInHierarchy);
                    }
                    else if (playerShip != null)
                    {
                        playerShip.AimAtPoint(aimPoint);
                    }

                    if (Input.GetButtonDown("FullReset"))
                    {

                    }
                    break;
                case ControlMode.Spectate:
                    break;
                default:
                    break;
            }


        }

        public void Action(byte[] action)
        {
            print(action[0]);
            switch (action[0])
            {
                case MessageValues.SINK:
                    playerShip.Sink(new Vector3(BitConverter.ToSingle(action, 3), BitConverter.ToSingle(action, 7), BitConverter.ToSingle(action, 11)),
                        BitConverter.ToSingle(action, 15), BitConverter.ToSingle(action, 19), BitConverter.ToSingle(action, 23));
                    break;
                case MessageValues.DESTRUCTION_STATE_RESET:
                    playerDestruction.FullDestructionReset();
                    break;
                default:
                    break;
            }
        }
        public void SetID(short i)
        {
            id = i;
        }


        public short GetID()
        {
            return id;
        }

        public void SetName(string n)
        {
            throw new System.NotImplementedException();
        }
        public void RegisterPlayer(GameObject player)
        {
            playerShipObject = player;
            playerShip = playerShipObject.GetComponent(typeof(IShipControl)) as IShipControl;
            playerDestruction = playerShipObject.GetComponentInChildren<DestroyableObject>();
            playerCam.ChangeTarget(player);
            mode = ControlMode.ShipControl;
        }

        public void RegisterSpectateTarget(GameObject spectateTarget)
        {
            playerCam.ChangeTarget(spectateTarget);
            uiController.UpdateSpectateUI(spectateTarget);
            mode = ControlMode.Spectate;
        }


        public void SetFocus(bool newFocus)
        {
            focus = newFocus;
        }

        public void EquipAbility(string key, Ability ability)
        {
            equippedAbilities[key] = ability;
        }

        public void UnequipAbility(string key)
        {
            equippedAbilities.Remove(key);
        }


        public void AddAbility(short id)
        {
        }

        public void RemoveAbility(short id)
        {
        }

        public void Action(GameMessage message)
        {
            throw new NotImplementedException();
        }
    }

}
