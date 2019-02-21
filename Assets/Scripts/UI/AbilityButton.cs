using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShipGame
{
    public class AbilityButton : MonoBehaviour
    {
        [SerializeField]
        private Text counter;
        [SerializeField]
        private Image icon;
        [SerializeField]
        private Color offCooldownColor, onCooldownColor;

        private void Start()
        {
            counter.gameObject.SetActive(false);
            icon.color = offCooldownColor;
        }

        public void StartCooldown(short id, float cooldown, PlayerInput.AbilityCallBack callBack)
        {
            counter.gameObject.SetActive(true);
            icon.color = onCooldownColor;
            StartCoroutine(CooldownRoutine(id, cooldown, callBack));
        }



        IEnumerator CooldownRoutine(short id, float cooldown, PlayerInput.AbilityCallBack callBack)
        {
            while (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                counter.text = Mathf.Ceil(cooldown).ToString();
                yield return 0;
            }
            counter.gameObject.SetActive(false);
            icon.color = offCooldownColor;
            callBack(id);
        }
    }
}

