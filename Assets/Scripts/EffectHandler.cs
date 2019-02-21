using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectHandler : MonoBehaviour {
    List<DefensiveEffect> defensiveEffects;
    List<OffensiveEffect> offensiveEffects;
    List<Effect> otherEffects;
    private float strength;
	// Use this for initialization
	void Start () {
        defensiveEffects = new List<DefensiveEffect>();
        offensiveEffects = new List<OffensiveEffect>();
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public float defense()
    {
        strength = 1;
        foreach ( DefensiveEffect e in defensiveEffects)
        { 
            strength = strength * e.getStrength();
        }
        return strength;
    }

    public void Register(DefensiveEffect e)
    {
        if (defensiveEffects.Contains(e))
        {
            defensiveEffects.Remove(e);
            print("removing: " + e.getName());
        }
        defensiveEffects.Add(e);
        StartCoroutine(e.effectLife());
    }

    public void Unregister(DefensiveEffect e)
    {
        defensiveEffects.Remove(e);
    }
    
}
