using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingHealthBar : MonoBehaviour {
    private Camera mainCamera;
	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;

	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(mainCamera.transform);
	}
}
