using UnityEngine;
using System.Collections;

public class FP_Shooting : MonoBehaviour {

	public GameObject thermal_det;
	public float detImpulse;

	// Use this for initialization
	void Start () {
		detImpulse = 50.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")){
			Camera cam = Camera.main;
			GameObject det = (GameObject)Instantiate(thermal_det, cam.transform.position + cam.transform.forward, cam.transform.rotation);
			det.rigidbody.AddForce(cam.transform.forward * detImpulse, ForceMode.Impulse);
		}
	}
}
