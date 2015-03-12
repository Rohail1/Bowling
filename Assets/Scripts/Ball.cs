﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

	public Text displayRound;
	public Text displayScore;
	public Text displayFinalScore;
	public BoxCollider boundingBox;
	private Vector3 screenPoint ;
	private Vector3 offset;
	public  GameObject[] PinSet = new GameObject[10];
	private Vector3[] PinPosition = new Vector3[10];
	private Quaternion[]  PinRotation = new Quaternion[10];
	private Vector3 defaultCameraPosition;
	private Quaternion defaultCameraRotation;
	private Vector3 defaultPosition;
	private Vector3 LastPinDefaultPosition;
	private bool soundFlag;
	public Camera cam1;
	public Camera cam2;
	private int currentTurn = 0;
	private int totalScore = 0;
	public static int FinalScore = 0;
	private int NumberOfGames = 1;
	private Vector3 tempPosition;
	private bool replayFlagChecker =false;
	private Vector3 force;
	private Replay replayObject = new Replay();
	// Use this for initialization
	void Start () {

		Physics.IgnoreCollision (this.GetComponent<Collider>(), boundingBox);
		defaultCameraPosition = cam1.transform.position;
		defaultCameraRotation = cam1.transform.rotation;
		LastPinDefaultPosition = PinSet[PinSet.Length - 1].transform.position;
		defaultPosition = transform.position;
		NumberOfGames = 1;
		totalScore = 0;
		FinalScore = 0;
		soundFlag = false;
		displayScore.text = displayFinalScore.text = "";
		displayRound.text = NumberOfGames+"";
		for (int i = 0; i < 10; i++) {
			PinPosition [i] = PinSet [i].transform.position;
			PinRotation [i] = PinSet [i].transform.rotation;
			replayObject.AddObjectForReplay(PinSet[i].transform);	
				
		}
		replayObject.AddObjectForReplay (this.transform);


		cam1.enabled = true;
	//	cam2.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	


	}

	void OnCollisionEnter(Collision c)
	{

		if (c.gameObject.tag == "PIN" && !soundFlag) {
			cam1.GetComponent<AudioSource>().Play();
			soundFlag = true;

				}
	}




	void FixedUpdate()

	{			replayObject.Update ();
				
				if (NumberOfGames > 5) {
						Application.LoadLevel ("ScoreMenu");
				}

//				if (transform.rigidbody.velocity.magnitude > 2 && cam1.enabled) {
//
//
//						if (Vector3.Distance (cam1.transform.position, transform.position) > 7 && Vector3.Distance (cam1.transform.position, LastPinDefaultPosition) > 25) {
//								cam1.transform.position = new Vector3 (cam1.transform.position.x,
//				                                      Mathf.Lerp (defaultCameraPosition.y, defaultCameraPosition.y + 2f,
//				             Vector3.Distance (cam1.transform.position, transform.position) / Vector3.Distance (cam1.transform.position, LastPinDefaultPosition)),
//				                                      transform.position.z - 7);
//						}
//		
//				}
		if (Vector3.Distance (defaultPosition, transform.position) > Vector3.Distance (defaultPosition, LastPinDefaultPosition )* 0.1f
					     && GetComponent<Rigidbody>().velocity.magnitude < .1f && !replayFlagChecker){
								reset();
					}
		if((Vector3.Distance (defaultPosition, transform.position) > Vector3.Distance (defaultPosition, LastPinDefaultPosition *2.1f))  && !replayFlagChecker) {
			reset ();
			}
//		 if (Vector3.Distance (defaultPosition, transform.position) > Vector3.Distance (defaultPosition, LastPinDefaultPosition )* 0.1f
//		    && GetComponent<Rigidbody>().velocity.magnitude < .1f && replayFlagChecker){
//			Replayreset();
//		}
//		 if((Vector3.Distance (defaultPosition, transform.position) > Vector3.Distance (defaultPosition, LastPinDefaultPosition *2.1f))&& replayFlagChecker) {
//			Replayreset ();
//		}

	}


	Vector2 velocity = Vector2.zero;
	Vector2 previousPosition;
	
	void replay()
	{	
		replayFlagChecker = true;
		this.transform.position = tempPosition;
		this.transform.rotation = ballRotation;
		Debug.Log(force);
		this.GetComponent<Rigidbody>().AddForce (force);
		Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
		this.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
	}



	void Replayreset()
	{
//		if (currentTurn == 1) {
//				RemoveFallenPinsForReplay ();
//				}
		this.gameObject.layer = LayerMask.NameToLayer ("Default");
		transform.position = defaultPosition;
		cam1.transform.position = defaultCameraPosition;
		cam1.transform.rotation = defaultCameraRotation;
		soundFlag = false;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		Debug.Log ("Replays reset");
		//replayFlagChecker = false;

	}


	void reset()
	{

		Debug.Log ("reset");
		this.gameObject.layer = LayerMask.NameToLayer ("Default");
		transform.position = defaultPosition;
		replayObject.isRecordingStarted = false;
		cam1.transform.position = defaultCameraPosition;
		cam1.transform.rotation = defaultCameraRotation;
		soundFlag = false;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		int score = 0;
		if (!replayFlagChecker) {
			score = GetFallenPinsCount ();
				}

				if (currentTurn == 0 && !replayFlagChecker) {

						if (score == PinPosition.Length) {
								score = 20;
								displayScore.text = score + "";
								FinalScore += score;	
								displayFinalScore.text = FinalScore + "";
//								ResetForReplay ();
								replayFlagChecker = true;
								replayObject.Play = true;
//								replay ();	
								ResetAll ();
			

						} else {
								totalScore = score;
								FinalScore += totalScore;
								displayScore.text = score + "";
								displayFinalScore.text = FinalScore + "";
								currentTurn++;
//								ResetForReplay ();
//								replay ();
								replayFlagChecker = true;
								replayObject.Play = true;
//								reset();
//								RemoveFallenPinsForReplay();
								
						}
				} else if (currentTurn == 1 && !replayFlagChecker) {
						if (score + totalScore == PinPosition.Length) {
								displayScore.text = (score + 4) + "";
								FinalScore += (score + 4);
								displayScore.text = FinalScore + "";
//								ResetForReplay ();
								replayFlagChecker = true;
								replayObject.Play = true;
//								replay ();

						} else {
								totalScore += score;
								displayScore.text = score + "";
								FinalScore += score;
								displayFinalScore.text = FinalScore + "";
//								ResetForReplay ();
								replayFlagChecker = true;
								replayObject.Play = true;
//								replay ();
						}
						//	ResetForReplay();
						//	replay();
						
						ResetAll ();
						//		replay();
				
				} 

		}

	void ResetAll(){

			replayFlagChecker = false;
			replayObject.isRecordingStarted = false;
			currentTurn = 0;
			displayScore.text = 0 + "";
			NumberOfGames++;
			displayRound.text = NumberOfGames+"";
		for (int i = 0; i < 10; i++) {
			PinSet[i].transform.position = PinPosition[i];
			PinSet[i].transform.rotation = PinRotation[i];

//			PinSet[i].transform.GetChild(0).transform.renderer.enabled = true;
//			PinSet[i].transform.collider.enabled = true;

			PinSet[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
			PinSet[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			PinSet[i].SetActive(true);
		}
	}

	void ResetForReplay()
	{


		for (int i = 0; i < 10; i++) {
		
//			if(!PinSet[i].activeSelf)
//				continue;
			PinSet[i].transform.position = PinPosition[i];
			PinSet[i].transform.rotation = PinRotation[i];
			PinSet[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
			PinSet[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			PinSet[i].SetActive(true);
		}
		Debug.Log ("Reset for Replay");
	//	cam1.enabled = false;
	//	cam2.enabled = true;
	}
	

	int GetFallenPinsCount(){
		int score = 0;
		for (int i = 0; i < 10; i++) {
			Vector3 cuurentRotation = PinSet[i].transform.rotation.eulerAngles;
			Vector3 defaultRotation = PinRotation[i].eulerAngles;


			if(!PinSet[i].activeSelf)
				continue;

			if((Mathf.Abs(cuurentRotation.x - defaultRotation.x) > 40 && Mathf.Abs(cuurentRotation.x - defaultRotation.x) < 320  ) ||
			   (Mathf.Abs(cuurentRotation.y - defaultRotation.y) > 40 && Mathf.Abs(cuurentRotation.y - defaultRotation.y) < 320  ) ||
			   (Mathf.Abs(cuurentRotation.z - defaultRotation.z) > 40 && Mathf.Abs(cuurentRotation.z - defaultRotation.z) < 320  ) 
			   ){
//				PinSet[i].GetChild(0).transform.renderer.enabled = PinSet[i].transform.collider.enabled = false;
				PinSet[i].SetActive(false);
				score++;
			}
			
		}
		return score;
	}

	void RemoveFallenPinsForReplay()
	{
	

		for (int i = 0; i < 10; i++) {
			Vector3 cuurentRotation = PinSet[i].transform.rotation.eulerAngles;
			Vector3 defaultRotation = PinRotation[i].eulerAngles;

			if(!PinSet[i].activeSelf)
				continue;
			
			if((Mathf.Abs(cuurentRotation.x - defaultRotation.x) > 40 && Mathf.Abs(cuurentRotation.x - defaultRotation.x) < 320  ) ||
			   (Mathf.Abs(cuurentRotation.y - defaultRotation.y) > 40 && Mathf.Abs(cuurentRotation.y - defaultRotation.y) < 320  ) ||
			   (Mathf.Abs(cuurentRotation.z - defaultRotation.z) > 40 && Mathf.Abs(cuurentRotation.z - defaultRotation.z) < 320  ) 
			   ){

				PinSet[i].SetActive(false);
			}
			
		}
	//	cam2.enabled = false;
	//	cam1.enabled = true;
	}

	void OnMouseDown() { 
		
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		previousPosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

	}

	void OnMouseDrag() 

	{  

//		Debug.Log(Mathf.Clamp(2,0,5));
//		Debug.Log (Input.mousePosition.x);
//		if (Input.mousePosition.x < 178) {
//						Vector3 curScreenPoint = new Vector3 (178, /*Input.mousePosition.y*/screenPoint.y, screenPoint.z);
//						Vector2 newPosition = new Vector2 (178, Input.mousePosition.y);
//						velocity = (newPosition - previousPosition);
//						previousPosition = newPosition;
//						Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
//						transform.position = new Vector3 (curPosition.x, defaultPosition.y, defaultPosition.z + curPosition.y);
//				} else if (Input.mousePosition.x > 420) {
//						Vector3 curScreenPoint = new Vector3 (420, /*Input.mousePosition.y*/screenPoint.y, screenPoint.z);
//						Vector2 newPosition = new Vector2 (420, Input.mousePosition.y);
//						velocity = (newPosition - previousPosition);
//						previousPosition = newPosition;
//						Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
//						transform.position = new Vector3 (curPosition.x, defaultPosition.y, defaultPosition.z + curPosition.y);
//
//				} else {		
//						Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, /*Input.mousePosition.y*/screenPoint.y, screenPoint.z);
//						Vector2 newPosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
//						velocity = (newPosition - previousPosition);
//						previousPosition = newPosition;
//						Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
//						transform.position = new Vector3 (curPosition.x, defaultPosition.y, defaultPosition.z + curPosition.y);
//				}
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, /*Input.mousePosition.y*/screenPoint.y, screenPoint.z);
		Vector2 newPosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		velocity = (newPosition - previousPosition);
		previousPosition = newPosition;
		Vector3 curPosition   = Camera.main.ScreenToWorldPoint(curScreenPoint)+ offset;
		transform.position = new Vector3(curPosition.x,defaultPosition.y, defaultPosition.z + curPosition.y);

	}

	private Quaternion ballRotation;

	void OnMouseUp()
	{
		if (Mathf.Abs (velocity.x) > 80) {
			velocity = new Vector2(Mathf.Sign(velocity.x) * 80,velocity.y);
				}

		if (Mathf.Abs (velocity.y) > 80) {
			velocity = new Vector2(velocity.x,Mathf.Sign(velocity.y) * 80);
		}

		if (velocity.y > 10) {

			tempPosition = this.transform.position;
			ballRotation = this.transform.rotation;
			force = new Vector3 (velocity.x / 7f, 0, velocity.y) * 55.0f;
			Debug.Log(force);
			replayObject.isRecordingStarted = true;
			this.GetComponent<Rigidbody>().AddForce (force);
			Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
			this.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

						
						
				}
	}

}