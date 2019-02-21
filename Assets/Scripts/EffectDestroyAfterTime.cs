using UnityEngine;
using System.Collections;

public class EffectDestroyAfterTime : MonoBehaviour {
    public float timeToLive;
    private float timeAlive = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(timeAlive >= timeToLive)
        {
            Destroy(gameObject);
        }
        else
        {
            timeAlive += Time.deltaTime;
        }
	    
	}
}
