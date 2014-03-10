using UnityEngine;
using System.Collections;

public class FP_Shooting : MonoBehaviour {

	public GameObject thermal_det;
	public float detImpulse;

	public GameObject debrisPrefab;

	public GameObject weapon;
	private GameObject currWeapon;
	public float cooldown = 0.2f;
	private float cooldownRemaining = 0;

	private float raycastRange = 100f;
	private Camera cam;
	private Vector3 weaponLoc;

	private LineRenderer laser;
	

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		weaponLoc = new Vector3(0.3f, -0.33f, 0.6f);
		Quaternion rotation = new Quaternion(cam.transform.rotation.x,cam.transform.rotation.y-180f,cam.transform.rotation.z,cam.transform.rotation.w); 

		currWeapon = (GameObject)Instantiate(weapon, cam.transform.position + weaponLoc, rotation);
		currWeapon.transform.parent = cam.transform;

		laser = currWeapon.GetComponent<LineRenderer> ();
		Vector3 gunTip = new Vector3 (currWeapon.transform.position.x, currWeapon.transform.position.y + 0.07f, currWeapon.transform.position.z + 0.13f);

		laser.SetPosition (0, gunTip);
		laser.SetPosition (1, new Vector3 (currWeapon.transform.position.x, currWeapon.transform.position.y + 0.07f, currWeapon.transform.position.z + 5));


	}
	
	// Update is called once per frame
	void Update () {
		cooldownRemaining -= Time.deltaTime;

		if(Input.GetButtonDown("Fire1") && cooldownRemaining <= 0){
			Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
			RaycastHit hitInfo;

			if(Physics.Raycast(ray, out hitInfo, raycastRange)){
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
