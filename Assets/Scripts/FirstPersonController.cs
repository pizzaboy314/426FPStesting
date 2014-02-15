using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

	public float MovementSpeed = 7.0f;
	public float SprintMultiplier = 1.5f;
	public float MouseSensitivity = 5.0f;
	public float UpDownRange = 60.0f;
	public float JumpSpeed = 5.0f;
	private float verticalRotation;
	private float verticalVelocity;
	private float sprintFactor;

	CharacterController cc;


	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController>();
		verticalRotation = 0f;
		verticalVelocity = 0f;
		sprintFactor = 1.0f;
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Rotation
		float rotLeftRight = Input.GetAxis("Mouse X") * MouseSensitivity;
		transform.Rotate(0,rotLeftRight,0);

		verticalRotation -= Input.GetAxis("Mouse Y") * MouseSensitivity;
		verticalRotation = Mathf.Clamp (verticalRotation, -UpDownRange, UpDownRange);
		
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation,0,0);

		// Sprint?
		if(Input.GetButton("Sprint")){
			sprintFactor = SprintMultiplier;
		} else {
			if(sprintFactor > 1.0f){
				sprintFactor -= 0.01f;
				Debug.Log(sprintFactor);
			}
		}
	
		// Movement
		float forwardSpeed = Input.GetAxis("Vertical") * MovementSpeed * sprintFactor;
		float sideSpeed = Input.GetAxis("Horizontal") * MovementSpeed;

		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		// jumping
		if(Input.GetButtonDown("Jump") && cc.isGrounded){
			verticalVelocity = JumpSpeed;
		}

		Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
		speed = transform.rotation * speed;

		cc.Move(speed * Time.deltaTime);

	}
}
