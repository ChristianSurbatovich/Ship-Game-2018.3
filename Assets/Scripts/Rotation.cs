using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {
    public float rotationX, rotationY, rotationZ;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(transform.worldToLocalMatrix.MultiplyVector(transform.right), rotationX * Time.deltaTime);
        transform.Rotate(transform.worldToLocalMatrix.MultiplyVector(transform.up), rotationY * Time.deltaTime);
        transform.Rotate(transform.worldToLocalMatrix.MultiplyVector(transform.forward), rotationZ * Time.deltaTime);
	}

}
