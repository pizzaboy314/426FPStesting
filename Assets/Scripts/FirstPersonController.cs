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
	private bool jumping;

	CharacterController cc;


	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController>();
		verticalRotation = 0f;
		verticalVelocity = 0f;
		sprintFactor = 1.0f;
		jumping = false;
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Rotation or Left/Right Viewpoint -- rotates character about the Y axis
		float rotLeftRight = Input.GetAxis("Mouse X") * MouseSensitivity;
		transform.Rotate(0,rotLeftRight,0);

		// Up/Down Viewpoint -- rotates camera about the X axis
		verticalRotation -= Input.GetAxis("Mouse Y") * MouseSensitivity;
		verticalRotation = Mathf.Clamp (verticalRotation, -UpDownRange, UpDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation,0,0);

		// Sprint?
		if(Input.GetButton("Sprint")){
			sprintFactor = SprintMultiplier;
		} else {
			// for slowly bringing the speed back to normal instead of abruptly
			if(sprintFactor > 1.0f){
				sprintFactor -= 0.01f;
			}
		}
	
		// Movement
		float forwardSpeed = Input.GetAxis("Vertical") * MovementSpeed * sprintFactor;
		float sideSpeed = Input.GetAxis("Horizontal") * MovementSpeed;

		// applies gravity to player
		if(!cc.isGrounded){
			verticalVelocity += Physics.gravity.y * Time.deltaTime;
		}

		// jumping
		if(Input.GetButtonDown("Jump") && cc.isGrounded){
			verticalVelocity = JumpSpeed;
			jumping = true;
			move(sideSpeed, verticalVelocity, forwardSpeed);
		} else if (cc.isGrounded && jumping == false){
			verticalVelocity = 0f;
			jumping = false;
			move(sideSpeed, verticalVelocity, forwardSpeed);
		} else {
			move(sideSpeed, verticalVelocity, forwardSpeed);
		}


	}

	// the actual player moving
	public void move(float sideSpeed, float verticalVelocity, float forwardSpeed){
		Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
		speed = transform.rotation * speed;
		cc.Move(speed * Time.deltaTime);
	}
}
