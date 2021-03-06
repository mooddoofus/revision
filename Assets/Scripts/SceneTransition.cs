﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

// do fancy scene transitions
// NOTE: this must be placed on the FinalSteve gameobject!

public class SceneTransition : MonoBehaviour {	

	// the list of renderers we have to enable
	List<Renderer> renderers;

	Color transitionBackgroundColor = new Color(0.169f, 0.169f, 0.169f);
	Color defaultBackgroundColor;
	Vector3 defaultStevePosition;
	Vector3 defaultSteveScale;
	Camera mainCamera;
	float transitionTime = 2f;

	enum State { Start, TransitionIn };
	State currentState;

    public bool _isSteveCenter = true;

	// Use this for initialization
	void Awake () {
		if (gameProgress.secondTime) { // do nothing 2nd time around
            _isSteveCenter = false;
			this.enabled = false;
			return;
		}
		defaultStevePosition = transform.position;
		defaultSteveScale = transform.localScale;
		currentState = State.Start;
		GetComponent<Rigidbody>().isKinematic = true;

		mainCamera = Object.FindObjectOfType<Camera>();
		Vector3 cameraP = mainCamera.transform.position;
		
		if (SceneManager.GetActiveScene().name.Equals("4. Room")) {
			transform.position = cameraP + new Vector3(0f, -16.09f, 2.53f - 6.33f);
		} else {
			transform.position = cameraP + new Vector3(0f, 2.53f - 6.33f, 16.09f);
		}
		transform.localScale = new Vector3(160f, 180f, 160f);


		defaultBackgroundColor = mainCamera.backgroundColor;
		mainCamera.backgroundColor = transitionBackgroundColor;

		// get all the renderers in scene
		Renderer[] r = Object.FindObjectsOfType<Renderer>();
		renderers = new List<Renderer>(r);

		Renderer[] steveRenderer = GetComponentsInChildren<Renderer>();
		// do not track renderers that belong to steve
		for (int i = 0; i < steveRenderer.Length; i++) {
			renderers.Remove(steveRenderer[i]);
		}

		// turn off all other renderers in scene
		for (int i = 0; i < renderers.Count; i++) {
			if (!renderers[i].gameObject.tag.Equals("SpeechBubble")) {
				renderers[i].enabled = false;
			}
		}
	}


	IEnumerator MoveSteveToDefault () {
		while ((transform.position - defaultStevePosition).sqrMagnitude> 0.001f) {
			transform.position = Vector3.Lerp(transform.position, defaultStevePosition, 0.1f);
			transform.localScale = Vector3.Lerp(transform.localScale, defaultSteveScale, 0.5f);
			yield return new WaitForSeconds(0.01f);
		}
	}

	IEnumerator MoveSteveToCenter () {
		Vector3 cameraP = Object.FindObjectOfType<Camera>().transform.position;
		Vector3 p;
		if (SceneManager.GetActiveScene().name.Equals("4. Room")) {
			p = cameraP + new Vector3(0f, -16.09f, 2.53f - 6.33f);
		} else {
			p = cameraP + new Vector3(0f, 2.53f - 6.33f, 16.09f);
		}
		Vector3 s = new Vector3(160f, 180f, 160f);

		while ((transform.position - p).sqrMagnitude> 0.001f) {
			transform.localScale = Vector3.Lerp(transform.localScale, s, 0.5f);
			transform.position = Vector3.Lerp(transform.position, p, 0.1f);
			yield return new WaitForSeconds(0.01f);
		}
	}

	IEnumerator StaggeredShowBackground () {
		float transitionStepTime = transitionTime / renderers.Count;
		for (int i = 0; i < renderers.Count; i+=2) {
			for (int j = 0; j < 2; j++) {
				int index = i + j;
				if (index >= renderers.Count) {
					mainCamera.backgroundColor = defaultBackgroundColor;
					break;
				} else {
					renderers[index].enabled = true;
				}
			}
			mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, defaultBackgroundColor, 0.02f);
			yield return new WaitForSeconds(0f);
		}
	}

	public void TransitionIntoScene () {
        Debug.Log (_isSteveCenter);
        _isSteveCenter = false;
		StartCoroutine(MoveSteveToDefault());
		StartCoroutine(StaggeredShowBackground());
	}

	public void TransitionOutOfScene () {
		mainCamera.backgroundColor = transitionBackgroundColor;
        _isSteveCenter = true;
		StartCoroutine(MoveSteveToCenter());
		for (int i = 0; i < renderers.Count; i++) {
			if (!renderers[i].gameObject.tag.Equals("SpeechBubble")) {
				renderers[i].enabled = false;
			}
		}
	}
}
