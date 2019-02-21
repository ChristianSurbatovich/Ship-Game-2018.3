using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShipGame.UI
{
    public class KillFeed : MonoBehaviour, IKillFeed
    {
        // when adding a new item to the feed add it as the child of the last item and then update the last item to point to it, move the root item down, and then start the removal coroutine
        // the removal coroutine
        // Use this for initialization
        private Queue<Text> messages;
        private Text root, last;
        public Text lineMessage;
        public float fadeTime, removeTime;
        void Start()
        {
            messages = new Queue<Text>();
        }

        public void AddToFeed(string feed)
        {
            if (root == null)
            {
                root = Instantiate(lineMessage, transform);
                last = root;
                messages.Enqueue(last);
            }
            else
            {
                Vector2 loc = root.rectTransform.anchoredPosition;
                loc.y -= 20;
                root.rectTransform.anchoredPosition = loc;
                last = Instantiate(lineMessage, last.transform);
                last.rectTransform.anchoredPosition = new Vector2(0, 20);
            }
            last.text = feed;
            StartCoroutine(RemoveFromFeed());
        }

        private IEnumerator RemoveFromFeed()
        {
            yield return new WaitForSeconds(removeTime);
            messages.Dequeue();
            StartCoroutine(FadeOut(root));
            try
            {
                root = messages.Peek();
            }
            catch (System.InvalidOperationException)
            {
                root = null;
            }
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
    }

}
