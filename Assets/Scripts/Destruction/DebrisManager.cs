using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame.Destruction
{
    public class DebrisManager : MonoBehaviour
    {
        public static DebrisManager DBM { get; private set; }
        public float keepAliveTime;
        public float fadeTime;
        public int maxDebris;
        public float fadeSpeed;
        private DebrisHolder[] holders;
        public DebrisHolder h;

        private void Awake()
        {
            DBM = this;
        }
        // Use this for initialization
        void Start()
        {
            holders = new DebrisHolder[maxDebris];
            for (int i = 0; i < holders.Length; i++)
            {
                holders[i] = Instantiate(h, transform);
                holders[i].index = i;
                holders[i].gameObject.SetActive(false);
            }
        }



        public void Register(TreeNode T)
        {
            float lowestRemaining = keepAliveTime + 1.0f;
            int index = 0;
            for (int i = 0; i < holders.Length; i++)
            {
                if (!holders[i].active)
                {
                    holders[i].gameObject.SetActive(true);
                    holders[i].setValues(fadeSpeed, fadeTime, keepAliveTime, T);
                    return;
                }
                if (holders[i].keepAliveTime < lowestRemaining)
                {
                    lowestRemaining = holders[i].keepAliveTime;
                    index = i;
                }
            }
            holders[index].ReturnObject();
            holders[index].gameObject.SetActive(true);
            holders[index].setValues(fadeSpeed, fadeTime, keepAliveTime, T);
        }
    }
}

