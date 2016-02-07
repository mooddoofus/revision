﻿using UnityEngine;
using System.Collections;

public class WordClickScript : MonoBehaviour {

    [SerializeField]
    private int _wordStringCount;

    [SerializeField]
    private GameObject _UIPrefab;
    [SerializeField]
    private GameObject _UIParent;
   

	// Use this for initialization
	void Awake () {
        _wordStringCount = GetComponent<TextMesh>().text.Length;
        GetComponent<BoxCollider>().size = new Vector3(_wordStringCount*5 / 8, 1f, 1f);
	}

    public void Initialize(GameObject UIParent) {
        _UIParent = UIParent;
    }
    

    void OnMouseDown() {
        Debug.Log("HELLO I WILL TRANSFER");
        GameObject UIObj = Instantiate(_UIPrefab, transform.position, transform.rotation) as GameObject;
        UIObj.GetComponent<TextUIObject>().Initialize(GetComponent<TextMesh>().text);
        UIObj.transform.parent = GameObject.Find("UIWordPanel").transform;
    }
}
