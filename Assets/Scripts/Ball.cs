﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
	public Text displayText;

	private  Vector3 screenPoint ;
	private  Vector3 offset;
	public   Transform[] PinSet = new Transform[10];
	private Vector3[] PinPosition = new Vector3[10];
	private Quaternion[]  PinRotation = new Quaternion[10];
	private int turnChecker;
	private Vector3 defaultPosition;
	private Vector3 LastPinDefaultPosition;

	// Use this for initialization
	void Start () {
		turnChecker = 0;
		LastPinDefaultPosition = PinSet[PinSet.Length - 1].position;
		defaultPosition  = new Vector3(0f,1f,0f);
		displayText.text = "";
		for (int i = 0; i < 10; i++) {
			PinPosition[i] = PinSet[i].position;
			PinRotation[i] = PinSet[i].rotation;

				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		//Rigidbody rigidbody = GetComponent<Rigidbody> ();

		if((Vector3.Distance(defaultPosition,transform.position) >  Vector3.Distance(defaultPosition,LastPinDefaultPosition * 1.25f)))

		{
//			displayText.text = "Game Over";
			transform.position = defaultPosition;
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			for (int i = 0; i < 10; i++) {
				PinSet[i].position = PinPosition[i];
				PinSet[i].rotation = PinRotation[i];
				PinSet[i].rigidbody.velocity = Vector3.zero;
				PinSet[i].rigidbody.angularVelocity = Vector3.zero;

			}

		}

	}


	Vector2 velocity = Vector2.zero;
	Vector2 previousPosition;

	void OnMouseDown() { 
		
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		//	Debug.Log(screenPoint);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		previousPosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		// 	Debug.Log(offset);
	}




	void OnMouseDrag() 
	{  
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		//	 Debug.Log(curScreenPoint);
		Vector2 newPosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		velocity = (newPosition - previousPosition);

		previousPosition = newPosition;


		Vector3 curPosition   = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;




		transform.position = new Vector3(curPosition.x,defaultPosition.y, defaultPosition.z + curPosition.y);
		
	}
	
	void OnMouseUp()
	{
		Debug.Log (velocity.x + " " +  velocity.y);
		if (Mathf.Abs (velocity.x) > 80) {
			velocity = new Vector2(Mathf.Sign(velocity.x) * 80,velocity.y);
				}
		if (Mathf.Abs (velocity.y) > 80) {
			velocity = new Vector2(velocity.x,Mathf.Sign(velocity.y) * 80);
		}

		if(velocity.y > 10)
		rigidbody.AddForce(new Vector3(velocity.x,0,velocity.y) * 30.0f);

		
		turnChecker++;
		
		
	}
}
