using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ShipGame.Network;
namespace ShipGame.UI
{
    public class UIController : MonoBehaviour, IUIController
    {
        public bool pauseMenuActive, settingsMenuActive, chatOpen;
        private SortedList<int, IUIObject> UIStack;
        private IChatBox chat;
        private ISpectateUI spectateUI;
        private IKillFeed killFeed;
        private INameChangePanel namePanel;
        public ItemManager itemManager;
        private PlayerInput playerController;
        [SerializeField]
        private List<AbilityButton> abilities;
        // Use this for initialization

        void Start()
        {
            UIStack = new SortedList<int, IUIObject>();
            playerController = FindObjectOfType<PlayerInput>();
            chat = GetComponent(typeof(IChatBox)) as IChatBox;
            namePanel = GetComponent(typeof(INameChangePanel)) as INameChangePanel;
            killFeed = GetComponent(typeof(IKillFeed)) as IKillFeed;
            spectateUI = GetComponent(typeof(ISpectateUI)) as ISpectateUI;
            chatOpen = false;
        }

        public void StartCooldown(short id, float cooldown, PlayerInput.AbilityCallBack callBack)
        {
            abilities[id].StartCooldown(id, cooldown, callBack);
        }

        public void StartSpectating(float t)
        {
            spectateUI.Open();
            spectateUI.StartSpawnCounter(t);
        }
        public void StopSpectating()
        {
            spectateUI.Close();
        }

        public void SpectateNext()
        {
          //  playerController.RegisterSpectateTarget(NetworkManager.Net.NextAgent());
        }

        public void SpectatePrevious()
        {
          //  playerController.RegisterSpectateTarget(NetworkManager.Net.NextAgent());
        }

        public void UpdateSpectateUI(GameObject target)
        {
            spectateUI.Spectate(target.GetComponentInChildren<Text>().text);
        }


        public void ShowInFeed(string s)
        {
            killFeed.AddToFeed(s);
        }


        public void ShowNameMenu()
        {
            namePanel.Open();
        }

        public void AddChatMessage(string feed)
        {
            chat.AddChatMessage(feed);
        }
    }

}
