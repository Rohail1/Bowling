﻿using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadLevel()
	{

		Application.LoadLevel("Main");

	}
	public void EndGame()
	{
		Application.Quit();
		
	}
}
