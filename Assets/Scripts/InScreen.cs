﻿using UnityEngine;
using System.Collections;

public class InScreen : MonoBehaviour {
   
        Transform camera;
        [SerializeField] bool _flip;
        [SerializeField] Transform _followCharacter;
        //  [SerializeField] float _yOffset = 0f;
    bool _justLoaded = true;

        [SerializeField] GameObject bubble;
        [SerializeField] GameObject uiText;
        [SerializeField] GameObject tail;
        Vector3 cameraEdge;
        bool _fi = true;
//        RectTransform _rectTrans;
//        RectTransform _rectTrans2;
        RectTransform _rectTrans3;
        RectTransform _UIFungusRect;

    SceneTransition _sceneTrans;
    Vector3 _origPos;
    float _yPosCenter;

        void Awake () {
        _sceneTrans = _followCharacter.GetComponent <SceneTransition> ();
        _origPos = transform.position;
        Debug.Log (_origPos);
        _yPosCenter = _origPos.y + 3.6f;
            camera = Camera.main.transform;

        cameraEdge = Camera.main.ViewportToWorldPoint(new Vector3(0f,0f, 0f));
//            _rectTrans = uiText.GetComponent <RectTransform> ();
//            _rectTrans2 = bubble.GetComponent <RectTransform> ();
            _rectTrans3 = tail.GetComponent <RectTransform> ();
            _UIFungusRect = gameObject.GetComponent <RectTransform> ();
            
        }

        void Start() {
        _UIFungusRect.localPosition = new Vector3 (141f, 1.999981f, 318.1433f);


        }

        void Update () {
            Vector3 lookAtCamera = (camera.position - transform.position);
            lookAtCamera.y = 0;
            //keep the x coordinate still
            //      lookAtCamera.x = transform.position.x;
            transform.forward = _flip? -lookAtCamera : lookAtCamera;

        if (_sceneTrans._isSteveCenter == true) {
            transform.position = new Vector3 (_followCharacter.transform.position.x, _yPosCenter, _followCharacter.transform.position.z);
        } else {
//            transform.position = _origPos;
            //      transform.position = new Vector3 (_followCharacter.transform.position.x, _followCharacter.transform.position.y + _yOffset, _followCharacter.transform.position.z);
            //            transform.position = new Vector3 (_followCharacter.transform.position.x, _yPos, _followCharacter.transform.position.z);

//        }

            //      transform.position = new Vector3 (_followCharacter.transform.position.x, _followCharacter.transform.position.y + _yOffset, _followCharacter.transform.position.z);
            if (_fi == false) {
                transform.position = new Vector3 (_followCharacter.transform.position.x, _origPos.y, _followCharacter.transform.position.z);
            }
            _rectTrans3.position = new Vector3 (_followCharacter.transform.position.x, _rectTrans3.position.y, _followCharacter.transform.position.z);

            if (gameProgress.secondTime && _justLoaded){
                transform.position = new Vector3 (cameraEdge.x + 6.5f, _origPos.y, _followCharacter.transform.position.z);
                _justLoaded = false;
            }


            if (_fi == false && _rectTrans3.position.x - 6.5f < cameraEdge.x) {
                _fi = true;
            } else if (_fi == true && _rectTrans3.position.x - 6.5f >= cameraEdge.x) {
                _fi = false;
            }
        }
        }
    }


