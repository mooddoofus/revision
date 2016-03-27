﻿using UnityEngine;
using System.Collections;

public class cameraSettingToggle : MonoBehaviour {
	//So it remembers where game mode's default camera is

	//Entire GO's start position
	public Vector3 startingSystemPosition;
	private Quaternion startingSystemRotation;

	//CameraPivot's starting position
	public GameObject cameraPivot;
	private Vector3 startingCameraPosition;
	private Quaternion startingCameraRotation;

	//Camera for PerspectiveSwitch
	private PerspectiveSwitcher theCamera;

	//these get toggled on and off with debug mode
	public CameraControlScript cameraController;
	public PlayerControllerTest playerController;
	private Vector3 controllerStartPosition;
	private Quaternion controllerStartRotation;

	//these are used for the model
	public GameObject playerModel;
	private Quaternion playerStartRotation;

	public Ability gameModeChanges;



	// Use this for initialization
	void Start () {
	
		//set starting values
		startingSystemPosition = transform.position;
		startingSystemRotation = transform.rotation;
		startingCameraPosition = cameraPivot.transform.position;
		startingCameraRotation = cameraPivot.transform.rotation;
		playerStartRotation = playerModel.transform.rotation;

		//grab camera
		theCamera = GetComponentInChildren<PerspectiveSwitcher>();

		controllerStartPosition = playerController.gameObject.transform.position;
		controllerStartRotation = playerController.gameObject.transform.rotation;
	}
	
	public void toggleControls(bool mode){
		cameraController.enabled = mode;
		playerController.enabled = mode;
	}


	IEnumerator ResetPositionAsync (float transitionTime) {
		float startTime = Time.time;
		playerController.GetComponent<Rigidbody>().velocity = Vector3.zero;
		playerController.GetComponent<Rigidbody>().useGravity = false;
		playerController.GetComponent<Collider>().enabled = false;

		Vector3 ogPosition = transform.position;
		Vector3 ogCameraPosition = cameraPivot.transform.position;
		Vector3 ogPlayerPosition = playerController.gameObject.transform.position;
		Quaternion ogRotation = transform.rotation;
		Quaternion ogCameraRotation = cameraPivot.transform.rotation;
		Quaternion ogPlayerRotation = playerController.gameObject.transform.rotation;

		while (Time.time - startTime < transitionTime) {
			float d = (Time.time - startTime) / transitionTime;
			transform.position = Vector3.Slerp(ogPosition, startingSystemPosition, d);
			transform.rotation = Quaternion.Slerp(ogRotation, startingSystemRotation, d);
			cameraPivot.transform.position = Vector3.Slerp(ogCameraPosition, startingCameraPosition, d);
			cameraPivot.transform.rotation = Quaternion.Slerp(ogCameraRotation, startingCameraRotation, d);

			playerController.gameObject.transform.position = Vector3.Slerp(ogPlayerPosition, controllerStartPosition, d);
			playerController.gameObject.transform.rotation = Quaternion.Slerp(ogPlayerRotation, controllerStartRotation, d);
			yield return 1;
		}
		playerController.GetComponent<Rigidbody>().useGravity = true;
		playerController.GetComponent<Collider>().enabled = true;
	}

	public void resetPosition(){
		StartCoroutine(ResetPositionAsync(3f));
	}

	public void switchMode(){
		StartCoroutine(switching());
	}

	public IEnumerator switching(){
		theCamera.switchPerspective();
		toggleControls(DebugSwitch.debugMode);
		//yield return new WaitForSeconds(4f);
		turnPlayerModel();
		if(DebugSwitch.debugMode == true){
		}
		else{
			resetPosition();
			if(gameModeChanges != null){
				gameModeChanges.Activate();
				gameModeChanges = null;
			}
		}

		yield return 1;
	}

	public void turnPlayerModel(){
		if(DebugSwitch.debugMode == true){
			Quaternion tempVect = playerModel.transform.localRotation;
			tempVect.y *= -1;
			playerModel.transform.rotation = tempVect;
		}
		else{playerModel.transform.rotation = playerStartRotation;}
	}
}
