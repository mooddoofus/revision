﻿using UnityEngine;
using System.Collections;

public class hubLevelSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//gameProgress.secondTime = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void jumpToLevel(string levelName){
		Application.LoadLevel(levelName);
	}
}
