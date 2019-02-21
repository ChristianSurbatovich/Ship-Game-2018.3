using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame.Destruction
{
    public class DebrisHolder : MonoBehaviour
    {
        private TreeNode node;
        private bool returning = false;
        public bool active;
        public float keepAliveTime;
        private float fadeTime;
        private float fadeSpeed;
        private DebrisManager dbm;
        private Vector3 shiftAmount;
        public int index;
        // Use this for initialization
        private void Awake()
        {
            shiftAmount = new Vector3(0.0f, 0.0f, 0.0f);
            active = false;
        }

        public void setValues(float fade, float speed, float aliveTime, TreeNode n)
        {
            fadeTime = fade;
            fadeSpeed = speed;
            keepAliveTime = aliveTime;
            node = n;
            node.transform.SetParent(transform);
            n.managedByDebrisHolder = true;
            n.holder = this;
            active = true;
        }



        // Update is called once per frame
        void Update()
        {
            if (keepAliveTime > 0)
            {
                keepAliveTime -= Time.deltaTime;
            }
            else if (fadeTime > 0)
            {
                Destroy(node.h.r);
                shiftAmount = Time.deltaTime * fadeSpeed * Vector3.down;
                node.transform.Translate(node.transform.InverseTransformVector(shiftAmount));
                fadeTime -= Time.deltaTime;
            }
            else
            {
                ReturnObject();
            }
        }



        // disables and returns the object
        public void ReturnObject()
        {
            node.gameObject.transform.SetParent(node.parentObject);
            node.active = false;
            //node.manager.removeEdges(node.h);
            node.holder = null;
            if (node.floatingData && (node.floatingData.sinking || node.floatingData.floating))
            {
                node.floatingData.StopFloating();
            }
            node.managedByDebrisHolder = false;
            node.gameObject.SetActive(false);
            gameObject.SetActive(false);
            active = false;
        }

    }

}
