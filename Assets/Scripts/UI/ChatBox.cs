using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ShipGame.Network;
namespace ShipGame.UI
{
    public class ChatBox : MonoBehaviour, IChatBox
    {

        // when adding a new item to the feed add it as the child of the last item and then update the last item to point to it, move the root item down, and then start the removal coroutine
        // the removal coroutine
        // Use this for initialization
        private Queue<Text> messages;
        private Text root, temp;
        public Text lineMessage;
        public float fadeTime, removeTime;
        public float maxChatMessagesSaved;
        public InputField inputBox;
        private IInputManager input;
        private IUIController ui;
    //    private byte[] chatMessage = new byte[MessageLengths.MAX_SEND_LENGTH];
        private bool focus, chatOpen;

        void Awake()
        {
            messages = new Queue<Text>();
            input = GameObject.FindGameObjectWithTag("Input").GetComponent(typeof(IInputManager)) as IInputManager;
        }

        void Start()
        {
            input.Register(this, InputFocus.SHARED);
        }

        private void Update()
        {
            if (focus)
            {
                if (Input.GetButtonDown("Cancel") && chatOpen)
                {
                    Cancel();
                }
                if (Input.GetButtonDown("Submit"))
                {
                    if (chatOpen)
                    {
                        Close();
                    }
                    else
                    {
                        Open();
                    }

                }
            }
        }

        public void AddChatMessage(string feed)
        {
            if (root == null)
            {
                root = Instantiate(lineMessage, transform);
                messages.Enqueue(root);
            }
            else
            {
                temp = Instantiate(lineMessage, transform);
                root.transform.SetParent(temp.transform);
                root.rectTransform.anchoredPosition = new Vector2(0, 25);
                root = temp;
                messages.Enqueue(root);
            }
            root.text = feed;
            if (messages.Count > maxChatMessagesSaved)
            {
                Destroy(messages.Dequeue());
            }
        }

        public void Open()
        {
            inputBox.gameObject.SetActive(true);
            chatOpen = true;
            input.Register(this, InputFocus.EXCLUSIVE);
        }

        public void Close()
        {
            chatOpen = false;
            string message = inputBox.text;

            inputBox.text = "";
            if(message != "")
            {
                byte[] chatBytes = System.Text.Encoding.UTF8.GetBytes(message);
             //   Array.Copy(BitConverter.GetBytes((short)(3 + chatBytes.Length)),0,chatMessage,0,2);
               // chatMessage[2] = MessageValues.CHAT;
             //   Array.Copy(BitConverter.GetBytes((short)chatBytes.Length), 0, chatMessage, 3, 2);
              //  Array.Copy(chatBytes, 0, chatMessage, 5, chatBytes.Length);
              //  NetworkManager.Net.SendNetworkMessage(chatMessage, 5 + chatBytes.Length);
            }
            inputBox.gameObject.SetActive(false);
            input.Unregister(this, InputFocus.EXCLUSIVE);
            input.Register(this, InputFocus.SHARED);
        }

        public void Cancel()
        {
            inputBox.text = "";
            Close();
        }

        private IEnumerator FadeOut(Text obj)
        {
            float fadeOutTime = fadeTime;
            while (fadeOutTime > 0)
            {
                obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, fadeOutTime / fadeTime);
                fadeOutTime -= Time.deltaTime;
                yield return 0;
            }
            Destroy(obj.gameObject);
        }

        public void SetFocus(bool newFocus)
        {
            focus = newFocus;
        }
    }

}
