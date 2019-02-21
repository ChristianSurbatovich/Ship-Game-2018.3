using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterSheet : MonoBehaviour {

    Text healthStat;
    Text damageStat;
    Text speedStat;


    public void SetHealthStat(float health)
    {
        healthStat.text = health.ToString();
    }
}
