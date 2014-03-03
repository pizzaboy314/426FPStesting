using UnityEngine;
using System.Collections;

public class FP_Shooting : MonoBehaviour {

	public GameObject thermal_det;
	public float detImpulse;

	public float range = 100f;

	public float cooldown = 0.2f;
	private float cooldownRemaining = 0;

	public GameObject debrisPrefab;

	// Use this for initialization
	void Start () {
		detImpulse = 50.0f;
	}
	
	// Update is called once per frame
	void Update () {
		cooldownRemaining -= Time.deltaTime;

		if(Input.GetButtonDown("Fire1") && cooldownRemaining <= 0){
			Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
			RaycastHit hitInfo;

			if(Physics.Raycast(ray, out hitInfo, range)){
				Vector3 hitPoint = hitInfo.point;
				if(debrisPrefab != null){
					Instantiate(debrisPrefab, hitPoint, Quaternion.identity);
				}

				GameObject obj = hitInfo.collider.gameObject;
				Debug.Log(obj.tag);
			}
		}

		if (Input.GetButtonDown("Fire2")){
			Camera cam = Camera.main;
			GameObject det = (GameObject)Instantiate(thermal_det, cam.transform.position + cam.transform.forward, cam.transform.rotation);
			det.rigidbody.AddForce(cam.transform.forward * detImpulse, ForceMode.Impulse);
		}
	}
}
