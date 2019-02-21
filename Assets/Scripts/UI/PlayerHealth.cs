using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ShipGame.Network;

namespace ShipGame
{
    public class PlayerHealth : MonoBehaviour
    {
        public float health, maxHealth;
        private Slider healthBar;
        public float fadeStart, fadeEnd;
        private float distance, fadeAmount, fadeLength;
        private Image[] colors;
        public short playerID;
        // Use this for initialization
        void Awake()
        {
            healthBar = GetComponentInChildren<Slider>();
            fadeLength = fadeEnd - fadeStart;
            colors = GetComponentsInChildren<Image>();
        }
        void Update()
        {

            distance = Vector3.Distance(transform.position, NetworkManager.player.transform.position);
            fadeAmount = distance - fadeStart;
            fadeAmount = 1 - (fadeAmount / fadeLength);
            fadeAmount = fadeAmount < 0 ? 0 : (fadeAmount > 1 ? 1 : fadeAmount);

            if(fadeAmount <= 0)
            {
                SetHealth(ShipGame.Game.GetStatValue(playerID, Stats.CURRENT_HEALTH));
                SetMaxHealth(ShipGame.Game.GetStatValue(playerID, Stats.MAX_HEALTH));
            }

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i].color = new Color(colors[i].color.r, colors[i].color.g, colors[i].color.b, fadeAmount);
            }
            transform.LookAt(Camera.main.transform);
        }
        public void Initialize(float current, float max)
        {
            health = current;
            maxHealth = max;
            SetHealth(health);
        }

        public void SetHealth(float f)
        {
            health = f;
            healthBar.value = (health / maxHealth) * 100;
        }

        public void SetMaxHealth(float f)
        {
            maxHealth = f;
            healthBar.value = (health / maxHealth) * 100;
        }
    }

}
