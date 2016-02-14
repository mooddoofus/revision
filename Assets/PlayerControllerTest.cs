﻿using UnityEngine;
using System.Collections;

public class PlayerControllerTest : MonoBehaviour {

    [SerializeField]
    private GameObject _camera;
    [SerializeField]
    private float _moveSpeed;

    private float _moveForward;
    private float _moveSide;

    private Vector3 _forwardPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ManageMovement();
        ManageRotation();
    }

    void ManageMovement() {
        _forwardPosition = _camera.transform.forward;
        _moveForward = Input.GetAxis("Vertical");
        _moveSide = Input.GetAxis("Horizontal");
        transform.position += (_camera.transform.forward * _moveForward) + (_camera.transform.right *_moveSide);
    }

    void ManageRotation() {
        transform.rotation = _camera.transform.rotation;
    }
}
