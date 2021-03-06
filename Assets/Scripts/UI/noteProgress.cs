﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class noteProgress : MonoBehaviour {
	public int levelNumber;
	public static int totalNotes;
	public static int notesFound;
	public GameObject[] endNarrativeObjects;

	// Use this for initialization
	void Start () {
		notesFound = 0;
		GameObject[] tempNotes = GameObject.FindGameObjectsWithTag("Note");
		totalNotes = tempNotes.Length;
		updateText();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateText(){

		string tempString = notesFound.ToString()+"/"+totalNotes.ToString()+" notes found";
		gameObject.GetComponent<Text>().text = tempString;

		if(totalNotes == notesFound){
			for(int i=0; i < endNarrativeObjects.Length; i++){
				gameProgress.levelsCompleted [levelNumber] = true;
				gameProgress.printGameLevelStatus ();
				endNarrativeObjects[i].gameObject.SetActive(true);
			}
		}

	}

	public void addNote(){
		print("Adding note");
		notesFound++;
		updateText();
	}

}
